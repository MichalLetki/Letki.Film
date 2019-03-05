using FilmCatalog.Bindable;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Core;

namespace FilmCatalog.ViewModel
{
    internal class FilmListViewModel : BindableBase
    {
        private BLC.BLC _blc;
        private readonly ObservableCollection<IProducer> _producers;


        private ObservableCollection<IFilm> _films;
        private ObservableCollection<IFilm> _allFilms;
        private FilmViewModel _selectedFilm;
        private string _nameFilm;
        private IProducer _selectedSearchProducer;
        private Category _selectedSearchCategory;


        public ObservableCollection<IFilm> Films
        {
            get => _films;
            set
            {
                _films = value;
                OnPropertyChanged(nameof(Films));
            }
        }

        public IEnumerable<Category> Categories =>
            Enum.GetValues(typeof(Category)).Cast<Category>();

        public string NameFilm
        {
            get => _nameFilm;
            set
            {
                _nameFilm = value;
                SearchCriteria();
                OnPropertyChanged(nameof(Films));
            }
        }

        public IProducer SelectedSearchProducer
        {
            get => _selectedSearchProducer;
            set
            {
                _selectedSearchProducer = value;
                SearchCriteria();
                OnPropertyChanged(nameof(Films));
            }
        }

        public Category SelectedSearchCategory
        {
            get => _selectedSearchCategory;
            set
            {
                _selectedSearchCategory = value;
                SearchCriteria();
                OnPropertyChanged(nameof(Films));
            }
        }

        public FilmListViewModel()
        {
            _films = new ObservableCollection<IFilm>();
            _allFilms = new ObservableCollection<IFilm>();

            _selectedSearchCategory = Category.Wszystkie;
            LoadMock();
            GetAllFilm();
            InitializeCommand();

            _producers = new ObservableCollection<IProducer>(_blc.GetProducers());
        }

        private void LoadMock()
        {
            try
            {
                var sett = new CarCatalog.Properties.Settings();
                _blc = new BLC.BLC(sett.DBNameConf);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(-1);
            }
        }

        private void InitializeCommand()
        {
            _createEmptyFilmCommand = new RelayCommand(CreateEmptyFilm);
            _deleteFilmCommand = new RelayCommand(DeleteFilm);

        }

        private void DeleteFilm(object obj)
        {
            var tmp = _allFilms.FirstOrDefault(x => x.ID == _selectedFilm?.ID);
            _allFilms.Remove(tmp);
            _films.Remove(_selectedFilm);

        }

        private void GetAllFilm()
        {
            foreach (var c in _blc.GetFilm())
            {
                _films.Add(new FilmViewModel(c));
                _allFilms.Add(new FilmViewModel(c));
            }
        }

        private void CreateEmptyFilm(object o)
        {
            var film = _blc.CreateEmptyFilm();
            film.ID = _allFilms.OrderByDescending(x => x.ID).FirstOrDefault()?.ID + 1 ?? 1;
            var cvm = new FilmViewModel(film);
            _films.Add(cvm);
            _allFilms.Add(cvm);
            SelectedFilm = cvm;
        }

        private RelayCommand _createEmptyFilmCommand;
        private RelayCommand _deleteFilmCommand;


        public RelayCommand CreateEmptyFilmCommand => _createEmptyFilmCommand;
        public RelayCommand DeleteFilmCommand => _deleteFilmCommand;




        public FilmViewModel SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }


        public ObservableCollection<IProducer> Producers => _producers;

        private void SearchCriteria()
        {
            Films = new ObservableCollection<IFilm>(_allFilms.Where(x =>
                (_nameFilm == null || x.Name == null ||  _nameFilm.Length == 0 || x.Name.ToLower().StartsWith(_nameFilm.ToLower()))
                && (_selectedSearchProducer == null || x.Producer.ID == _selectedSearchProducer.ID)
                && (_selectedSearchCategory == Category.Wszystkie || x.Category == _selectedSearchCategory)
            ));
        }
    }
}
