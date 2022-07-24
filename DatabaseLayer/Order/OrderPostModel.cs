using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Order
{
    public class OrderPostModel
    {

        public string CartId { get; set; }

        public string userId { get; set; }

        public string addressID { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
