namespace MVC_BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QTV")]
    public partial class QTV
    {
        [Required]
        [StringLength(20)]
        public string TenDN { get; set; }

        [Required]
        [StringLength(50)]
        public string MK { get; set; }

        [Key]
        public int MaQTV { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
