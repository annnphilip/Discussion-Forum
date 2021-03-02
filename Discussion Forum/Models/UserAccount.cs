using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Discussion_Forum.Models
{
    public class UserAccount : IdentityUser<int>
    {

        //public int Id { get; set; }
      
        public string FullName { get; set; }
       
        //public string Email { get; set; }
      
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
    }

   // public class UserAccount : IdentityUser<long,AppUserClaim,AppUserRole,AppUserLogin> { } 
    //public class AppUserClaim : IdentityUserClaim<int> { } 
    //public class AppUserToken : IdentityUserToken<int> { } 
    //public class AppUserLogin: IdentityUserLogin<int> { } 
    //public class AppUserRole : IdentityUserRole<int> { }
    //public class AppRole : IdentityRole<int> { }// IdentityRole<long,AppUserRole,AppRoleClaim> {} 
    //public class AppRoleClaim : IdentityRoleClaim<int> { }
}
