using Concert.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Concert.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
    }
}
