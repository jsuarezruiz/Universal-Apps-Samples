using Ejemplo_JumpList.Models;
using Ejemplo_JumpList.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace Ejemplo_JumpList.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private List<MoviesByYear> _movies;

        public List<MoviesByYear> Movies
        {
            get { return _movies; }
            set { _movies = value; }
        }

        public MainViewModel()
        {
            var movies = new List<Movie>
                             {    
                                 new Movie {Title = "John Carter", Year = 2012, Image = "ms-appx:///Assets/Carter.jpg"},
                                 new Movie {Title = "El caballero oscuro: La leyenda renace", Year = 2012, Image = "ms-appx:///Assets/Batman.jpg"},
                                 new Movie {Title = "Cisne Negro", Year = 2011, Image = "ms-appx:///Assets/Cisne.jpg"},
                                 new Movie {Title = "Drive", Year = 2011, Image = "ms-appx:///Assets/Drive.jpg"},
                                 new Movie {Title = "Toy Story 3", Year = 2010, Image = "ms-appx:///Assets/Toy.jpg"},
                                 new Movie {Title = "El discurso del rey", Year = 2010, Image = "ms-appx:///Assets/Rey.jpg"},
                                 new Movie {Title = "Origen", Year = 2010, Image = "ms-appx:///Assets/Origen.jpg"},    
                                 new Movie {Title = "Avatar", Year = 2009, Image = "ms-appx:///Assets/Avatar.jpg"}
                             };

            var moviesByYear = movies.GroupBy(f => f.Year).Select(f => new MoviesByYear { Year = f.Key, Movies = f.ToList() });

            Movies = moviesByYear.ToList();
        }

        public override System.Threading.Tasks.Task OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            return null;
        }

        public override System.Threading.Tasks.Task OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            return null;
        }
    }
}
