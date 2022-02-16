using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Web.Models
{
    public class Role : IdentityRole
    {
        //public string Name { get; set; }
        public Role(string roleName)

        {
            Name = roleName;
            Id = new Guid().ToString();
        }
    }
}
