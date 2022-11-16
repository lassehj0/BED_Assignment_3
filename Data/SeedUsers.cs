using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Assignment3.Data
{
	public static class SeedUsers
	{
       
            public static void SeedWaiter(UserManager<IdentityUser> userManager)
            {
                const string WaiterEmail = "waiter@hotelcalifornia.com";
                const string WaiterPassword = "WaiterPassword1234#";
                if (userManager == null)
                    throw new ArgumentNullException(nameof(userManager));
                if (userManager.FindByNameAsync(WaiterEmail).Result == null)
                {
                    var user = new IdentityUser();
                    user.UserName = WaiterEmail;
                    user.Email = WaiterEmail;
                    user.EmailConfirmed = true;
                    IdentityResult result = userManager.CreateAsync(user, WaiterPassword).Result;

                    if (result.Succeeded)
                    {
                        var WaiterUser = userManager.FindByNameAsync(WaiterEmail).Result;
                        var claim = new Claim("Waiter", "true");
                        var claimAdded = userManager.AddClaimAsync(WaiterUser, claim).Result;
                    }
                }
            }

        public static void SeedReceptionist(UserManager<IdentityUser> userManager)
        {
            const string ReceptionistEmail = "receptionist@hotelcalifornia.com";
            const string ReceptionistPassword = "ReceptionistPassword1234#";
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));
            if (userManager.FindByNameAsync(ReceptionistEmail).Result == null)
            {
                var user = new IdentityUser();
                user.UserName = ReceptionistEmail;
                user.Email = ReceptionistEmail;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, ReceptionistPassword).Result;

                if (result.Succeeded)
                {
                    var ReceptionistUser = userManager.FindByNameAsync(ReceptionistEmail).Result;
                    var claim = new Claim("Receptionist", "true");
                    var claimAdded = userManager.AddClaimAsync(ReceptionistUser, claim).Result;
                }
            }
        }


    }
}
