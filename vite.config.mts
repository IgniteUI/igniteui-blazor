import { cpSync, renameSync } from 'node:fs';
import { resolve } from 'node:path';
import { defineConfig, type Plugin } from 'vite';

const outDir = 'src/wwwroot';
const initializer = 'IgniteUI.Blazor.Lite.lib.module.js';
const licenseManifest = 'THIRD-PARTY-LICENSES.md';
/** Public modules keep fixed names so apps can import them and/or import maps can override them. */
const fixedNames: Record<string, string> = { api: 'api.js', 'lit-html': 'lit-html.js', legacyStub: 'app.bundle.js' };
const entries = { app: 'src/src/index.ts', api: 'src/src/api.ts', legacyStub: 'src/src/app.bundle.ts' };

/**
 * Emits the Blazor JS initializer as a flat list of static imports.
 * Blazor awaits the import, so no startup hooks required for that.
 * api.js included for the deprecated window globals before Blazor starts.
 */
function emitInitializer(): Plugin {
  return {
    name: 'ig-emit-initializer',
    generateBundle(_options, bundle) {
      const app = Object.values(bundle).find((c) => c.type === 'chunk' && c.isEntry && c.name === 'app');
      if (!app) {
        throw this.error(`no 'app' entry chunk — cannot emit ${initializer}`);
      }
      const imports = new Set<string>();
      const visit = (fileName: string) => {
        if (imports.has(fileName)) return;
        imports.add(fileName);
        const chunk = bundle[fileName];
        if (chunk?.type === 'chunk') chunk.imports.forEach(visit);
      };
      visit(app.fileName);
      if (bundle[fixedNames.api]?.type !== 'chunk') {
        throw this.error(`no ${fixedNames.api} entry chunk — cannot emit ${initializer}`);
      }
      visit(fixedNames.api);
      this.emitFile({
        type: 'asset',
        fileName: initializer,
        source: [...imports].map((f) => `import './${f}';`).join('\n') + '\n',
      });
    },
  };
}

/** Copy igniteui-webcomponents themes to wwwroot & move the license manifest */
function copyThemes(): Plugin {
  return {
    name: 'ig-copy-themes',
    closeBundle() {
      cpSync('node_modules/igniteui-webcomponents/themes', `${outDir}/themes`, { recursive: true });
      // move out of wwwroot since it's not a web asset (rolldown only emits inside outDir)
      renameSync(`${outDir}/${licenseManifest}`, `src/${licenseManifest}`);
    },
  };
}

export default defineConfig(({ mode }) => {
  const isDev = mode === 'development';
  return {
    // Satisfies lib-mode validation; the build uses rolldownOptions.input below.
    input: entries,
    resolve: { alias: { 'igniteui-core': resolve(import.meta.dirname, 'src/src/ig/igniteui-core') } },
    plugins: [emitInitializer(), copyThemes()],
    build: {
      target: 'es2022',
      outDir,
      emptyOutDir: true,
      // One manifest of every bundled dependency's license; does not include inlined ones from rolldownOptions.output.comments.legal
      license: { fileName: licenseManifest },
      sourcemap: isDev,
      // Lib mode keeps the lazy imports free of vite's preload helper (import.meta.url-based — wrong under _content/).
      lib: { formats: ['es'] },
      rolldownOptions: {
        // Separate entry for lit-html as its own stable module (lit-html.js).
        input: { ...entries, 'lit-html': 'lit-html' },
        // Public modules keep their exports ('allow-extension' rather than 'strict' only because the igniteui-core group below
        // must not follow dependencies; npm test asserts the declared exports exist).
        preserveEntrySignatures: 'allow-extension',
        output: {
          entryFileNames: (chunk) => fixedNames[chunk.name] ?? '[name].[hash].bundle.js',
          chunkFileNames: '[name].[hash].bundle.js',
          // Lib mode only honors minify at the output level.
          minify: !isDev,
          // Legal notices (@license, /*! …) stay inline:
          comments: { legal: true, annotation: false, jsdoc: false },
          codeSplitting: {
            groups: [
              // One igniteui-core chunk. Dependencies are not followed: template.ts imports lit-html,
              // which would otherwise be folded in and turn lit-html.js into a facade of this chunk.
              { name: 'igniteui-core', test: /[\/]igniteui-core[\/]/, includeDependenciesRecursively: false },

              // Knob: only the runtime the initializer reaches statically goes in one `core` chunk; per-component descriptions
              // keep rolldown's default splitting and load lazily (~32 KB gzip deferred, one extra request per component type).
              // Compatible with preserveEntrySignatures: 'strict'.
              // { name: 'core', tags: ['$initial'], test: /[\/]igniteui-core[\/]/ },
            ],
          },
        },
      },
    },
  };
});
