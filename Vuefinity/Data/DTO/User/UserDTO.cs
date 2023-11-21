using System.ComponentModel.DataAnnotations;

namespace Vuefinity.Data.DTO.User
{
    public class UserDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int Score { get; set; }
        public string Email { get; set; } = null!;
      

    }
}
