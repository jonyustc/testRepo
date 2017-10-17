﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAMCUC.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GaMCucDbEntities : DbContext
    {
        public GaMCucDbEntities()
            : base("name=GaMCucDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AcademicRecord> AcademicRecords { get; set; }
        public virtual DbSet<AcademicSession> AcademicSessions { get; set; }
        public virtual DbSet<AcrBoard> AcrBoards { get; set; }
        public virtual DbSet<AcrExam> AcrExams { get; set; }
        public virtual DbSet<AcrGroup> AcrGroups { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Guardian> Guardians { get; set; }
        public virtual DbSet<InitialPaymentDetail> InitialPaymentDetails { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<OriginalDocument> OriginalDocuments { get; set; }
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentSchedule> PaymentSchedules { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<StudentOriginalDocument> StudentOriginalDocuments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
    }
}
