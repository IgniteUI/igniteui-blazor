//function that invokes the server-side render of a component by name
async function renderComponent(componentName) {
  var res = await DotNet.invokeMethodAsync('IgniteUI.Blazor.Lite.TestBed', 'SetComponentType', componentName);
  console.log('Task completed successfully: ' + res);
}

function onAfterRender() {
  console.log('App Loaded.');
}

async function setSelector(componentSelector) {
  window.targetName = componentSelector;
}

async function getErrors() {
  var res = await DotNet.invokeMethodAsync('IgniteUI.Blazor.Lite.TestBed', 'GetErrors');
  return res;
}

// dispatch a spy
function spyOnMethod(methodName) {
  console.log('Spy on: ' + methodName);
  var container = document.getElementById('container');
  var target = container.querySelectorAll(window.targetName)[0];
  var spy = Spy(target, methodName);
  window.SpyAgent = spy;
}

// check on spy to get what was called on target.
async function checkOnSpy(methodName) {
  var args = window.SpyAgent.args;
  var result = window.SpyAgent.result[0];
  if (Object.prototype.toString.call(result) === '[object Promise]') {
    result = await result;
  }
  var info = { methodName: methodName, args: args, result: result };
  console.log('Verify method info: ', info);
  return JSON.stringify(result);
}

function stringifyObject(object) {
  var cacheSet = new Set();
  var result = JSON.stringify(object, function (key, value) {
    if (
      (key && key.startsWith('$')) ||
      key.startsWith('_') ||
      key == 'name' ||
      key == 'externalObject' ||
      key == 'level' ||
      value === null
    ) {
      // skip internal and nulls
      return;
    }
    if (typeof value === 'object' && value !== null) {
      if (cacheSet.has(value)) {
        // Circular reference found, discard key
        return;
      }
      cacheSet.add(value);
    }
    return value;
  });
  cacheSet = null;
  return result;
}

function checkServerTemplate(propName) {
  var container = document.getElementById('container');
  var target = container.querySelectorAll(window.targetName)[0];
  var tmpl = target[propName];
  console.log('Checking server template: ' + propName);
  return tmpl?.___templateId !== null;
}

function checkClientTemplate(propName) {
  var container = document.getElementById('container');
  var target = container.querySelectorAll(window.targetName)[0];
  var tmpl = target[propName];
  console.log('Checking client template: ' + propName);
  return tmpl?.name === 'TmplHandler';
}

function checkProp(propName) {
  var container = document.getElementById('container');
  // TODO - probably need more complex logic to get target here
  var target = container.querySelectorAll(window.targetName)[0];
  console.log('Checking prop: ' + propName);
  var val = stringifyObject(target[propName]);
  console.log(val);
  return val;
}

function triggerEvent(eventName, arg) {
  var container = document.getElementById('container');
  var target = container.querySelectorAll(window.targetName)[0];
  console.log('Trigger event: ' + eventName);
  target.dispatchEvent(
    new CustomEvent(
      eventName,
      Object.assign(
        {
          bubbles: true,
          cancelable: false,
          composed: true,
          detail: arg || {},
        },
        {},
      ),
    ),
  );
}

// this spies on function calls, to know when a method was called.
function Spy(obj, method) {
  let spy = {
    args: [],
    result: [],
  };

  let original = obj[method];
  obj[method] = function () {
    let args = [].slice.apply(arguments);
    spy.count++;
    spy.args.push(args);
    let res = original.apply(obj, args);
    spy.result.push(res);
    return res;
  };

  return Object.freeze(spy);
}

function generateClientTmpl(name) {
  igRegisterScript(
    name,
    function TmplHandler(ctx) {
      var html = window.igTemplating.html;
      return html`<div>Template</div>`;
    },
    false,
  );
}
