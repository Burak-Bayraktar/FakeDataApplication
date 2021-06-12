using FakeDataApplication.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FakeDataApplication.Business
{
    public interface IFluentBase
    {
        string CreateAsJSON(string fileFolder);
        void CreateAsXML(string fileFolder);
    }
}
