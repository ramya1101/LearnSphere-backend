//using LearnSphere.Models;
//using Microsoft.EntityFrameworkCore;
//using System;

//namespace LearnSphere.Data
//{
//    public class LearnSphereContext : DbContext
//    {
//        public LearnSphereContext(DbContextOptions<LearnSphereContext> options) : base(options) { }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Course> Courses { get; set; }
//        public DbSet<Enrollment> Enrollments { get; set; }
//        public DbSet<Progress> Progresses { get; set; }
//        public DbSet<Feedback> Feedbacks { get; set; }
//        public DbSet<Session> Sessions { get; set; }
//        // Add DbSet for Enrollments, Sessions, Progress, Feedback as needed
//    }
//}
using Microsoft.EntityFrameworkCore;
using LearnSphere.Models;

namespace LearnSphere.Data
{
    public class LearnSphereContext : DbContext
    {
        public LearnSphereContext(DbContextOptions<LearnSphereContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public DbSet<LearnSphere.Models.Progress> Progresses { get; set; }
        public DbSet<Feedback> Feedback { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enrollment relationships
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Progress relationships
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.Student)
                .WithMany()
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Progress>()
                .HasOne(p => p.Course)
                .WithMany()
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Feedback relationships
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Student)
                .WithMany()
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Course)
                .WithMany()
                .HasForeignKey(f => f.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Session relationships
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Course)
                .WithMany()
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Mentor)
                .WithMany()
                .HasForeignKey(s => s.MentorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // Enrollment → Student (No cascade delete)
//            modelBuilder.Entity<Enrollment>()
//                .HasOne(e => e.Student)
//                .WithMany()
//                .HasForeignKey(e => e.StudentId)
//                .OnDelete(DeleteBehavior.Restrict);

//            // Enrollment → Course (Cascade delete allowed if you want)
//            modelBuilder.Entity<Enrollment>()
//                .HasOne(e => e.Course)
//                .WithMany()
//                .HasForeignKey(e => e.CourseId)
//                .OnDelete(DeleteBehavior.Cascade);
//        }
//    }
//}


//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);

//        // Enrollment: ensure FK relationships are clear
//        modelBuilder.Entity<Enrollment>()
//            .HasKey(e => e.EnrollmentId);

//        modelBuilder.Entity<Enrollment>()
//            .HasOne(e => e.Course)
//            .WithMany()
//            .HasForeignKey(e => e.CourseId)
//            .OnDelete(DeleteBehavior.Cascade);

//        modelBuilder.Entity<Enrollment>()
//            .HasOne(e => e.Student)
//            .WithMany()
//            .HasForeignKey(e => e.StudentId)
//            .OnDelete(DeleteBehavior.Cascade);
//    }
//}

