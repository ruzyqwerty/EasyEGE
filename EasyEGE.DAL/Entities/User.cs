using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EasyEGE.DAL.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public List<Option> Options { get; set; }
    }
}
