using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IShipSoft
    {
        int TranId { get; set; }
        int LocationId { get; set; }
        int ProspectId { get; set; }
        string BookingNo { get; set; }
        string BLANumber { get; set; }
        string VesselVoyage { get; set; }
        string ShipperName { get; set; }
        int PortId { get; set; }
        int? TEU { get; set; }
        int? FEU { get; set; }
        DateTime? SOBDate { get; set; }
        int CustomerId { get; set; }
    }
}
