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
    class PreferredShares:shares
    {
        //variables
        const int PreferredPrice = 100;
        const int votingPower = 10;

        //constructor
        public PreferredShares(string name, string date, int numOfShares,string shareType) : base(name, date, numOfShares, shareType)
        {
            this.buyerName = base.buyerName;
            this.buyDate = base.buyDate;
            this.numShares = base.numShares;
            this.shareType = base.shareType;
        }
        //getters for vote power and share price for preferred shares.
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
