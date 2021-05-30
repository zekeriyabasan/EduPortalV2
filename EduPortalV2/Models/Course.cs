using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortalV2.Models
{
    public class Course
    {

        [Key]
        public int Id { get; set; }
        [DisplayName("Kurs Adı")]
        public string CourseName { get; set; }
        [DisplayName("Kurs Açıklaması")]
        public string CourseDescription { get; set; }
        [DisplayName("Kurs Kodu")]
        public string Code { get; set; }
        [DisplayName("Kontenjan Sayısı")]
        public int CuotaCount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        [DisplayName("(Günlük) Kurs Fiyatı")]
        public decimal PriceDaily { get; set; }
        [DisplayName("Kurs Bağlantısı")]
        public string VideoUrl { get; set; }
        [DisplayName("Döküman Bağlantısı")]
        public string DocumentUrl { get; set; }
        [DisplayName("Kategori ID")]
        public int CategoryId { get; set; }
        [DisplayName("Eğitmen ID")]
        public int EducatorId { get; set; }
        [DisplayName("Kategori")]
        public virtual Category Category { get; set; }
        [DisplayName("Eğitmen")]
        public virtual Educator Educator { get; set; }
        [DisplayName("Kayıtlar")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<MyCourse> MyCourses { get; set; }
    }
}
