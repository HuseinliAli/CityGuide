using CityGuideAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CityGuideAPI.Dtos
{
    public class CityForDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; } 
    }
}
