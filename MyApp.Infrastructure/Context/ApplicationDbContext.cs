using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Seeders;

namespace MyApp.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
            modelBuilder.Entity<DoctorsEntity>()
                .HasMany(d => d.DoctorSchedules)
                .WithOne(s => s.Doctor)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<AllergiesEntity> Allergies { get; set; }
        public DbSet<AppointmentPaymentsEntity> AppointmentPayments { get; set; }
        public DbSet<AppointmentsEntity> Appointments { get; set; }
        public DbSet<BloodTypesEntity> BloodTypes { get; set; }
        public DbSet<ConsumptionHabitsEntity> ConsumptionHabits { get; set; }
        public DbSet<DiagnosesEntity> Diagnoses { get; set; }
        public DbSet<DiseasesEntity> Diseases { get; set; }
        public DbSet<DoctorSchedulesEntity> DoctorSchedules { get; set; }
        public DbSet<DoctorsEntity> Doctors { get; set; }
        public DbSet<DoctorVacationsEntity> DoctorVacations { get; set; }
        public DbSet<EmergencyContactsEntity> EmergencyContacts { get; set; }
        public DbSet<GendersEntity> Genders { get; set; }
        public DbSet<HospitalScheduleDetailsEntity> HospitalScheduleDetails { get; set; }
        public DbSet<HospitalSchedulesEntity> HospitalSchedules { get; set; }
        public DbSet<IdentificationTypesEntity> IdentificationTypes { get; set; }
        public DbSet<MaritalStatusesEntity> MaritalStatuses { get; set; }
        public DbSet<MedicalConditionAllergiesEntity> MedicalConditionAllergies { get; set; }
        public DbSet<MedicalConditionDiseasesEntity> MedicalConditionDiseases { get; set; }
        public DbSet<MedicalConditionsEntity> MedicalConditions { get; set; }
        public DbSet<MedicalHistoriesEntity> MedicalHistories { get; set; }
        public DbSet<MedicalOrdersEntity> MedicalOrders { get; set; }
        public DbSet<MunicipalitiesEntity> Municipalities { get; set; }
        public DbSet<NotificationsEntity> Notifications { get; set; }
        public DbSet<OrderTypesEntity> OrderTypes { get; set; }
        public DbSet<PaymentTypesEntity> PaymentTypes { get; set; }
        public DbSet<PermissionsEntity> Permissions { get; set; }
        public DbSet<PrescriptionsEntity> Prescriptions { get; set; }
        public DbSet<RefreshTokensEntity> RefreshTokens { get; set; }
        public DbSet<RelationShipsEntity> RelationShips { get; set; }
        public DbSet<RolePermissionsEntity> RolePermissions { get; set; }
        public DbSet<RolesEntity> Roles { get; set; }
        public DbSet<SpecialtiesEntity> Specialties { get; set; }
        public DbSet<StatusesEntity> Statuses { get; set; }
        public DbSet<StatusTypesEntity> StatusTypes { get; set; }
        public DbSet<UserAddressDetailsEntity> UserAddressDetails { get; set; }
        public DbSet<UsersEntity> Users { get; set; }
        public DbSet<UserSessionsEntity> UserSessions { get; set; }

    }
}