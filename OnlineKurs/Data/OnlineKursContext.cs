using Microsoft.EntityFrameworkCore;
using OnlineKurs.Models;

namespace OnlineKurs.Data
{
    public class OnlineKursContext : DbContext
    {
        // Constructor
        public OnlineKursContext(DbContextOptions<OnlineKursContext> options)
            : base(options)
        {
        }

        // DbSet Properties (Tablolar)
        public DbSet<Users> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
        public DbSet<Reviews> Reviews { get; set; }

        // OnModelCreating Method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tablolar için özel ayarlar
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Courses>().ToTable("Courses");
            modelBuilder.Entity<Enrollments>().ToTable("Enrollments");
            modelBuilder.Entity<Reviews>().ToTable("Reviews");

            // Users ve Courses İlişkisi
            modelBuilder.Entity<Courses>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde kursları da sil

            // Enrollments İlişkileri
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reviews İlişkileri
            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.Course)
                .WithMany()
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique Constraint: Bir kullanıcı bir kursa yalnızca bir kez kayıt olabilir
            modelBuilder.Entity<Enrollments>()
                .HasIndex(e => new { e.UserId, e.CourseId })
                .IsUnique();

            // Default Değerler (Varsayılan roller ve tarihler)
            modelBuilder.Entity<Users>()
                .Property(u => u.Role)
                .HasDefaultValue("user");

            modelBuilder.Entity<Courses>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Enrollments>()
                .Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Reviews>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
