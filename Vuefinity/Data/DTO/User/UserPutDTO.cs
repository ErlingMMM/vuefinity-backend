using System.ComponentModel.DataAnnotations;

namespace Vuefinity.Data.DTO.User
{
    public class UserPutDTO
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Email { get; set; } = null!;


        [StringLength(100)]
        public string Name { get; set; } = null!;

        public int Score { get; set; }

    }
}

