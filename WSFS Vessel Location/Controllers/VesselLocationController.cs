using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WSFSVesselLocation.Controllers
{
    [ApiController]
    [Route("[controller]-v1")]

    public class VesselLocationController : ControllerBase
    {
        ILogger _logger;
        IDataAccess _dataAccess;

        public VesselLocationController(ILogger<VesselLocationController> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        [HttpPost("SaveVesselLocationData")]
        public async Task<bool> SaveVesselLocationData(List<ApiModels.VesselLocationInfo> vesselBasicInfos)
        {
            bool success = true;

            try
            {
                success = await _dataAccess.SaveVesselLocationInfo(vesselBasicInfos);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return success;
        }

        [HttpGet("GetAllVesselLocations")]
        public IEnumerable<VesselLocation> GetAllVesselLocations()
        {
            return _dataAccess.GetAllVesselLocations();
        }

        [HttpGet("GetVesselLocation")]
        public VesselLocation GetVesselLocation(long Id)
        {
            return _dataAccess.GetVesselLocation(Id);
        }
    }
}
