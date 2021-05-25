using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class IdentityNumber : IEntity
    {
        public int id { get; set; }
        public long tc { get; set; }
    }
}
