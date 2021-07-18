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

    public class VesselInfoController : ControllerBase
    {
        ILogger _logger;
        IDataAccess _dataAccess;

        public VesselInfoController(ILogger<VesselInfoController> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        [HttpPost("SaveBasicVesselInfo")]
        public async Task<bool> SaveBasicVesselInfo(List<ApiModels.VesselBasicInfo> vesselBasicInfos)
        {
            bool success = true;

            try
            {
                success = await _dataAccess.SaveVesselBasicInfo(vesselBasicInfos);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return success;
        }

        [HttpGet("GetAllVesselInfo")]
        public IEnumerable<VesselInfo> GetAllVesselInfo()
        {
            return _dataAccess.GetVesselInfos();
        }

        [HttpGet("GetVesselInfo")]
        public VesselInfo GetVesselInfo(long Id)
        {
            return _dataAccess.GetVesselInfo(Id);
        }
    }
}
