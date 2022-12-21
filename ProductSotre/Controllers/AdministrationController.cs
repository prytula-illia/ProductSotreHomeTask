using DAL.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductSotre.Data;
using ProductSotre.Filters;

namespace ProductSotre.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private PSContext _context;

        public AdministrationController(PSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _context.Users;
        }
    }
}
