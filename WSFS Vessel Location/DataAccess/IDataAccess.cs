using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WSFSVesselLocation
{
    public interface IDataAccess
    {
        public Task<bool> SaveVesselLocationInfo(List<ApiModels.VesselLocationInfo> vesselBasicInfos);
        public IEnumerable<VesselLocation> GetAllVesselLocations();
        public VesselLocation GetVesselLocation(long Id);
    }
}
