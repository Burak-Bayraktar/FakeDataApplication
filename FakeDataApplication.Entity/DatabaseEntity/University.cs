using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class University : IEntity
    {
        public int id { get; set; }
        public string university { get; set; }
    }
}
