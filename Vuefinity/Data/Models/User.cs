

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vuefinity.Data.Models
{
    //Define the structure of the data that will be stored in the database. 
    [Table(nameof(User))]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Email { get; set; } = null!;


        [StringLength(100)]
        public string Name { get; set; } = null!;

        public int Score { get; set; }
      
    }
}
