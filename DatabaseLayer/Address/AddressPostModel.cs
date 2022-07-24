using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Address
{
    public class AddressPostModel
    {
        public string fullAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public double pinCode { get; set; }
    }
}
