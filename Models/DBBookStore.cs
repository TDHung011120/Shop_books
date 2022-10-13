using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MVC_BookStore.Models
{
    public partial class DBBookStore : DbContext
    {
        public DBBookStore()
            : base("name=DBBookStore")
        {
        }

        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<CTDatHang> CTDatHangs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<QTV> QTVs { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<VietSach> VietSaches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTDatHang>()
                .Property(e => e.Dongia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CTDatHang>()
                .Property(e => e.Thanhtien)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.Trigia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.CTDatHangs)
                .WithRequired(e => e.DonDatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QTV>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.Dongia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.CTDatHangs)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.VietSaches)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TacGia>()
                .HasMany(e => e.VietSaches)
                .WithRequired(e => e.TacGia)
                .WillCascadeOnDelete(false);
        }
    }
}
