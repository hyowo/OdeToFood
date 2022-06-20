using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Models
{
    public class UserProfile : IdentityUser
    {
        [PersonalData]
        public string FavoriteRestaurant { get; set; }
    }
}
