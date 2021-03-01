using Microsoft.AspNet.Identity.EntityFramework;
using OnnoRokom.BDJobs.DAL.Helpers;
using OnnoRokom.BDJobs.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OnnoRokom.BDJobs.Web.Seed
{
    public class Initializer
    {
        public Initializer(ApplicationUserManager userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;

            adminUser = new ApplicationUser("admin@gmail.com", "Md. Kawsarul Alam", "admin@gmail.com", "no-image.jpg");
            employerUser = new ApplicationUser("employer@gmail.com", "Md. Ariful Islam", "employer@gmail.com", "no-image.jpg");
            adminRole = new IdentityRole("Admin");
            employerRole = new IdentityRole("Employer");
        }

        public ApplicationUserManager _userManager { get; }
        public ApplicationDbContext _dbContext { get; }

        private ApplicationUser adminUser, employerUser;
        private IdentityRole adminRole, employerRole;

        public async Task SeedAsync()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            _dbContext.Database.Initialize(true);
            using (var tableCreatioinHelper = FNhibernateHelper.OpenSession(connectionString)) { }


            try
            {
                var result = await _userManager.CreateAsync(adminUser, "Admin$2021");
                if (result.Succeeded)
                {
                    _dbContext.Roles.Add(adminRole);
                    await _dbContext.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name);
                }

                result = await _userManager.CreateAsync(employerUser, "Employer$2021");
                if (result.Succeeded)
                {
                    _dbContext.Roles.Add(employerRole);
                    await _dbContext.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(employerUser.Id, employerRole.Name);
                }
                
            }
            catch(Exception ex)
            {

            }
        }
    }
}