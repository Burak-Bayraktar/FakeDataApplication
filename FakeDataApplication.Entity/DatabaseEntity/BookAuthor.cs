using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class BookAuthor : IEntity
    {
        public int id { get; set; }
        public string book { get; set; }
        public string book_author { get; set; }
    }
}
