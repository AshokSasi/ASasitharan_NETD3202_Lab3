using System;
using System.Collections.Generic;
using System.Text;

namespace ASasitharan_NETD3202_Lab3
{
    class CommonShare:shares
    {
        const int commonPrice = 42;
        const int votingPower = 1;
        public CommonShare(string name, string date, int numOfShares,string shareType):base(name,date,numOfShares,shareType)
        {
            this.buyerName = base.buyerName;
            this.buyDate = base.buyDate;
            this.numShares = base.numShares;
            this.shareType = base.shareType;
          
        }

        public int VotePower
        {
            get { return votingPower; }
  
        }
        public int SharePrice
        {
            get { return commonPrice; }

        }
    }
}
