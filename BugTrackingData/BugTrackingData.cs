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
        public const int IterationNumber = 100000;

    }
    [ComplexType]
    public class Address {
        [Required]
        public virtual string Country { get; set; }

        [Required]
        public virtual string Street { get; set; }

        [Required]
        public virtual int Number { get; set; }

        public virtual int ZIP { get; set; }
    }

    public class User {
    }

    public class Admin : User { }

    public class Product {
    }

    public enum ReportState {
        Open,
        InProgress,
        Closed
    }

    public class Report {
     }

    public class Comment {
 
    }

    public class BTContext : DbContext {
        public BTContext(string ConnectionString) : base(ConnectionString) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb) {
         }
    }
}