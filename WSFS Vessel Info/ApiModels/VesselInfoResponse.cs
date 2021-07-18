using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels
{
    public class VesselBasicInfo
    {
        public int VesselID { get; set; }
        public int VesselSubjectID { get; set; }
        public string VesselName { get; set; }
        public string VesselAbbrev { get; set; }
        public VesselClassInfo Class { get; set; }
        public int Status { get; set; }
        public bool OwnedByWSF { get; set; }
    }

    public class VesselClassInfo
    {
        public int ClassID { get; set; }
        public int ClassSubjectID { get; set; }
        public string ClassName { get; set; }
        public int SortSeq { get; set; }
        public string DrawingImg { get; set; }
        public string SilhouetteImg { get; set; }
        public string PublicDisplayName { get; set; }
    }
}
