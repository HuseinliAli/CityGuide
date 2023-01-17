using CityGuideAPI.Models;
using System.Collections.Generic;

namespace CityGuideAPI.DataAccess
{
    public interface IEntityRepository
    {
        void Add<T>(T entity);
        void Delete<T>(T entity);
        bool SaveAll();

        List<City> GetCities();
        List<Photo> GetPhotosByCityId(int id);
        City GetCityById(int id);
        Photo GetPhotoById(int id);
    }
}
