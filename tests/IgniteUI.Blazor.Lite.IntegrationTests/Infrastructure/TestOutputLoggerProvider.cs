using Microsoft.Extensions.Logging;

namespace IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Routes host logs into the output of the test that is running. Writing them to the
    /// process console instead attributes them to whatever test the runner happens to be
    /// reporting when the host emits them.
    /// </summary>
    public sealed class TestOutputLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new TestOutputLogger(categoryName);

        public void Dispose()
        {
        }

        private sealed class TestOutputLogger(string category) : ILogger
        {
            public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(
                LogLevel logLevel,
                EventId eventId,
                TState state,
                Exception? exception,
                Func<TState, Exception?, string> formatter)
            {
                var message = $"[host {logLevel}] {category}: {formatter(state, exception)}";
                if (exception != null)
                {
                    message += Environment.NewLine + exception;
                }

                // Out is attributed to the current test but only surfaced when it fails;
                // Progress is always shown, so errors go to both.
                TestContext.Out.WriteLine(message);
                if (logLevel >= LogLevel.Error)
                {
                    TestContext.Progress.WriteLine(message);
                }
            }
        }
    }
}
