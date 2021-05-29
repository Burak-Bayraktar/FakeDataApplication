using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class District : IEntity
    {
        public int id { get; set; }

        public string district { get; set; }
    }
}
