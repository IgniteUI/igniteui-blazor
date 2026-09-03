using System.Diagnostics.CodeAnalysis;
#if NET8_0
using System.Linq.Expressions;
#endif
using System.Runtime.CompilerServices;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Controls
{
    // The InvokeUnmarshalled fast path exists only on net8 (API removed in net9+), so the probe and its
    // trim/AOT surface compile only there; net9+ always uses the raw-pointer InvokeVoid path.
    internal class RuntimeHelper
    {
#if NET8_0
        private Func<IJSInProcessRuntime, string, string, int, UnmarshalledColumn[], string> _callSendUnmarshalledColumnMessage;
        private Func<IJSInProcessRuntime, string, string, string, string> _callSendUnmarshalledColumnDataIntentMessage;
#endif
        private IJSInProcessRuntime _inprocRuntime;
        private IIgniteUIBlazor _igBlazor;

#if NET8_0
        [DynamicDependency(
            DynamicallyAccessedMemberTypes.PublicMethods,
            "Microsoft.AspNetCore.Components.WebAssembly.Services.DefaultWebAssemblyJSRuntime",
            "Microsoft.AspNetCore.Components.WebAssembly")]
        [UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Probes the net8-only InvokeUnmarshalled methods, preserved via the DynamicDependency above; absence falls back to the raw-pointer InvokeVoid path.")]
        [UnconditionalSuppressMessage("Trimming", "IL2060", Justification = "The generic arguments are statically referenced framework/library types, and the InvokeUnmarshalled generic parameters carry no DynamicallyAccessedMembers requirements.")]
        [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "The DynamicDependency above marks the runtime's RequiresUnreferencedCode members (Invoke, GetValue, SetValue, ...); the probe filters by name and never invokes them.")]
        [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "net8 Blazor WASM-only probe (IJSInProcessRuntime + InvokeUnmarshalled); no NativeAOT target exists for net8 wasm, and under Mono AOT the interpreter executes this.")]
#endif
        public RuntimeHelper(IJSRuntime runtime, IIgniteUIBlazor igBlazor)
        {
            _igBlazor = igBlazor;
            var inprocRuntime = runtime as IJSInProcessRuntime;
            _inprocRuntime = inprocRuntime;
            if (inprocRuntime != null)
            {
                IsInproc = true;
            }
#if NET8_0
            if (IsInproc)
            {
                var unmarshalled = inprocRuntime.GetType().GetMethods().Where(m => m.Name == "InvokeUnmarshalled").ToList();

                if (unmarshalled.Count > 0)
                {
                    var target = unmarshalled.Where(m => m.GetGenericArguments() != null &&
                    m.GetGenericArguments().Length == 4).FirstOrDefault();
                    if (target != null)
                    {
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
                        Expression.Lambda<Func<IJSInProcessRuntime, string, string, int, UnmarshalledColumn[], string>>(
                            call, jsRuntimeParam, methodNameParam, refNameParam, indexParam, columnsParam).Compile();
                    }

                    target = unmarshalled.Where(m => m.GetGenericArguments() != null &&
                    m.GetGenericArguments().Length == 3).FirstOrDefault();
                    if (target != null)
                    {
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
                        Expression.Lambda<Func<IJSInProcessRuntime, string, string, string, string>>(
                            call, jsRuntimeParam, methodNameParam, refNameParam, dataIntentParam).Compile();
                    }
                }
            }
#endif
        }

        public unsafe string SendUnmarshalledColumnMessage(string methodName, string refName, int index, UnmarshalledColumn[] columns)
        {
#if NET8_0
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
#if NET8_0
            if (_callSendUnmarshalledColumnDataIntentMessage != null)
            {
                return _callSendUnmarshalledColumnDataIntentMessage(_inprocRuntime, methodName, refName, dataIntents);
            }
#endif
            _inprocRuntime.InvokeVoid(methodName, new object[] { refName, dataIntents });

            return null;
        }

        public bool IsInproc { get; private set; }
        public bool IsForcedJsonDataMarshalling { get { return _igBlazor.Settings.ForceJsonDataMarshalling; } }
    }
}
