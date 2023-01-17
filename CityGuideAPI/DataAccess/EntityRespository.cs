using AutoMapper;
using CityGuideAPI.Dtos;
using CityGuideAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityGuideAPI.DataAccess
{
    public class EntityRespository : IEntityRepository
    {
        private DatabaseContext _databaseContext;

        public EntityRespository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add<T>(T entity)
        {
            _databaseContext.Add(entity);
        }

        public void Delete<T>(T entity)
        {
            _databaseContext.Remove(entity);
        }

        public List<City> GetCities()
        {
            var cities = _databaseContext.Set<City>().ToList();
            return cities;
        }

        public City GetCityById(int id)
        {
            var city = _databaseContext.Set<City>().FirstOrDefault(c => c.Id == id);
            return city;
        }

        public Photo GetPhotoById(int id)
        {
            var photo = _databaseContext.Set<Photo>().FirstOrDefault(c => c.Id == id);
            return photo;
        }

        public List<Photo> GetPhotosByCityId(int id)
        {
            var photos = _databaseContext.Set<Photo>().Where(p=>p.CityId == id).ToList();
            return photos;
        }

        public bool SaveAll()
        {
            return _databaseContext.SaveChanges() > 0;
        }
    }
}
