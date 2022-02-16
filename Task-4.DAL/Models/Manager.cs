using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task_4.DAL.Models
{
    public sealed class Manager : HaveId
    {
        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
