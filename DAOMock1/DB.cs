using Interfaces;
using System.Collections.Generic;
using Core;
using DAOMock1.BO;

namespace DAOMock1
{
    public class DB : IDAO
    {
        private readonly List<IProducer> _producers;
        private readonly List<IFilm> _films;

        public DB()
        {
            _producers = new List<IProducer>
            {
                new Producent{ ID=1, Name="Netflix", Founded=1999 },
                new Producent{ ID=2, Name="Dreamworks", Founded=1920 },
                new Producent{ ID=3, Name="The Walt Disney Company", Founded=1930}
            };

            _films = new List<IFilm>
            {
                new Film
                {
                    ID =1,
                    Name ="Budapeszt",
                    Producer = _producers[0],
                    ProductionYear =2010,
                    Category =Category.Komedia
                },
                new Film
                {
                    ID =2,
                    Name ="Jak Romantycznie",
                    Producer = _producers[0],
                    ProductionYear =2009,
                    Category =Category.Thriller
                },
                new Film
                {
                    ID =3,
                    Name ="Transformers",
                    Producer = _producers[1],
                    ProductionYear =2000,
                    Category =Category.Komedia
                },
                new Film
                {
                    ID =4,
                    Name ="Madagaskar",
                    Producer = _producers[1],
                    ProductionYear = 2007,
                    Category =Category.Thriller
                },
                new Film
                {
                    ID =5,
                    Name ="Toy Story",
                    Producer = _producers[2],
                    ProductionYear =2002,
                    Category =Category.Komedia
                }


            };
        }

        public IFilm CreateEmptyFilm()
        {
            return new BO.Film();
        }

        public IEnumerable<IFilm> GetAllFilm()
        {
            return _films;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }
    }
}
