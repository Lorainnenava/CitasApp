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
                new MunicipalitiesEntity { MunicipalityId = 1, Name = "Barranquilla", IsSystemDefined = true },
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
                new GendersEntity { GenderId = 4, Name = "Bisexual" },
                new GendersEntity { GenderId = 5, Name = "Otro" }
            );

            // Roles
            modelBuilder.Entity<RolesEntity>().HasData(
                new RolesEntity { RoleId = 1, Name = "SuperAdmin" },
                new RolesEntity { RoleId = 2, Name = "Administrador" },
                new RolesEntity { RoleId = 3, Name = "Doctor" },
                new RolesEntity { RoleId = 4, Name = "Paciente" }
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
                new SpecialtiesEntity { SpecialtyId = 6, Name = "Psicologia" },
                new SpecialtiesEntity { SpecialtyId = 7, Name = "Odontologia" }
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
                new StatusesEntity { StatusId = 12, Name = "No presentada", StatusTypeId = 1 },
                new StatusesEntity { StatusId = 13, Name = "En estudio", StatusTypeId = 4 },
                new StatusesEntity { StatusId = 14, Name = "Aceptada", StatusTypeId = 4 },
                new StatusesEntity { StatusId = 15, Name = "Rechazada", StatusTypeId = 4 }
            );

            // Tipos de estados
            modelBuilder.Entity<StatusTypesEntity>().HasData(
                new StatusTypesEntity { StatusTypeId = 1, Name = "Citas medicas" },
                new StatusTypesEntity { StatusTypeId = 2, Name = "Pagos citas medicas" },
                new StatusTypesEntity { StatusTypeId = 3, Name = "Ordenes medicas" },
                new StatusTypesEntity { StatusTypeId = 4, Name = "Solicitudes de cambio" }
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

            // Tipos de relaciones
            modelBuilder.Entity<RelationShipsEntity>().HasData(
                new RelationShipsEntity { RelationShipId = 1, Name = "Padre" },
                new RelationShipsEntity { RelationShipId = 2, Name = "Madre" },
                new RelationShipsEntity { RelationShipId = 3, Name = "Hermano/a" },
                new RelationShipsEntity { RelationShipId = 4, Name = "Tío/a" },
                new RelationShipsEntity { RelationShipId = 5, Name = "Abuelo/a" },
                new RelationShipsEntity { RelationShipId = 6, Name = "Esposo/a" },
                new RelationShipsEntity { RelationShipId = 7, Name = "Hijo/a" },
                new RelationShipsEntity { RelationShipId = 8, Name = "Primo/a" },
                new RelationShipsEntity { RelationShipId = 9, Name = "Otro" }
            );

            // Módulos
            modelBuilder.Entity<ModulesEntity>().HasData(
                new ModulesEntity { ModuleId = 1, Name = "Gestión de Usuarios", Icon = "", Route = "gestionUsuarios", Order = 1, IsActive = true },
                new ModulesEntity { ModuleId = 2, Name = "Gestión de Hospitales", Icon = "", Route = "gestionhospitales", Order = 2, IsActive = true },
                new ModulesEntity { ModuleId = 3, Name = "Citas Médicas", Icon = "", Route = "citasMedicas", Order = 3, IsActive = true },
                new ModulesEntity { ModuleId = 4, Name = "Historias Clinicas", Icon = "", Route = "historiasClinicas", Order = 4, IsActive = true },
                new ModulesEntity { ModuleId = 5, Name = "Reportes", Icon = "", Route = "reportes", Order = 5, IsActive = true },
                new ModulesEntity { ModuleId = 6, Name = "Solicitudes", Icon = "", Route = "solicitudes", Order = 6, IsActive = true },
                new ModulesEntity { ModuleId = 7, Name = "Utilitarios", Icon = "", Route = "utilitarios", Order = 7, IsActive = true },
                new ModulesEntity { ModuleId = 8, Name = "Configuración", Icon = "", Route = "configuracion", Order = 8, IsActive = true },
                new ModulesEntity { ModuleId = 9, Name = "Seguridad y Accesos", Icon = "", Route = "seguridadAcceso", Order = 9, IsActive = true },
                new ModulesEntity { ModuleId = 10, Name = "Panel personal", Icon = "", Route = "panelPersonal", Order = 10, IsActive = true }
            );

            // Submódulos
            modelBuilder.Entity<SubModulesEntity>().HasData(
                // Gestión de Usuarios
                new SubModulesEntity { SubModuleId = 1, ModuleId = 1, Name = "Lista de usuarios", Route = "usuarios", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 2, ModuleId = 1, Name = "Crear usuario", Route = "crear", Order = 2, IsActive = true },
                new SubModulesEntity { SubModuleId = 3, ModuleId = 1, Name = "Lista doctores", Route = "doctores", Order = 3, IsActive = true },
                new SubModulesEntity { SubModuleId = 4, ModuleId = 1, Name = "Vacaciones doctores", Route = "vacacionesDoctores", Order = 4, IsActive = true },

                // Gestión de Hospitales
                new SubModulesEntity { SubModuleId = 5, ModuleId = 2, Name = "Lista de hospitales", Route = "hospitales", Order = 1, IsActive = true },

                // Citas Médicas
                new SubModulesEntity { SubModuleId = 6, ModuleId = 3, Name = "Agendar cita", Route = "agendar", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 7, ModuleId = 3, Name = "Citas por paciente", Route = "porPaciente", Order = 2, IsActive = true },
                new SubModulesEntity { SubModuleId = 8, ModuleId = 3, Name = "Citas por médico", Route = "porDoctor", Order = 3, IsActive = true },
                new SubModulesEntity { SubModuleId = 9, ModuleId = 3, Name = "Citas canceladas", Route = "canceladas", Order = 4, IsActive = true },
                new SubModulesEntity { SubModuleId = 10, ModuleId = 3, Name = "Lista de citas", Route = "todas", Order = 5, IsActive = true },
                new SubModulesEntity { SubModuleId = 11, ModuleId = 3, Name = "Reprogramar cita", Route = "reprogramar", Order = 6, IsActive = true },

                // Historias Clínicas
                new SubModulesEntity { SubModuleId = 12, ModuleId = 4, Name = "Buscar historia", Route = "buscar", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 13, ModuleId = 4, Name = "Crear historia medica", Route = "buscar", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 14, ModuleId = 4, Name = "Mi historia medica", Route = "miHistoriaMedica", Order = 2, IsActive = true },

                // Reportes
                new SubModulesEntity { SubModuleId = 15, ModuleId = 5, Name = "Reportes médicos", Route = "medicos", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 16, ModuleId = 5, Name = "Reportes administrativos", Route = "administrativos", Order = 2, IsActive = true },

                // Solicitudes
                new SubModulesEntity { SubModuleId = 17, ModuleId = 6, Name = "Solicitudes cambio de hospital", Route = "cambioDeHospital", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 18, ModuleId = 6, Name = "Solicitudes de vacaciones", Route = "solicitudVacaciones", Order = 2, IsActive = true },
                new SubModulesEntity { SubModuleId = 19, ModuleId = 10, Name = "Mis solicitudes", Route = "misVacaciones", Order = 4, IsActive = true },

                // Utilitarios
                new SubModulesEntity { SubModuleId = 20, ModuleId = 7, Name = "Géneros", Route = "generos", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 21, ModuleId = 7, Name = "Tipos de documento", Route = "tiposDeIdentificacion", Order = 2, IsActive = true },
                new SubModulesEntity { SubModuleId = 22, ModuleId = 7, Name = "Especialidades", Route = "especialidades", Order = 3, IsActive = true },
                new SubModulesEntity { SubModuleId = 23, ModuleId = 7, Name = "Alergias", Route = "alergias", Order = 4, IsActive = true },
                new SubModulesEntity { SubModuleId = 24, ModuleId = 7, Name = "Tipos de sangre", Route = "tiposDeSangre", Order = 5, IsActive = true },
                new SubModulesEntity { SubModuleId = 25, ModuleId = 7, Name = "Enfermedades", Route = "enfermedades", Order = 6, IsActive = true },
                new SubModulesEntity { SubModuleId = 26, ModuleId = 7, Name = "Tipos de estado", Route = "tiposDeEstado", Order = 7, IsActive = true },
                new SubModulesEntity { SubModuleId = 27, ModuleId = 7, Name = "Municipios", Route = "municipios", Order = 8, IsActive = true },
                new SubModulesEntity { SubModuleId = 28, ModuleId = 7, Name = "Tipos de órdenes", Route = "tiposDeOrdenes", Order = 9, IsActive = true },
                new SubModulesEntity { SubModuleId = 29, ModuleId = 7, Name = "Tipos de pago", Route = "tiposDePago", Order = 10, IsActive = true },
                new SubModulesEntity { SubModuleId = 30, ModuleId = 7, Name = "Estados", Route = "estados", Order = 11, IsActive = true },

                // Configuración
                new SubModulesEntity { SubModuleId = 31, ModuleId = 8, Name = "Asignación de horarios", Route = "asignarHorarios", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 32, ModuleId = 8, Name = "Asignación de especialidades", Route = "asignarEspecialidades", Order = 2, IsActive = true },

                // Seguridad y Accesos
                new SubModulesEntity { SubModuleId = 33, ModuleId = 9, Name = "Lista de roles", Route = "roles", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 34, ModuleId = 9, Name = "Lista de módulos", Route = "modulos", Order = 2, IsActive = true },
                new SubModulesEntity { SubModuleId = 35, ModuleId = 9, Name = "Lista de submódulos", Route = "subModulos", Order = 3, IsActive = true },

                // Panel personal
                new SubModulesEntity { SubModuleId = 36, ModuleId = 10, Name = "Mi perfil", Route = "perfil", Order = 1, IsActive = true },
                new SubModulesEntity { SubModuleId = 37, ModuleId = 10, Name = "Mi agenda", Route = "miAgenda", Order = 2, IsActive = true },
                new SubModulesEntity { SubModuleId = 38, ModuleId = 10, Name = "Mis pacientes", Route = "misPacientes", Order = 3, IsActive = true },
                new SubModulesEntity { SubModuleId = 39, ModuleId = 10, Name = "Solicitar vacaciones", Route = "misVacaciones", Order = 4, IsActive = true }
            );

            // Permisos
            modelBuilder.Entity<PermissionsEntity>().HasData(
                new PermissionsEntity { PermissionId = 1, Name = "Ver", Code = "VIEW" },
                new PermissionsEntity { PermissionId = 2, Name = "Crear", Code = "CREATE" },
                new PermissionsEntity { PermissionId = 3, Name = "Editar", Code = "EDIT" },
                new PermissionsEntity { PermissionId = 4, Name = "Eliminar", Code = "DELETE" },
                new PermissionsEntity { PermissionId = 5, Name = "Exportar", Code = "EXPORT" }
            );
        }
    }
}
