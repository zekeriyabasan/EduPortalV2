using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortalV2.Models
{
    public class ApplicationUser:IdentityUser
    {
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Educator> Educators { get; set; }
        public virtual ICollection<MyCourse> MyCourses { get; set; }

    }
}
