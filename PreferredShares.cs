using System;
using System.Collections.Generic;
using System.Text;

namespace ASasitharan_NETD3202_Lab3
{
    class PreferredShares:shares
    {
        const int PreferredPrice = 100;
        const int votingPower = 10;

        public PreferredShares(string name, string date, int numOfShares,string shareType) : base(name, date, numOfShares, shareType)
        {
            this.buyerName = base.buyerName;
            this.buyDate = base.buyDate;
            this.numShares = base.numShares;
            this.shareType = base.shareType;
        }
        public int SharePrice
        {
            get { return PreferredPrice; }

        }
        public int VotePower
        {
            get { return votingPower; }

        }
    }
}
