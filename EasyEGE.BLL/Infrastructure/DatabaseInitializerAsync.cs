using EasyEGE.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEGE
{
    public class DatabaseInitializerAsync
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext db)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Ruslan2201!";
            string adminName = "Большой босс";
            if (await roleManager.FindByNameAsync("Администратор") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Администратор"));
            }
            if (await roleManager.FindByNameAsync("Учитель") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Учитель"));
            }
            if (await roleManager.FindByNameAsync("Ученик") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Ученик"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, Name = adminName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Администратор");
                }
            }
            if (db.Subjects.FirstOrDefault(s => s.Name == "Математика") == null)
            {
                var subject = new Subject
                {
                    MaxNumber = 19,
                    MaxNumberInTest = 12,
                    Name = "Математика"
                };
                await db.Subjects.AddAsync(subject);
            }
            if (db.Subjects.FirstOrDefault(s => s.Name == "Информатика") == null)
            {
                var subject = new Subject
                {
                    MaxNumber = 27,
                    MaxNumberInTest = 23,
                    Name = "Информатика"
                };
                await db.Subjects.AddAsync(subject);
            }
            if (db.Subjects.FirstOrDefault(s => s.Name == "Русский язык") == null)
            {
                var subject = new Subject
                {
                    MaxNumber = 27,
                    MaxNumberInTest = 26,
                    Name = "Русский язык"
                };
                await db.Subjects.AddAsync(subject);
            }
            if (db.Subjects.FirstOrDefault(s => s.Name == "Физика") == null)
            {
                var subject = new Subject
                {
                    MaxNumber = 32,
                    MaxNumberInTest = 26,
                    Name = "Физика"
                };
                await db.Subjects.AddAsync(subject);
            }
            await db.SaveChangesAsync();
        }
    }
}
