using Cw11.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cw11.Models
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescriptions_Medicaments { get; set; }

        public HospitalDbContext() { }

        public HospitalDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new PrescriptionConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentConfiguration());
            modelBuilder.ApplyConfiguration(new Prescription_MedicamentConfiguration());

            InsertData(modelBuilder);
        }
        public void InsertData(ModelBuilder modelBuilder)
        {
            var doctors = new List<Doctor>();
            doctors.Add(new Doctor { IdDoctor = 1, FirstName = "FN1", LastName = "LN1", Email = "e1@tmp.us" });
            doctors.Add(new Doctor { IdDoctor = 2, FirstName = "FN2", LastName = "LN2", Email = "e2@tmp.us" });
            doctors.Add(new Doctor { IdDoctor = 3, FirstName = "FN3", LastName = "LN3", Email = "e3@tmp.us" });
            modelBuilder.Entity<Doctor>().HasData(doctors);

            var patients = new List<Patient>();
            patients.Add(new Patient { IdPatient = 1, FirstName = "FNP1", LastName = "LNP1", BirdthDate = DateTime.Today });
            patients.Add(new Patient { IdPatient = 2, FirstName = "FNP2", LastName = "LNP2", BirdthDate = DateTime.Today.AddYears(2) });
            patients.Add(new Patient { IdPatient = 3, FirstName = "FNP3", LastName = "LNP3", BirdthDate = DateTime.Today.AddYears(4) });
            modelBuilder.Entity<Patient>().HasData(patients);

            var prescriptions = new List<Prescription>();
            prescriptions.Add(new Prescription { IdPrescription = 1, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(14), IdPatient = 1, IdDoctor = 1 });
            prescriptions.Add(new Prescription { IdPrescription = 2, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(14), IdPatient = 2, IdDoctor = 2 });
            prescriptions.Add(new Prescription { IdPrescription = 3, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(14), IdPatient = 3, IdDoctor = 3 });
            modelBuilder.Entity<Prescription>().HasData(prescriptions);

            var medicaments = new List<Medicament>();
            medicaments.Add(new Medicament { IdMedicament = 1, Name = "M1", Description = "DM1",Type="T1" });
            medicaments.Add(new Medicament { IdMedicament = 2, Name = "M2", Description = "DM2",Type="T2" });
            medicaments.Add(new Medicament { IdMedicament = 3, Name = "M3", Description = "DM3",Type="T3" });
            modelBuilder.Entity<Medicament>().HasData(medicaments);

            var prescriptionsMedicaments = new List<Prescription_Medicament>();
            prescriptionsMedicaments.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "D123" });
            prescriptionsMedicaments.Add(new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 11, Details = "D12" });
            prescriptionsMedicaments.Add(new Prescription_Medicament { IdMedicament = 3, IdPrescription = 3, Dose = 111, Details = "D1" });
            modelBuilder.Entity<Prescription_Medicament>().HasData(prescriptionsMedicaments);

        }
    }
}
