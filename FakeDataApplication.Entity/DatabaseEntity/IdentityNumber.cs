using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class IdentityNumber : IEntity
    {
        public int id { get; set; }
        public string tc { get; set; }
    }
}
