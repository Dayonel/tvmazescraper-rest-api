using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TvMaze.Core.Settings;

namespace TvMaze.Core.Handlers
{
    public class ThrottlingDelegateHandler : DelegatingHandler
    {
        private SemaphoreSlim _throttler;
        private readonly RateLimitSettings _rateLimitSettings;
        public ThrottlingDelegateHandler(RateLimitSettings rateLimitSettings)
        {
            _throttler = new SemaphoreSlim(1, 1);
            _rateLimitSettings = rateLimitSettings;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (_rateLimitSettings == null)
                throw new ArgumentNullException(nameof(_rateLimitSettings));

            if (_rateLimitSettings.CallsPerSecond == default)
                throw new ArgumentNullException(nameof(_rateLimitSettings.CallsPerSecond), 
                    $"{nameof(_rateLimitSettings.CallsPerSecond)} must be greater than 0.");

            await _throttler.WaitAsync(cancellationToken);
            try
            {
                var result = await base.SendAsync(request, cancellationToken);
                await Task.Delay(TimeSpan.FromSeconds((double)1 / _rateLimitSettings.CallsPerSecond));

                return result;
            }
            finally
            {
                _throttler.Release();
            }
        }
    }
}
