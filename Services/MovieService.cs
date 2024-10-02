using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class MovieService
    {
        public void Add(Movie movie)
        {
            UnitOfWork.Instance.movieRepository.Add(movie);
        }

        public void Update(Movie movie)
        {
            Movie movie1 = SearchByName(movie.Name);
            movie1.Status = movie.Status;
            UnitOfWork.Instance.movieRepository.Update(movie);
        }

        public void Delete(Movie movie)
        {
            UnitOfWork.Instance.movieRepository.Delete(movie);
        }

        public Movie SearchById(string Id) 
        {
            return UnitOfWork.Instance.movieRepository.Get(Id);
        }

        public Movie SearchByName(string Name)
        {
            foreach (var item in UnitOfWork.Instance.movieRepository.Gets())
                if (item.Name.ToLower().CompareTo(Name.ToLower()) == 0)
                    return item;
            return null;
        }

        public bool isExist(string Name)
        {
            if (SearchByName(Name) == null)
                return false;
            return true;
        }

        public List<Movie> Gets()
        {
            return UnitOfWork.Instance.movieRepository.Gets();
        }
    }
}
