using CityGuideAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGuideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private DatabaseContext _context;

        public WeatherForecastController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetTests()
        {
            var tests = await _context.Tests.ToListAsync();
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTest(int id)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(t => t.Id == id);
            return Ok(test);
        }


    }
}
