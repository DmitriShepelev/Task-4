using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
   public sealed class Client
    {
       // [Key]
        public int Id { get; set; }

       // [Required]
       public string Name { get; }
        public string FirstName { get; set; }

       // [Required]
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Client() => Orders = new HashSet<Order>();
    }
}
