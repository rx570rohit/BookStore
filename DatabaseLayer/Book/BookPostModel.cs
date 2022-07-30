﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Book
{
    public class BookPostModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public decimal Rating { get; set; }
        public int totalRating { get; set; }
        public int DiscountPrice { get; set; }
        public int ActualPrice { get; set; }
        public string Description { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }
    }
}
