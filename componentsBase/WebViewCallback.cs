using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Controls
{
    public class WebCallback
    {
        //private WebCallback() { }
        //private static WebCallback _instance;
        //public static WebCallback Instance {
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new WebCallback();                 
        //        }                    

        //        return _instance;
        //    }
        //    private set
        //    {
        //        _instance = value;
        //    } 
        //}
        
        public bool IsReady
        {
            get
            {
                return _isReady;
            }
        }

        private bool _isReady;

        [JSInvokable]
        public void OnReady() {
            _isReady = true;
            ForControls((c) => c.OnReady());
        }

        private void ForControls(Action<BaseRendererControl> act)
        {
            List<string> toRemove = null;
            foreach (var controlKey in _controlsMap.Keys)
            {
                var control = _controlsMap[controlKey];
                BaseRendererControl target;
                if (control.TryGetTarget(out target)) {
                    target.OnReady();
                } else {
                    if (toRemove == null)
                    {
                        toRemove = new List<string>();
                        toRemove.Add(controlKey);
                    }
                }
            }

            if (toRemove != null)
            {
                foreach (var key in toRemove)
                {
                    _controlsMap.Remove(key);
                }
            }
        }

        private BaseRendererControl GetControl(string key)
        {
            if (_controlsMap.ContainsKey(key))
            {
                var control = _controlsMap[key];
                BaseRendererControl target;
                if (control.TryGetTarget(out target))
                {
                    return target;
                }
                else
                {
                    _controlsMap.Remove(key);
                    return null;
                }
            }
            return null;
        }

        private Dictionary<string, WeakReference<BaseRendererControl>> _controlsMap = new Dictionary<string, WeakReference<BaseRendererControl>>();
        public void Register(BaseRendererControl control)
        {
            _controlsMap.Add(control.ContainerId, new WeakReference<BaseRendererControl>(control));
        }

        [JSInvokable]
        public void OnInvokeReturn(string containerId, long invokeId, object returnValue)
        {
            var control = GetControl(containerId);
            if (control != null)
            {
                control.OnInvokeReturn(invokeId, returnValue);
            }
        }

        [JSInvokable]
        public void OnRaiseEvent(string containerId, string name, string propertyName, string args) {
            var control = GetControl(containerId);
            //Console.WriteLine("raising event");
            if (control != null)
            {
                //Console.WriteLine("found target");
                control.OnRaiseEvent(name, propertyName, args);
            }
        }

        [JSInvokable]
        public void AdjustDynamicContent(string containerId, string contentType, string templateId, string contentId, string actionType, string args) {
            var control = GetControl(containerId);
            //Console.WriteLine("raising event");
            if (control != null)
            {
                //Console.WriteLine("found target");
                control.AdjustDynamicContent(containerId, contentType, templateId, contentId, actionType, args);
            }
        }

        [JSInvokable]
        public void AdjustDynamicContentBatch(string containerId, string batch) {
            var control = GetControl(containerId);
            if (control != null)
            {
                var arr = control.DeserializeDictionaryArray(batch);
                for (var i = 0; i < arr.Length; i++)
                {
                    var item = arr[i];
                    string currContainer = item.ContainsKey("containerId") ? (string)item["containerId"].ToString() : null;
                    string contentType = item.ContainsKey("contentType") ? (string)item["contentType"].ToString() : null;
                    string templateId = item.ContainsKey("templateId") ? (string)item["templateId"].ToString() : null;
                    string contentId = item.ContainsKey("contentId") ? (string)item["contentId"].ToString() : null;
                    string actionType = item.ContainsKey("actionType") ? (string)item["actionType"].ToString() : null;
                    string args = item.ContainsKey("args") ? (item["args"] != null ? item["args"].ToString() : null) : null;
                    //Console.WriteLine("raising event");
                    if (currContainer != null)
                    {
                        var currControl = GetControl(currContainer);
                        //Console.WriteLine("found target");
                        currControl.AdjustDynamicContent(containerId, contentType, templateId, contentId, actionType, args);
                    }
                }
            }
        }
    }
}
