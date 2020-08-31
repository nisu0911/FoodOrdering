using FoodOrdering.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodOrdering.ViewComponents
{
    public class UserNameViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext db;

        public UserNameViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsidetity = (ClaimsIdentity)User.Identity;
            var claims = claimsidetity.FindFirst(ClaimTypes.NameIdentifier);

            var userDb = await db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == claims.Value);

            return View(userDb);
        }
    }
}
