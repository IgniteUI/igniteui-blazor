using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure
{
    public class BlazorApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly Action<IWebHostBuilder>? configureWebHost;
        private IHost? host;

        public string ServerAddress
        {
            get
            {
                EnsureServer();
                return ClientOptions.BaseAddress.ToString();
            }
        }

        public BlazorApplicationFactory()
        {
        }

        public BlazorApplicationFactory(Action<IWebHostBuilder> configureWebHost)
        {
            this.configureWebHost = configureWebHost;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            configureWebHost?.Invoke(builder);

            // Setting port to 0 means that Kestrel will pick any free a port.
            // but we don't want freedom, just use to use same port
            builder.UseUrls("http://127.0.0.1:5249");
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Create the host for TestServer now before we modify the builder to use Kestrel instead.
            var testHost = builder.Build();

            // Modify the host builder to use Kestrel instead of TestServer so we can listen on a real address.    
            // configure and start the actual host using Kestrel.
            builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

            // Create and start the Kestrel server before the test server,  
            // otherwise due to the way the deferred host builder works    
            // for minimal hosting, the server will not get "initialized    
            // enough" for the address it is listening on to be available.    
            // See https://github.com/dotnet/aspnetcore/issues/33846.    
            host = builder.Build();
            host.Start();

            // Extract the selected dynamic port out of the Kestrel server  
            // and assign it onto the client options for convenience so it    
            // "just works" as otherwise it'll be the default http://localhost    
            // URL, which won't route to the Kestrel-hosted HTTP server.     
            var server = host.Services.GetRequiredService<IServer>();
            var addresses = server.Features.Get<IServerAddressesFeature>();
            ClientOptions.BaseAddress = addresses!.Addresses.Select(x => new Uri(x)).Last();

            // Return the host that uses TestServer, rather than the real one.  
            // Otherwise the internals will complain about the host's server    
            // not being an instance of the concrete type TestServer.    
            // See https://github.com/dotnet/aspnetcore/pull/34702.   
            testHost.Start();
            return testHost;
        }

        private void EnsureServer()
        {
            if (host is null)
            {
                // This forces WebApplicationFactory to bootstrap the server  
                try
                {
                    using var _ = CreateDefaultClient();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            host?.Dispose();
        }
    }
}
