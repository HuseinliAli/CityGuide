using AutoMapper;
using CityGuideAPI.DataAccess;
using CityGuideAPI.Dtos;
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
    }
}
