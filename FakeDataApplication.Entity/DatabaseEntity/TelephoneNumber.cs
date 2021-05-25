using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class TelephoneNumber : IEntity
    {
        public int id { get; set; }
        public long telephone_number { get; set; }
    }
}
