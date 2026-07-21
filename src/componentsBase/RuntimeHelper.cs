using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace IgniteUI.Blazor.Controls
{
    internal class RuntimeHelper
    {
#if NET5_0
        private IJSUnmarshalledRuntime _unmarshalledRuntime;
#else
        private Func<IJSInProcessRuntime, string, string, int, UnmarshalledColumn[], string> _callSendUnmarshalledColumnMessage;
        private Func<IJSInProcessRuntime, string, string, string, string> _callSendUnmarshalledColumnDataIntentMessage;
#endif
        private IJSInProcessRuntime _inprocRuntime;
        private IIgniteUIBlazor _igBlazor;

#if !NETSTANDARD
        [DynamicDependency(
            DynamicallyAccessedMemberTypes.PublicMethods,
            "Microsoft.AspNetCore.Components.WebAssembly.Services.DefaultWebAssemblyJSRuntime",
            "Microsoft.AspNetCore.Components.WebAssembly")]
#endif
        //[System.Diagnostics.CodeAnalysis.DynamicDependency(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicMethods, typeof(WebAssemblyJSRuntime))]
        public RuntimeHelper(IJSRuntime runtime, IIgniteUIBlazor igBlazor)
        {
            _igBlazor = igBlazor;
            //Console.WriteLine("initializing runtime helper");
            if (runtime == null)
            {
                //Console.WriteLine("runtime is null");
            }
            var inprocRuntime = runtime as IJSInProcessRuntime;
            _inprocRuntime = inprocRuntime;
            if (inprocRuntime != null)
            {
                IsInproc = true;
                //Console.WriteLine("is inproc");
            }
            if (IsInproc)
            {
#if NET5_0
                _unmarshalledRuntime = _inprocRuntime as IJSUnmarshalledRuntime;
#else
                //Console.WriteLine("inproc type: " + _inprocRuntime.GetType().Name);
                var unmarshalled = inprocRuntime.GetType().GetMethods().Where(m => m.Name == "InvokeUnmarshalled").ToList();

                var name = inprocRuntime.GetType().Assembly.GetName();
                if (unmarshalled.Count > 0)
                {
                    var target = unmarshalled.Where(m => m.GetGenericArguments() != null &&
                    m.GetGenericArguments().Length == 4).FirstOrDefault();
                    if (target != null)
                    {
                        //Console.WriteLine("found target");
                        var meth = target.MakeGenericMethod(new Type[] { 
                            typeof(string), 
                            typeof(int), 
                            typeof(UnmarshalledColumn[]),
                            typeof(string)
                            });

                        var jsRuntimeParam = Expression.Parameter(typeof(IJSInProcessRuntime), "jsRuntime");
                        var methodNameParam = Expression.Parameter(typeof(string), "methodName");
                        var refNameParam = Expression.Parameter(typeof(string), "refName");
                        var indexParam = Expression.Parameter(typeof(int), "index");
                        var columnsParam = Expression.Parameter(typeof(UnmarshalledColumn[]), "columns");

                        var wsRuntime = Expression.Convert(jsRuntimeParam, inprocRuntime.GetType());
                        var call = Expression.Call(wsRuntime, meth, methodNameParam, refNameParam,
                        indexParam, columnsParam);

                        _callSendUnmarshalledColumnMessage = 
                        (Func<IJSInProcessRuntime, string, string, int, UnmarshalledColumn[], string>)Expression.Lambda(
                            call, jsRuntimeParam, methodNameParam, refNameParam, indexParam, columnsParam).Compile();
                    }

                    target = unmarshalled.Where(m => m.GetGenericArguments() != null &&
                    m.GetGenericArguments().Length == 3).FirstOrDefault();
                    if (target != null)
                    {
                        //Console.WriteLine("found target");
                        var meth = target.MakeGenericMethod(new Type[] { 
                            typeof(string), 
                            typeof(string), 
                            typeof(string)
                            });

                        var jsRuntimeParam = Expression.Parameter(typeof(IJSInProcessRuntime), "jsRuntime");
                        var methodNameParam = Expression.Parameter(typeof(string), "methodName");
                        var refNameParam = Expression.Parameter(typeof(string), "refName");
                        var dataIntentParam = Expression.Parameter(typeof(string), "index");
                        
                        var wsRuntime = Expression.Convert(jsRuntimeParam, inprocRuntime.GetType());
                        var call = Expression.Call(wsRuntime, meth, methodNameParam, refNameParam,
                        dataIntentParam);

                        _callSendUnmarshalledColumnDataIntentMessage = 
                        (Func<IJSInProcessRuntime, string, string, string, string>)Expression.Lambda(
                            call, jsRuntimeParam, methodNameParam, refNameParam, dataIntentParam).Compile();
                    }
                }
#endif
            }
        }

        public unsafe string SendUnmarshalledColumnMessage(string methodName, string refName, int index, UnmarshalledColumn[] columns)
        {
#if NET5_0
            if (_unmarshalledRuntime != null)
            {
                return _unmarshalledRuntime.InvokeUnmarshalled<string, int, UnmarshalledColumn[], string>(methodName, refName, index, columns);
            }
#else
            if (_callSendUnmarshalledColumnMessage != null)
            {
                return _callSendUnmarshalledColumnMessage(_inprocRuntime, methodName, refName, index, columns);
            }
#endif
            var intptr = Unsafe.AsPointer(ref columns);
            _inprocRuntime.InvokeVoid(methodName, new object[] { refName, index, (int)intptr });

            return null;
        }

        public string SendUnmarshalledColumnDataIntentsMessage(string methodName, string refName, string dataIntents)
        {
#if NET5_0
            if (_unmarshalledRuntime != null)
            {
                //Console.WriteLine("invoking data intent");
                return _unmarshalledRuntime.InvokeUnmarshalled<string, string, string>(methodName, refName, dataIntents);
            }
#else
            if (_callSendUnmarshalledColumnMessage != null)
            {
                //Console.WriteLine("invoking sadness");
                return _callSendUnmarshalledColumnDataIntentMessage(_inprocRuntime, methodName, refName, dataIntents);
            }
#endif
            _inprocRuntime.InvokeVoid(methodName, new object[] { refName, dataIntents });

            return null;
        }

        public bool IsInproc { get; private set; }
        public bool IsForcedJsonDataMarshalling { get { return _igBlazor.Settings.ForceJsonDataMarshalling;  } }
    }
}