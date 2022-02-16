using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Task_4.DAL.Models;

namespace Web.Models
{
    public class OrderModel : HaveId
    {
        [Required(ErrorMessage = "Не указана дата")]
        public string PurchaseDate { get; set; }

        [Required(ErrorMessage = "Не указано количество")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Не указана фамилия клиента")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Client { get; set; }

        [Required(ErrorMessage = "Не указан товар")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Product { get; set; }

        [Required(ErrorMessage = "Не указана фамилия менеджера")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Manager { get; set; }
    }
}
