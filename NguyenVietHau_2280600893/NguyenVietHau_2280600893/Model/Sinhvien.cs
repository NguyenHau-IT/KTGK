namespace NguyenVietHau_2280600893.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sinhvien")]
    public partial class Sinhvien
    {
        [Key]
        [StringLength(6)]
        public string MaSv { get; set; }

        [StringLength(40)]
        public string HotenSV { get; set; }

        public DateTime? Ngaysinh { get; set; }

        [StringLength(3)]
        public string Malop { get; set; }

        public virtual Lop Lop { get; set; }
    }
}
