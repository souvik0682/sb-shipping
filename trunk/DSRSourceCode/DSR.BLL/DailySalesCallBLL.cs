using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.DAL;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities;
using System.Web;
using DSR.Utilities.ResourceManager;
using DSR.Utilities.Cryptography;

namespace DSR.BLL
{
    public class DailySalesCallBLL
    {
        public int ValidateDestination(string portCode)
        {
            return DailySalesCallDAL.ValidateDestination(portCode);
        }

        public int SaveDailySalesCallEntry(IDailySalesCall dailySalesCall, List<ICommitment> commitments)
        {
            int callId = 0;
            int commitmentId = 0;

            callId = DailySalesCallDAL.SaveDailySalesCall(dailySalesCall);

            if (callId > 0)
            {
                if (!ReferenceEquals(commitments, null))
                {
                    foreach (ICommitment commitment in commitments)
                    {
                        commitment.CallId = callId;
                        commitmentId = DailySalesCallDAL.SaveCommitment(commitment);
                    }
                }
            }

            return callId;
        }

        public IDailySalesCall GetDailySalesCallEntry(int callId)
        {
            return DailySalesCallDAL.GetDailySalesCallEntry(callId);
        }

        public List<ICommitment> GetCommitments(int callId)
        {
            return DailySalesCallDAL.GetCommitments(callId);
        }
    }
}
