namespace Auxom.API.Requests.UserProfile
{
    public class UpdateProfileRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IFormFile? ProfileImage { get; set; }
    }
}
