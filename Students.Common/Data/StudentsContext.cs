﻿using Microsoft.EntityFrameworkCore;
using Students.Common.Models;
using System.Reflection;

namespace Students.Common.Data;

public class StudentsContext : DbContext
{
    public StudentsContext (DbContextOptions<StudentsContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Student { get; set; } = default!;
    public DbSet<Subject> Subject { get; set; } = default!;
    public DbSet<StudentSubject> StudentSubject { get; set; } = default!;

    public DbSet<Book> Book { get; set; } = default!;
    public DbSet<LectureHall> LectureHall { get; set; } = default!;

    public DbSet<LectureHallSubject> LectureHallSubject { get; set; } = default!;

    public DbSet<ResearchWorker> ResearchWorker { get; set; } = default!;  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Book>().HasKey(b => b.Id);

        modelBuilder.Entity<StudentSubject>()
            .HasKey(ss => new { ss.StudentId, ss.SubjectId });

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.StudentId);

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Subject)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.SubjectId);

        //modelBuilder.Entity<LectureHall>()
        //  .HasMany(s => s.Subjects)
        //  .WithOne(s => s.LectureHall)
        //  .HasForeignKey(s => s.LectureHallID);


        modelBuilder.Entity<LectureHallSubject>()
            .HasKey(ss => new { ss.LectureHallId, ss.SubjectId });

        modelBuilder.Entity<LectureHallSubject>()
           .HasOne(ls => ls.LectureHall)
           .WithMany(s => s.LectureHallSubjects)
           .HasForeignKey(ls => ls.LectureHallId);

        modelBuilder.Entity<LectureHallSubject>()
            .HasOne(ls => ls.Subject)
            .WithMany(s => s.LectureHallSubjects)
            .HasForeignKey(ls => ls.SubjectId);



        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
