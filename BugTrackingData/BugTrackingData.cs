using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BugTrackingData {
    public static class Auxiliary {
        public const int HashedPwSize = 24;
        public const int SaltSize = 24;
        public const int PasswordLength = 16;
        public const int MinLoginLength = 8;
        public const int MaxLoginLength = 32;
        public const int HashedPwOccupation = (HashedPwSize+SaltSize)/3*4;
        public const int IterationNumber = 100000;

    }
    [ComplexType]
    public class Address {
        [Required]
        public virtual string Country { get; set; }

        [Required]
        public virtual string Street { get; set; }

        public virtual int Number { get; set; }

        public virtual int ZIP { get; set; }
    }

    public class User {
        public virtual int Id{ get; set; }
        [Required,MinLength(Auxiliary.MinLoginLength),MaxLength(Auxiliary.MaxLoginLength)]
        [Index(IsUnique = true)]
        public virtual string Login{ get; set; }
        [Required, MinLength(Auxiliary.HashedPwOccupation),MaxLength(Auxiliary.HashedPwOccupation)]
        public virtual string Password{ get; set; }
        public virtual string FirstName{ get; set; }
        public virtual string LastName{ get; set; }
        [Required,MaxLength(16),Index(IsUnique = true),RegularExpression(@"[a-zA-Z]{6}\d{2}[a-zA-Z]\d{2}[a-zA-Z]\d{3}[a-zA-Z]")]
        public virtual string FiscalCode{ get; set; }
        public virtual Address Address{ get; set; }
        [NotMapped] public virtual int Age => DateTime.Now.Year - BirthDate.Year;
        public virtual DateTime BirthDate{ get; set; }

        public virtual ICollection<Comment> Comments{ get; set; }
        public virtual ICollection<Report> Reports{ get; set; }
    }

    public class Admin : User { }

    public class Product {
        public virtual int Id{ get; set; }
        //TODO TO be completed
    }

    public enum ReportState {
        Open,
        InProgress,
        Closed
    }

    public class Report {
        public virtual int Id{ get; set; }
        public virtual ReportState State{ get; set; }
        public virtual DateTime CreationDate{ get; set; }
        [Required,MaxLength(256)]
        public virtual String ShortDescription{ get; set; }
        public virtual String Text{ get; set; }
        public virtual int AuthorId{ get; set; }
        public virtual User Author{ get; set; }
        //TODO Add relationship toward Product
    }

    public class Comment {
        public virtual int Id{ get; set; }
        public virtual DateTime CreationDate{ get; set; }
        public virtual String Text{ get; set; }
        public virtual int AuthorId{ get; set; }
        public virtual User Author{ get; set; }
        public virtual int ReportId{ get; set; }
        public virtual Report Report{ get; set; }

    }

    public class BTContext : DbContext {
        public BTContext(string ConnectionString) : base(ConnectionString) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb){
            mb.Entity<User>().ToTable("Users");
            mb.Entity<Admin>().ToTable("Admins");
            mb.Entity<Comment>().HasRequired(c=>c.Author).WithMany(u=>u.Comments).WillCascadeOnDelete(false);
        }
    }
}