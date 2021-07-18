using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels
{
    public class VesselLocationInfo
    {
        public int VesselID { get; set; }
        //public string VesselName { get; set; }
        //public int Mmsi { get; set; }
        //public int DepartingTerminalID { get; set; }
        //public string DepartingTerminalName { get; set; }
        //public string DepartingTerminalAbbrev { get; set; }
        //public int ArrivingTerminalID { get; set; }
        //public string ArrivingTerminalName { get; set; }
        //public string ArrivingTerminalAbbrev { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Speed { get; set; }
        public int Heading { get; set; }
        //public bool InService { get; set; }
        //public bool AtDock { get; set; }
        //public DateTime LeftDock { get; set; }
        //public DateTime Eta { get; set; }
        //public string EtaBasis { get; set; }
        //public DateTime ScheduledDeparture { get; set; }
        //public string[] OpRouteAbbrev { get; set; }
        //public int VesselPositionNum { get; set; }
        //public int SortSeq { get; set; }
        //public int ManagedBy { get; set; }
        //public DateTime TimeStamp { get; set; }
        //public int VesselWatchShutID { get; set; }
        //public string VesselWatchShutMsg { get; set; }
        //public string VesselWatchShutFlag { get; set; }
        //public string VesselWatchStatus { get; set; }
        //public string VesselWatchMsg { get; set; }
    }
}
