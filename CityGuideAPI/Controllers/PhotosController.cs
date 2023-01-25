using AutoMapper;
using CityGuideAPI.DataAccess;
using CityGuideAPI.Dtos;
using CityGuideAPI.Helpers;
using CityGuideAPI.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace CityGuideAPI.Controllers
{
    [Route("api/[controller]/{cityId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IEntityRepository _entityRepository;
        private IMapper _mapper;
        private IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public PhotosController(IEntityRepository entityRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _entityRepository=entityRepository;
            _mapper=mapper;
            _cloudinaryConfig=cloudinaryConfig;
            Account account = new(_cloudinaryConfig.Value.CloudName,
                                  _cloudinaryConfig.Value.ApiKey,
                                  _cloudinaryConfig.Value.ApiSecret);
            _cloudinary=new Cloudinary(account);
        }
        [HttpPost]
        public ActionResult AddPhotoForCity(int cityId, [FromBody] PhotoForCreationDto photoForCreationDto)
        {
            var city = _entityRepository.GetCityById(cityId);
            if (city == null)
            {
                return BadRequest("couldnt find a city");
            }
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId != city.UserId)
            {
                return Unauthorized();
            }

            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length >0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.City = city;

            if (!city.Photos.Any(p => p.IsMain))
            {
                photo.IsMain = true;
            }

            city.Photos.Add(photo);

            if (_entityRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("couldnt add yout photo");
        }

        [HttpGet("{id}", Name ="getphoto")]
        public ActionResult GetPhoto(int id)
        {
            var photoFromDb = _entityRepository.GetPhotoById(id);
            var photo = _mapper.Map<PhotoForReturnDto>(photoFromDb);
            return Ok(photo);
        }
    }
}
