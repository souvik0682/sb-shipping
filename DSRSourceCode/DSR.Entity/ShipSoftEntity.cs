using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;
using System.Xml.Serialization;

namespace DSR.Entity
{
    [Serializable]
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

        public string LocationName
        {
            get;
            set;
        }

        public int ProspectId
        {
            get;
            set;
        }

        public string ProspectName
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

        public string PortName
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

        public int? CustomerId
        {
            get;
            set;
        }

        public string CustomerName
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
            this.TranId = Convert.ToInt32(reader["TranID"]);
            this.LocationId = Convert.ToInt32(reader["LocId"]);
            this.LocationName = Convert.ToString(reader["LocName"]);
            this.ProspectId = Convert.ToInt32(reader["ProspectId"]);
            this.ProspectName = Convert.ToString(reader["ProspectName"]);
            this.BookingNo = Convert.ToString(reader["BookingNo"]);
            this.BLANumber = Convert.ToString(reader["BLNumber"]);
            this.VesselVoyage = Convert.ToString(reader["VesselVoyage"]);
            this.ShipperName = Convert.ToString(reader["ShipperName"]);
            this.PortId = Convert.ToInt32(reader["PortId"]);
            this.PortName = Convert.ToString(reader["PortName"]);
            this.TEU = Convert.ToInt32(reader["TEU"]);
            this.FEU = Convert.ToInt32(reader["FEU"]);
            this.SOBDate = Convert.ToDateTime(reader["SOBDate"]);

            if (reader["CustId"] != DBNull.Value)
            {
                this.CustomerId = Convert.ToInt32(reader["CustId"]);
                this.CustomerName = Convert.ToString(reader["CustName"]);
            }
        }

        #endregion
    }
}
