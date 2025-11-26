using System;
using IgniteUI.Blazor.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfragisticsBlazorExtensions 
    {

        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddIgniteUIBlazor(this Microsoft.Extensions.DependencyInjection.IServiceCollection collection,
            params Type[] modulesToLoad)
        {
            var s = collection.AddScoped(
                typeof(IIgniteUIBlazorSettings), 
                (sp) => {
                    var bs = new IgniteUIBlazorSettings();
                    bs = bs.WithModulesToLoad(modulesToLoad != null && modulesToLoad.Length > 0 ? new ReadOnlyCollection<Type>(modulesToLoad) : null);
                    return bs;
                });

            return s.AddScoped(
                typeof(IIgniteUIBlazor), 
                typeof(IgniteUIBlazor));
        }

        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddIgniteUIBlazor(this Microsoft.Extensions.DependencyInjection.IServiceCollection collection,
            IIgniteUIBlazorSettings settings,
            params Type[] modulesToLoad)
        {
            var s = collection.AddScoped(
                typeof(IIgniteUIBlazorSettings), 
                (sp) => {
                    var bs = new IgniteUIBlazorSettings(settings);
                    bs = bs.WithModulesToLoad(modulesToLoad != null && modulesToLoad.Length > 0 ? new ReadOnlyCollection<Type>(modulesToLoad) : null);
                    return bs;
                });

            return s.AddScoped(
                typeof(IIgniteUIBlazor), 
                typeof(IgniteUIBlazor));
        } 
    }
 
}