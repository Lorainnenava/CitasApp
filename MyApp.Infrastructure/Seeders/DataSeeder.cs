using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Seeders
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Municipios del Atlántico, Colombia
            modelBuilder.Entity<MunicipalitiesEntity>().HasData(
                new MunicipalitiesEntity { MunicipalityId = 1, Name = "Barranquilla" },
                new MunicipalitiesEntity { MunicipalityId = 2, Name = "Baranoa" },
                new MunicipalitiesEntity { MunicipalityId = 3, Name = "Campo de la Cruz" },
                new MunicipalitiesEntity { MunicipalityId = 4, Name = "Candelaria" },
                new MunicipalitiesEntity { MunicipalityId = 5, Name = "Galapa" },
                new MunicipalitiesEntity { MunicipalityId = 6, Name = "Juan de Acosta" },
                new MunicipalitiesEntity { MunicipalityId = 7, Name = "Luruaco" },
                new MunicipalitiesEntity { MunicipalityId = 8, Name = "Malambo" },
                new MunicipalitiesEntity { MunicipalityId = 9, Name = "Manatí" },
                new MunicipalitiesEntity { MunicipalityId = 10, Name = "Palmar de Varela" },
                new MunicipalitiesEntity { MunicipalityId = 11, Name = "Piojó" },
                new MunicipalitiesEntity { MunicipalityId = 12, Name = "Polonuevo" },
                new MunicipalitiesEntity { MunicipalityId = 13, Name = "Ponedera" },
                new MunicipalitiesEntity { MunicipalityId = 14, Name = "Puerto Colombia" },
                new MunicipalitiesEntity { MunicipalityId = 15, Name = "Repelón" },
                new MunicipalitiesEntity { MunicipalityId = 16, Name = "Sabanagrande" },
                new MunicipalitiesEntity { MunicipalityId = 17, Name = "Sabanalarga" },
                new MunicipalitiesEntity { MunicipalityId = 18, Name = "Santa Lucía" },
                new MunicipalitiesEntity { MunicipalityId = 19, Name = "Santo Tomás" },
                new MunicipalitiesEntity { MunicipalityId = 20, Name = "Soledad" },
                new MunicipalitiesEntity { MunicipalityId = 21, Name = "Suán" },
                new MunicipalitiesEntity { MunicipalityId = 22, Name = "Tubará" }
            );

            // Alergias comunes
            modelBuilder.Entity<AllergiesEntity>().HasData(
                new AllergiesEntity { AllergyId = 1, Name = "Polen" },
                new AllergiesEntity { AllergyId = 2, Name = "Ácaros del polvo" },
                new AllergiesEntity { AllergyId = 3, Name = "Moho" },
                new AllergiesEntity { AllergyId = 4, Name = "Caspa de animales" },
                new AllergiesEntity { AllergyId = 5, Name = "Látex" },
                new AllergiesEntity { AllergyId = 6, Name = "Picaduras de insectos" },
                new AllergiesEntity { AllergyId = 7, Name = "Alimentos" },
                new AllergiesEntity { AllergyId = 8, Name = "Medicamentos" }
            );

            // Enfermedades comunes
            modelBuilder.Entity<DiseasesEntity>().HasData(
                new DiseasesEntity { DiseaseId = 1, Name = "Hipertensión arterial" },
                new DiseasesEntity { DiseaseId = 2, Name = "Diabetes mellitus" },
                new DiseasesEntity { DiseaseId = 3, Name = "Asma" },
                new DiseasesEntity { DiseaseId = 4, Name = "Gastritis" },
                new DiseasesEntity { DiseaseId = 5, Name = "Migraña" },
                new DiseasesEntity { DiseaseId = 6, Name = "Alergia alimentaria" },
                new DiseasesEntity { DiseaseId = 7, Name = "Obesidad" },
                new DiseasesEntity { DiseaseId = 8, Name = "Artritis" },
                new DiseasesEntity { DiseaseId = 9, Name = "Bronquitis crónica" },
                new DiseasesEntity { DiseaseId = 10, Name = "Colesterol alto" }
            );

            // Géneros
            modelBuilder.Entity<GendersEntity>().HasData(
                new GendersEntity { GenderId = 1, Name = "Masculino" },
                new GendersEntity { GenderId = 2, Name = "Femenino" },
                new GendersEntity { GenderId = 3, Name = "Homosexual" },
                new GendersEntity { GenderId = 3, Name = "Bisexual" },
                new GendersEntity { GenderId = 3, Name = "Otro" }
            );

            // Roles
            modelBuilder.Entity<RolesEntity>().HasData(
                new RolesEntity { RoleId = 1, Name = "Administrador" },
                new RolesEntity { RoleId = 2, Name = "Doctor" },
                new RolesEntity { RoleId = 3, Name = "Paciente" }
            );

            // Métodos de pago
            modelBuilder.Entity<PaymentTypesEntity>().HasData(
                new PaymentTypesEntity { PaymentTypeId = 1, Name = "Efectivo" },
                new PaymentTypesEntity { PaymentTypeId = 2, Name = "Tarjeta" }
            );

            // Especialidades médicas
            modelBuilder.Entity<SpecialtiesEntity>().HasData(
                new SpecialtiesEntity { SpecialtyId = 1, Name = "Pediatría" },
                new SpecialtiesEntity { SpecialtyId = 3, Name = "Cardiología" },
                new SpecialtiesEntity { SpecialtyId = 4, Name = "Medicina General" },
                new SpecialtiesEntity { SpecialtyId = 5, Name = "Medicina Externa" },
                new SpecialtiesEntity { SpecialtyId = 8, Name = "Psicologia" },
                new SpecialtiesEntity { SpecialtyId = 8, Name = "Odontologia" }
            );

            // Estados
            modelBuilder.Entity<StatusesEntity>().HasData(
                new StatusesEntity { StatusId = 1, Name = "Activo", StatusTypeId = 3 },
                new StatusesEntity { StatusId = 2, Name = "Vencido", StatusTypeId = 3 },
                new StatusesEntity { StatusId = 3, Name = "Postergada", StatusTypeId = 3 },
                new StatusesEntity { StatusId = 4, Name = "Facturada", StatusTypeId = 3 },
                new StatusesEntity { StatusId = 5, Name = "Completada", StatusTypeId = 3 },
                new StatusesEntity { StatusId = 6, Name = "Sin pagar", StatusTypeId = 2 },
                new StatusesEntity { StatusId = 7, Name = "Pagada", StatusTypeId = 2 },
                new StatusesEntity { StatusId = 8, Name = "Pendiente", StatusTypeId = 1 },
                new StatusesEntity { StatusId = 9, Name = "Confirmada", StatusTypeId = 1 },
                new StatusesEntity { StatusId = 10, Name = "Cancelada", StatusTypeId = 1 },
                new StatusesEntity { StatusId = 11, Name = "Reprogramada", StatusTypeId = 1 },
                new StatusesEntity { StatusId = 12, Name = "No presentada", StatusTypeId = 1 }
            );

            // Tipos de estados
            modelBuilder.Entity<StatusTypesEntity>().HasData(
                new StatusTypesEntity { StatusTypeId = 1, Name = "Citas medicas" },
                new StatusTypesEntity { StatusTypeId = 2, Name = "Pagos citas medicas" },
                new StatusTypesEntity { StatusTypeId = 3, Name = "Ordenes medicas" }
            );

            // Tipos de ordenes
            modelBuilder.Entity<OrderTypesEntity>().HasData(
                new OrderTypesEntity { OrderTypeId = 1, Name = "Orden de laboratorio" },
                new OrderTypesEntity { OrderTypeId = 2, Name = "Orden de imagenología" },
                new OrderTypesEntity { OrderTypeId = 3, Name = "Receta médica" },
                new OrderTypesEntity { OrderTypeId = 4, Name = "Orden de remisión" },
                new OrderTypesEntity { OrderTypeId = 5, Name = "Orden de procedimiento" },
                new OrderTypesEntity { OrderTypeId = 6, Name = "Orden de hospitalización" },
                new OrderTypesEntity { OrderTypeId = 7, Name = "Orden de terapias" },
                new OrderTypesEntity { OrderTypeId = 8, Name = "Orden de incapacidad" },
                new OrderTypesEntity { OrderTypeId = 9, Name = "Orden de control" },
                new OrderTypesEntity { OrderTypeId = 10, Name = "Orden de dieta" },
                new OrderTypesEntity { OrderTypeId = 11, Name = "Orden quirúrgica" }
            );

            // Tipos de identificación
            modelBuilder.Entity<IdentificationTypesEntity>().HasData(
                new IdentificationTypesEntity { IdentificationTypeId = 1, Name = "Cédula de Ciudadanía" },
                new IdentificationTypesEntity { IdentificationTypeId = 2, Name = "Tarjeta de Identidad" },
                new IdentificationTypesEntity { IdentificationTypeId = 3, Name = "Registro Civil" },
                new IdentificationTypesEntity { IdentificationTypeId = 4, Name = "Cédula de Extranjería" },
                new IdentificationTypesEntity { IdentificationTypeId = 5, Name = "Pasaporte" },
                new IdentificationTypesEntity { IdentificationTypeId = 6, Name = "Permiso por Protección Temporal" }
            );

            // Tipos de estado civil
            modelBuilder.Entity<MaritalStatusesEntity>().HasData(
                new MaritalStatusesEntity { MaritalStatusId = 1, Name = "Soltero (a)" },
                new MaritalStatusesEntity { MaritalStatusId = 2, Name = "Casado (a)" },
                new MaritalStatusesEntity { MaritalStatusId = 3, Name = "Unión libre" },
                new MaritalStatusesEntity { MaritalStatusId = 4, Name = "Separado (a)" },
                new MaritalStatusesEntity { MaritalStatusId = 5, Name = "Divorciado (a)" },
                new MaritalStatusesEntity { MaritalStatusId = 6, Name = "Viudo (a)" }
            );

            // Tipos de sangre
            modelBuilder.Entity<BloodTypesEntity>().HasData(
                new BloodTypesEntity { BloodTypeId = 1, Name = "A+" },
                new BloodTypesEntity { BloodTypeId = 2, Name = "A−" },
                new BloodTypesEntity { BloodTypeId = 3, Name = "B+" },
                new BloodTypesEntity { BloodTypeId = 4, Name = "B−" },
                new BloodTypesEntity { BloodTypeId = 5, Name = "AB+" },
                new BloodTypesEntity { BloodTypeId = 6, Name = "AB−" },
                new BloodTypesEntity { BloodTypeId = 7, Name = "O+" },
                new BloodTypesEntity { BloodTypeId = 8, Name = "O−" }
            );
        }
    }
}
