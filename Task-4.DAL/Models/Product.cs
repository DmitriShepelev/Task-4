using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task_4.DAL.Models
{
    public sealed class Product : HaveId
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
