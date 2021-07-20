using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace WSFSVesselLocation
{
    public class DataAccess : IDataAccess
    {
        ILogger _logger;
        VesselInfoContext _vesselInfoContext;

        public DataAccess(ILogger<DataAccess> logger, VesselInfoContext vesselInfoContext)
        {
            _logger = logger;
            _vesselInfoContext = vesselInfoContext;
        }

        /// <summary>
        /// This takes an array of basic vessel info in the shape of ApiModels.VesselBasicInfo
        /// from the WSFS API http://www.wsdot.wa.gov/Ferries/API and saves to db
        /// </summary>
        /// <param name="vesselBasicInfoApiResponse">json string</param>
        /// <returns></returns>
        public async Task<bool> SaveVesselBasicInfo(List<ApiModels.VesselBasicInfo> vesselBasicInfos)
        {
            bool success = true;

            try
            {
                foreach (var vbi in vesselBasicInfos)
                {
                    var existingId = _vesselInfoContext.VesselInfos.FirstOrDefault(x => x.Id == vbi.VesselID);

                    if (existingId != null) //update
                    {
                        existingId.Name = vbi.VesselName;
                        existingId.Status = vbi.Status;
                    }
                    else //insert
                    {
                        _vesselInfoContext.VesselInfos.Add(new VesselInfo { Id = vbi.VesselID, Name = vbi.VesselName, Status = vbi.Status });
                    }
                }

                await _vesselInfoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                success = false;
            }

            return success;
        }

        public IEnumerable<VesselInfo> GetVesselInfos()
        {
            try
            {
                return _vesselInfoContext.VesselInfos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<VesselInfo>();
            }
        }

        public VesselInfo GetVesselInfo(long Id)
        {
            try
            {
                return _vesselInfoContext.VesselInfos.FirstOrDefault(v => v.Id == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new VesselInfo();
            }
        }
    }
}
