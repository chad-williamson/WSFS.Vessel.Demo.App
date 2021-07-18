using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WSFS_Vessel_Info_Worker
{
    public class VesselInfoWorker : BackgroundService
    {
        private readonly ILogger<VesselInfoWorker> _logger;
        private IConfiguration _config;

        string apiUrl = string.Empty;
        string apiKey = string.Empty;
        string saveInfoUrl = string.Empty;

        public VesselInfoWorker(IConfiguration configuration, ILogger<VesselInfoWorker> logger)
        {
            _config = configuration;
            _logger = logger;

            apiUrl = _config.GetValue<string>("wsfsApiUrl");
            apiKey = _config.GetValue<string>("wsfsApiKey");
            saveInfoUrl = _config.GetValue<string>("saveInfoUrl");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int delay = _config.GetValue<int>("infoWorkerDelaySec") * 1000;

            if(string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiUrl))
            {
                _logger.LogError("API key or API url is missing or empty. Aborting Vessel Info Worker.");
                return;
            }
            
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //call WSFS to get our vessel basic info
                    var client = new HttpClient();
                    var url = $"{apiUrl}/Vessels/rest/vesselbasics?apiaccesscode={apiKey}";
                    var result = await client.GetStringAsync(url);

                    //post to our vessel info service to save the data
                    var saveResult = await client.PostAsync(saveInfoUrl, new StringContent(result, Encoding.UTF8, "application/json"));

                    await Task.Delay(delay, stoppingToken);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }
    }
}
