using AutoMapper;
using Auxom.API.Requests.UserProfile;
using Auxom.Application.DTOs.User;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auxom.API.Controllers
{

    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public ProfileController(IUserProfileService userProfileService, IImageService imageService , IMapper mapper)
        {
            _userProfileService = userProfileService;
            _imageService = imageService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserProfileAsync()
        {
            Guid userId = GetUserId();
            var userProfile = await _userProfileService.GetProfileAsync(userId);
            return Ok(userProfile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfileAsync([FromForm] UpdateProfileRequest request)
        {
            var userId = GetUserId();
            var dto = _mapper.Map<UpdateProfileDto>(request);
            
            if (request.ProfileImage != null)
            {
                var profileImageUrl = await _imageService.UploadImageAsync(
          request.ProfileImage.OpenReadStream(),
          request.ProfileImage.FileName,
          request.ProfileImage.ContentType,
          "ProfileImage"
          );
                dto.ProfileImage = profileImageUrl;
            }
           var profile =  await _userProfileService.UpdateProfileAsync(userId, dto);
            return Ok(profile);
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return Guid.Parse(userId);
        }
    }
}
