using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
    }
}
