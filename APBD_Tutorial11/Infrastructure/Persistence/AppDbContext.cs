using APBD_Tutorial11.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Tutorial11.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Medicament?> Medicaments { get; set; } 
    public DbSet<Patient?> Patients { get; set; }
    public DbSet<Doctor?> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .HasKey(p => p.IdPatient);
        
        modelBuilder.Entity<Medicament>(entity =>
        {
            entity.HasKey(m => m.IdMedicament);

            entity.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(m => m.Type)
                .IsRequired()
                .HasMaxLength(100);
        });
        
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(d => d.IdDoctor);

            entity.Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Medicament>(entity =>
        {
            entity.HasKey(m => m.IdMedicament);

            entity.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(m => m.Type)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        {
            entity.HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

            entity.Property(pm => pm.Details)
                .IsRequired()
                .HasMaxLength(100);
        });
        
        modelBuilder.Entity<Prescription>()
            .HasKey(p => p.IdPrescription);
        
        
        /*Relations*/
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);
        
        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Patient)
            .WithMany(pat => pat.Prescriptions)
            .HasForeignKey(p => p.IdPatient);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdDoctor);
    }
}