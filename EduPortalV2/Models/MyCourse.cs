using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortalV2.Models
{
    public class MyCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int EducaterId { get; set; }
        public string UserId { get; set; }
        [DisplayName("Aktif mi?")]
        public bool Statu { get; set; }

        public virtual Course Course { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Educator Educator { get; set; }

    }
}
