using FakeDataApplication.Entity.Abstract;

namespace FakeDataApplication.Entity
{
    public class TechnologicalDevice : IEntity
    {
        public string Laptop { get; set; }
        public string Tablet { get; set; }
        public string Telephone { get; set; }
        public string Television { get; set; }
    }
}