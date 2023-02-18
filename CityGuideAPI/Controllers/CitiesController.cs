using AutoMapper;
using CityGuideAPI.DataAccess;
using CityGuideAPI.Dtos;
using CityGuideAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CityGuideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        IEntityRepository _entityRepository;
        private IMapper _mapper;
        public CitiesController(IEntityRepository entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        [HttpGet("getcities")]
        public ActionResult GetCities()
        {
            var cities = _entityRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities); 
            return Ok(citiesToReturn);
        }
        [HttpGet("detail")]
        public ActionResult GetCityById(int id)
        {
            var city = _entityRepository.GetCityById(id); ;
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }

        [HttpPost("add")]
        public ActionResult Add([FromBody]City city)
        {
            _entityRepository.Add(city);
            _entityRepository.SaveAll();
            return Ok(city);
        }

        [HttpGet("photos")]
        public ActionResult Photos(int cityId)
        {
            var photos = _entityRepository.GetPhotosByCityId(cityId);
            return Ok();
        }

        [HttpPost("delete")]
        public ActionResult Delete([FromBody]City city)
        {
            var name = city.Name;
            _entityRepository.Delete(city);
            _entityRepository.SaveAll();
            return Ok("city is deleted from database");
        }
    }
}
