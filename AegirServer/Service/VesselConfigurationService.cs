using AegirDataTypes.Vessel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Service
{
    public class VesselConfigurationService
    {
        public static List<VesselConfiguration> Vessels = new List<VesselConfiguration> {
            new VesselConfiguration("Tug of Power",40,60),
            new VesselConfiguration("Luxury Liner", 95, 300),
            new VesselConfiguration("Carrier of Consumables", 80, 250),
            new VesselConfiguration("Big Buoyant Boat", 120, 500)
        };
        public VesselConfigurationService()
        {

        }

    }
}
