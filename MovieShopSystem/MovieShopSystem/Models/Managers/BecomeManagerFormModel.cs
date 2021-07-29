using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Models.Managers
{
    public class BecomeManagerFormModel
    {
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
