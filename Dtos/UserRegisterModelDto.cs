using System.ComponentModel.DataAnnotations;

namespace ProductManager.API.Dtos {
    public class UserRegisterModelDto {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
