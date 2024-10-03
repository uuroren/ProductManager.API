using System.ComponentModel.DataAnnotations;

namespace ProductManager.API.Dtos {
    public class UserLoginModelDto {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
