using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortalV2.Models
{
    public class Educator
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Adı Soyadı")]
        public string NameSurname { get; set; }
        [DisplayName("Eğitmen Bilgisi")]
        public bool EducationType { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<MyCourse> MyCourses { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
