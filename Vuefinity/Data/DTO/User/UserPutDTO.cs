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

        [StringLength(20)]
        public string PhoneNumber { get; set; } = null!;

        public Boolean AllowContact { get; set; }

    }
}

