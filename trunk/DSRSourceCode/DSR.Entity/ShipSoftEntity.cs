using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class ShipSoftEntity : IShipSoft
    {
        #region IShipSoft Members

        public int TranId
        {
            get;
            set;
        }

        public int LocationId
        {
            get;
            set;
        }

        public int ProspectId
        {
            get;
            set;
        }

        public string BookingNo
        {
            get;
            set;
        }

        public string BLANumber
        {
            get;
            set;
        }

        public string VesselVoyage
        {
            get;
            set;
        }

        public string ShipperName
        {
            get;
            set;
        }

        public int PortId
        {
            get;
            set;
        }

        public int? TEU
        {
            get;
            set;
        }

        public int? FEU
        {
            get;
            set;
        }

        public DateTime? SOBDate
        {
            get;
            set;
        }

        public int CustomerId
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public ShipSoftEntity()
        {

        }

        public ShipSoftEntity(DataTableReader reader)
        {

        }

        #endregion
    }
}
