using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class BookAuthor : IEntity
    {
        public int id { get; set; }
        public string book_name { get; set; }
        public string author { get; set; }
    }
}
