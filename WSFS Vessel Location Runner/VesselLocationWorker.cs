using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WSFS_Vessel_Location_Runner
{
    public class VesselLocationWorker : BackgroundService
    {
        private readonly ILogger<VesselLocationWorker> _logger;
        private IConfiguration _config;

        string apiUrl = string.Empty;
        string apiKey = string.Empty;
        string saveLocationUrl = string.Empty;

        public VesselLocationWorker(IConfiguration configuration,ILogger<VesselLocationWorker> logger)
        {
            _config = configuration;
            _logger = logger;

            apiUrl = _config.GetValue<string>("wsfsApiUrl");
            apiKey = _config.GetValue<string>("wsfsApiKey");
            saveLocationUrl = _config.GetValue<string>("saveLocationUrl");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int delay = _config.GetValue<int>("locationWorkerDelaySec") * 1000;

            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiUrl))
            {
                _logger.LogError("API key or API url is missing or empty. Aborting Vessel Location Worker.");
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //call WSFS and get location data
                    var client = new HttpClient();
                    var url = $"{apiUrl}/Vessels/rest/vessellocations?apiaccesscode={apiKey}";
                    var result = await client.GetStringAsync(url);

                    //post to our vessel location service to save
                    var saveResult = await client.PostAsync(saveLocationUrl, new StringContent(result, Encoding.UTF8, "application/json"));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
