using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;

namespace WSFSVesselLocation
{
    public class DataAccess : IDataAccess
    {
        ILogger _logger;
        VesselLocationContext _vesselLocationContext;

        public DataAccess(ILogger<DataAccess> logger, VesselLocationContext vesselLocationContext)
        {
            _logger = logger;
            _vesselLocationContext = vesselLocationContext;
        }

        /// <summary>
        /// This takes an array of basic vessel info in the shape of ApiModels.VesselBasicInfo
        /// from the WSFS API http://www.wsdot.wa.gov/Ferries/API and saves to db
        /// </summary>
        /// <param name="vesselBasicInfoApiResponse">json string</param>
        /// <returns></returns>
        public async Task<bool> SaveVesselLocationInfo(List<ApiModels.VesselLocationInfo> vesselLocationInfos)
        {
            bool success = true;

            try
            {
                foreach (var vli in vesselLocationInfos)
                {
                    var existingId = _vesselLocationContext.VesselLocations.FirstOrDefault(x => x.Id == vli.VesselID);

                    if (existingId != null) //update
                    {
                        existingId.Heading = vli.Heading;
                        existingId.Speed = vli.Speed;
                        existingId.Latitude = vli.Latitude;
                        existingId.Longitude = vli.Longitude;
                    }
                    else //insert
                    {
                        _vesselLocationContext.VesselLocations.Add(new VesselLocation { Id = vli.VesselID, Heading = vli.Heading, Latitude = vli.Latitude, Longitude = vli.Longitude });
                    }

                    await _vesselLocationContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                success = false;
            }

            return success;
        }

        public IEnumerable<VesselLocation> GetAllVesselLocations()
        {
            try
            {
                return _vesselLocationContext.VesselLocations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<VesselLocation>();
            }
        }

        VesselLocation IDataAccess.GetVesselLocation(long Id)
        {
            return _vesselLocationContext.VesselLocations.FirstOrDefault(v => v.Id == Id);
        }
    }
}
