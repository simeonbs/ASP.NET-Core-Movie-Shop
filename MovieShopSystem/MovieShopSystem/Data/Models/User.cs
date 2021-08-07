using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(35)]
        public string FullName { get; set; }


    }
}
