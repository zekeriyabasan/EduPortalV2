using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortalV2.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Kategori Adı")]
        public string CategoryName { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
