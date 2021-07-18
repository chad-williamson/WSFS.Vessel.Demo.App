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
        public Task<bool> SaveVesselBasicInfo(List<ApiModels.VesselBasicInfo> vesselBasicInfos);
        public IEnumerable<VesselInfo> GetVesselInfos();
        public VesselInfo GetVesselInfo(long Id);
    }
}
