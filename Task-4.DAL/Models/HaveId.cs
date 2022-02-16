using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.DAL.Models
{
  public  class HaveId
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}
