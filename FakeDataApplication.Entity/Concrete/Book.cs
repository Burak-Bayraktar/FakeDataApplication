using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity.Concrete
{
    public class Book : IEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
    }
}
