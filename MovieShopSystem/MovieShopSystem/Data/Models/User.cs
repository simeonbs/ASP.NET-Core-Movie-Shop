using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieShopSystem.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(35)]
        public string FullName { get; set; }


    }
}
