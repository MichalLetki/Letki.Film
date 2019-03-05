using Core;
using FilmCatalog.Bindable;
using Interfaces;

namespace FilmCatalog.ViewModel
{
    internal class FilmViewModel : BindableBase, IFilm
    {

        public FilmViewModel(IFilm film)
        {
            _film = film;
        }

        private readonly IFilm _film;

        public int ID
        {
            get => _film.ID;
            set
            {
                _film.ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string Name
        {
            get => _film.Name;
            set
            {
                _film.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int ProductionYear
        {
            get => _film.ProductionYear;
            set
            {
                _film.ProductionYear = value;
                OnPropertyChanged(nameof(ProductionYear));
            }
        }

        public IProducer Producer
        {
            get => _film.Producer;
            set
            {
                _film.Producer = value;
                OnPropertyChanged(nameof(Producer));
            }
        }

        public Category Category
        {
            get => _film.Category;
            set
            {
                _film.Category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
    }
}
