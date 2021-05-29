using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Sentence : IEntity
    {
        public int id { get; set; }
        public string verb { get; set; }
        public string pronoun { get; set; }
        public string adjective { get; set; }
        public string conjunction { get; set; }
        public string preposition { get; set; }
        public string envelope { get; set; }
    }
}
