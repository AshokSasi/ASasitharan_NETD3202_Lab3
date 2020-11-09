/*Name:Ashok Sasitharan
 * Student ID: 100745484
 * Date: November 9 2020
 * NETD 3202 Lab 3
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace ASasitharan_NETD3202_Lab3
{
    class CommonShare:shares
    {
        //variables
        const int commonPrice = 42;
        const int votingPower = 1;
        //constructor
        public CommonShare(string name, string date, int numOfShares,string shareType):base(name,date,numOfShares,shareType)
        {
            this.buyerName = base.buyerName;
            this.buyDate = base.buyDate;
            this.numShares = base.numShares;
            this.shareType = base.shareType;
          
        }

        //getters for vote power and share price of common shares
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
