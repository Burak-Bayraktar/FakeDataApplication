using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class TelephoneBrand : IEntity
    {
        public int id { get; set; }
        public string telephone_brand { get; set; }
    }
}
