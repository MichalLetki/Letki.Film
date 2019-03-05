using Core;
using Interfaces;

namespace DAOMock1.BO
{
    public class Film : IFilm
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProductionYear { get; set; }
        public IProducer Producer { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} - {ProductionYear} [ {Producer.Name} ] {Category} ";
        }
    }
}
