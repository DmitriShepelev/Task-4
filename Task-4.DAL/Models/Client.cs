using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task_4.DAL.Models
{
    public sealed class Client : HaveId
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
