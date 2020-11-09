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
    class shares
    {
        //data members
        protected string buyerName;
        protected string buyDate;
        protected int numShares;
        protected string shareType;

        //getters and setters
        public string BuyerName
        {
            get { return this.buyerName; }
            set { this.buyerName = value; }
        }
        public string BuyDate
        {
            get { return this.buyDate; }
            set { this.buyDate = value; }
        }
        public int NumShares
        {
            get { return this.numShares; }
            set { this.numShares = value; }
        }
        public string ShareType
        {
            get { return this.shareType; }
            set { this.shareType = value; }
        }

        //parameterized constructor
        public shares(string name, string date,int numOfShares, string shareType)
        {
            this.buyerName = name;
            this.buyDate = date;
            this.numShares = numOfShares;
            this.shareType = shareType;
        }
       
     
    }
}
