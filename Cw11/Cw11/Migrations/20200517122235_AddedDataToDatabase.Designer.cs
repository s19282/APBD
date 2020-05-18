﻿// <auto-generated />
using System;
using Cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cw11.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    [Migration("20200517122235_AddedDataToDatabase")]
    partial class AddedDataToDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cw11.Models.Doctor", b =>
                {
                    b.Property<int>("IdDoctor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100)
                        .HasAnnotation("EmailAddress", "");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdDoctor");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            IdDoctor = 1,
                            Email = "e1@tmp.us",
                            FirstName = "FN1",
                            LastName = "LN1"
                        },
                        new
                        {
                            IdDoctor = 2,
                            Email = "e2@tmp.us",
                            FirstName = "FN2",
                            LastName = "LN2"
                        },
                        new
                        {
                            IdDoctor = 3,
                            Email = "e3@tmp.us",
                            FirstName = "FN3",
                            LastName = "LN3"
                        });
                });

            modelBuilder.Entity("Cw11.Models.Medicament", b =>
                {
                    b.Property<int>("IdMedicament")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdMedicament");

                    b.ToTable("Medicaments");

                    b.HasData(
                        new
                        {
                            IdMedicament = 1,
                            Description = "DM1",
                            Name = "M1",
                            Type = "T1"
                        },
                        new
                        {
                            IdMedicament = 2,
                            Description = "DM2",
                            Name = "M2",
                            Type = "T2"
                        },
                        new
                        {
                            IdMedicament = 3,
                            Description = "DM3",
                            Name = "M3",
                            Type = "T3"
                        });
                });

            modelBuilder.Entity("Cw11.Models.Patient", b =>
                {
                    b.Property<int>("IdPatient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirdthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdPatient");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            IdPatient = 1,
                            BirdthDate = new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "FNP1",
                            LastName = "LNP1"
                        },
                        new
                        {
                            IdPatient = 2,
                            BirdthDate = new DateTime(2022, 5, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "FNP2",
                            LastName = "LNP2"
                        },
                        new
                        {
                            IdPatient = 3,
                            BirdthDate = new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "FNP3",
                            LastName = "LNP3"
                        });
                });

            modelBuilder.Entity("Cw11.Models.Prescription", b =>
                {
                    b.Property<int>("IdPrescription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdDoctor")
                        .HasColumnType("int");

                    b.Property<int?>("IdPatient")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription");

                    b.HasIndex("IdDoctor");

                    b.HasIndex("IdPatient");

                    b.ToTable("Prescriptions");

                    b.HasData(
                        new
                        {
                            IdPrescription = 1,
                            Date = new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2020, 5, 31, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 1,
                            IdPatient = 1
                        },
                        new
                        {
                            IdPrescription = 2,
                            Date = new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2020, 5, 31, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 2,
                            IdPatient = 2
                        },
                        new
                        {
                            IdPrescription = 3,
                            Date = new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2020, 5, 31, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 3,
                            IdPatient = 3
                        });
                });

            modelBuilder.Entity("Cw11.Models.Prescription_Medicament", b =>
                {
                    b.Property<int?>("IdPrescription")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Dose")
                        .HasColumnType("int");

                    b.Property<int?>("IdMedicament")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("IdPrescription");

                    b.HasIndex("IdMedicament");

                    b.ToTable("Prescriptions_Medicaments");

                    b.HasData(
                        new
                        {
                            IdPrescription = 1,
                            Details = "D123",
                            Dose = 1,
                            IdMedicament = 1
                        },
                        new
                        {
                            IdPrescription = 2,
                            Details = "D12",
                            Dose = 11,
                            IdMedicament = 2
                        },
                        new
                        {
                            IdPrescription = 3,
                            Details = "D1",
                            Dose = 111,
                            IdMedicament = 3
                        });
                });

            modelBuilder.Entity("Cw11.Models.Prescription", b =>
                {
                    b.HasOne("Cw11.Models.Doctor", "doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdDoctor");

                    b.HasOne("Cw11.Models.Patient", "patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdPatient");
                });

            modelBuilder.Entity("Cw11.Models.Prescription_Medicament", b =>
                {
                    b.HasOne("Cw11.Models.Medicament", "medicament")
                        .WithMany("PrescriptionsMedicaments")
                        .HasForeignKey("IdMedicament")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cw11.Models.Prescription", "prescription")
                        .WithMany("PrescriptionsMedicaments")
                        .HasForeignKey("IdPrescription")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
