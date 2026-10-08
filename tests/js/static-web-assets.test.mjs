import assert from 'node:assert/strict';
import { existsSync, readdirSync, readFileSync } from 'node:fs';
import { join } from 'node:path';
import { test } from 'node:test';

const wwwroot = 'src/wwwroot';
const read = (name) => readFileSync(join(wwwroot, name), 'utf8');
const jsFiles = readdirSync(wwwroot).filter((f) => f.endsWith('.js'));
const exportsOf = (source) =>
  [...source.matchAll(/export\s*\{([^}]*)\}/g)]
    .flatMap((m) => m[1].split(','))
    .map((e) =>
      e
        .trim()
        .split(/\s+as\s+/)
        .pop(),
    )
    .sort();

test('no emitted file inspects its own URL', () => {
  for (const f of jsFiles) {
    assert.doesNotMatch(read(f), /document\.currentScript|import\.meta/, f);
  }
});

test('initializer is a flat import list covering the app entry and api.js', () => {
  const lines = read('IgniteUI.Blazor.Lite.lib.module.js').trim().split('\n');
  const imported = lines.map((l) => {
    const m = /^import '\.\/(.+)';$/.exec(l);
    assert.ok(m, `unexpected initializer line: ${l}`);
    assert.ok(jsFiles.includes(m[1]), `initializer imports a missing file: ${m[1]}`);
    return m[1];
  });
  assert.ok(
    imported.some((f) => /^app\..+\.bundle\.js$/.test(f)),
    'app entry missing',
  );
  assert.ok(imported.includes('api.js'), 'api.js missing');
});

test('api.js exports everything api.d.ts declares', () => {
  // api.d.ts is generated separately; with preserveEntrySignatures 'allow-extension' this guarantees the declared exports exist.
  // @internal exports (used by the bundle, hidden from the typings) may exist under any name — the bundle is built with them.
  const dts = read('api.d.ts');
  const declared = [...dts.matchAll(/^export declare (?:function|const|let) (\w+)/gm)].map((m) => m[1]);
  assert.ok(declared.length > 0, 'api.d.ts declares no runtime exports');
  assert.doesNotMatch(dts, /@internal/, 'stripInternal did not run');
  const exported = exportsOf(read('api.js'));
  for (const name of declared) {
    assert.ok(exported.includes(name), `api.js does not export ${name}`);
  }
});

test('lit-html.js is the real lit-html module', async () => {
  // A strict entry signature is what makes it swappable for another lit-html copy via import map.
  const real = Object.keys(await import('lit-html')).sort();
  assert.deepEqual(exportsOf(read('lit-html.js')), real);
  // ...and it must be the implementation, not a facade re-exporting a copy that landed in another chunk
  // (which chunk groups can cause), or the override would leave the library on its own copy.
  assert.deepEqual(
    jsFiles.filter((f) => read(f).includes('litHtmlVersions')),
    ['lit-html.js'],
  );
});

test('no Ignite UI window state', () => {
  const windowState = /\b__ig[A-Z]/;
  assert.match('w.__igLoaded = 1', windowState);
  for (const f of jsFiles) {
    assert.doesNotMatch(read(f), windowState, f);
  }
});

test('legacy script globals and compatibility bundle are not emitted', () => {
  assert.equal(existsSync(join(wwwroot, 'app.bundle.js')), false);
  const emitted = jsFiles.map(read).join('\n');
  assert.doesNotMatch(emitted, /igRegisterScript|igRemoveScript|igTemplating/);
});

test('no free `global` references (webpack polyfilled it; ESM does not)', () => {
  const nodeGlobal = /[^\w$.]global\./;
  assert.match('x=global.getValue', nodeGlobal); // the pattern is live
  for (const f of jsFiles) {
    assert.doesNotMatch(read(f), nodeGlobal, f);
  }
});

test('legal notices survive: in-source ones inline, dependencies via the license manifest', () => {
  assert.ok(existsSync('src/THIRD-PARTY-LICENSES.md'), 'build.license manifest missing next to the csproj');
  // Notices inside our own sources (e.g. vendored snippets) are invisible to build.license, so they must stay inline.
  const emitted = jsFiles.map(read).join('\n');
  const walk = (dir) => {
    for (const e of readdirSync(dir, { withFileTypes: true })) {
      const p = join(dir, e.name);
      if (e.isDirectory()) walk(p);
      else if (e.name.endsWith('.ts')) {
        for (const m of readFileSync(p, 'utf8').matchAll(/\/\*!\s*([^\n]+)/g)) {
          const notice = m[1].trim();
          assert.ok(emitted.includes(notice), `legal notice dropped from the output: ${notice}`);
        }
      }
    }
  };
  walk('src/src');
});

test('api.js registers and removes scripts, defaulting shouldCall to false', async () => {
  // lit-html touches document at module scope.
  globalThis.document ??= { createTreeWalker: () => ({}), createComment: () => ({}), createElement: () => ({}) };
  const api = await import(new URL(`../../${wwwroot}/api.js`, import.meta.url));
  const fn = () => 'value';
  api.registerScript('default', fn);
  api.registerScript('factory', fn, true);
  assert.equal(api._getRegisteredScript('default').func, fn);
  assert.equal(api._getRegisteredScript('default').shouldCall, false);
  assert.equal(api._getRegisteredScript('factory').shouldCall, true);
  api.removeScript('default');
  assert.equal(api._getRegisteredScript('default'), undefined);
  assert.equal(typeof api.html, 'function');
});
