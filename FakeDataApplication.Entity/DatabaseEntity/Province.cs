using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Province : IEntity
    {
        public int id { get; set; }
        public string province { get; set; }
    }
}
