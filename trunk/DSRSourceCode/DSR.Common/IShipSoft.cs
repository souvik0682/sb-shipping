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
        string LocationName { get; set; }
        int ProspectId { get; set; }
        string ProspectName { get; set; }
        string BookingNo { get; set; }
        string BLANumber { get; set; }
        string VesselVoyage { get; set; }
        string ShipperName { get; set; }
        int PortId { get; set; }
        string PortName { get; set; }
        int? TEU { get; set; }
        int? FEU { get; set; }
        DateTime? SOBDate { get; set; }
        int? CustomerId { get; set; }
        string CustomerName { get; set; }
    }
}
