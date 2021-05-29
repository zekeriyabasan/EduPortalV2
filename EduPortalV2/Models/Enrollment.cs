using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortalV2.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual Course Course { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
