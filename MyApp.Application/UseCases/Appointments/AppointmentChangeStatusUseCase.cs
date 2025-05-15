using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Appointments;
using MyApp.Application.Enums;
using MyApp.Application.Interfaces.UseCases.Appointments;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Appointments
{
    public class AppointmentChangeStatusUseCase : IAppointmentChangeStatusUseCase
    {
        private readonly IGenericRepository<AppointmentsEntity> _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentChangeStatusUseCase> _logger;

        public AppointmentChangeStatusUseCase(
            IGenericRepository<AppointmentsEntity> appointmentRepository,
            IMapper mapper,
            ILogger<AppointmentChangeStatusUseCase> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentResponse> Execute(int StatusId, int AppointmentId)
        {
            _logger.LogInformation("Iniciando el cambio de estado de una cita con el ID: {AppointmentId}", AppointmentId);

            if (StatusId < 8 || StatusId > 12)
            {
                _logger.LogWarning("El valor del statusId {StatusId} debe estar entre un rango de 8 y 12", StatusId);
                throw new InvalidDataException("El valor de 'StatusId' no es válido. Debe estar entre 8 y 12 inclusive.");
            }

            var changeStatus = await _appointmentRepository.GetByCondition(x => x.AppointmentId == AppointmentId);

            if (changeStatus is null)
            {
                _logger.LogWarning("No se encontró ninguna cita con el ID: {AppointmentId}", AppointmentId);
                throw new NotFoundException("No se encontro la cita solicitada.");
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var now = TimeOnly.FromDateTime(DateTime.UtcNow);

            if (changeStatus.AppointmentDate > today ||
                (changeStatus.AppointmentDate == today && changeStatus.AppointmentTime > now) &&
                (StatusId != (int)StatusIdEnum.Reprogramada || StatusId != (int)StatusIdEnum.NoPresentada))
            {
                _logger.LogWarning("Solo se puede reprogramar o marcar como 'No presentada' una cita pasada.");
                throw new ConflictException("Solo se puede reprogramar o marcar como 'No presentada' una cita pasada.");
            }

            if (changeStatus.StatusId == StatusId)
            {
                _logger.LogWarning("La cita ya tiene el estado especificado. No se realizaron cambios.");
                throw new ConflictException("La cita ya tiene el estado al que se quiere actualizar.");
            }

            var dateWithTime = DateOnly.FromDateTime(DateTime.UtcNow);
            var appointmentDateTime = changeStatus.AppointmentDate.ToDateTime(changeStatus.AppointmentTime);

            if (StatusId == (int)StatusIdEnum.Cancelada && (appointmentDateTime - DateTime.UtcNow).TotalHours < 2)
            {
                _logger.LogWarning("La cita no puede ser cancelada ya que faltan menos de 2 horas para ella.");
                throw new ConflictException("No se puede cancelar la cita porque faltan menos de 2 horas.");
            }

            if (changeStatus.StatusId == (int)StatusIdEnum.Cancelada
                && (StatusId == (int)StatusIdEnum.Pendiente ||
                StatusId == (int)StatusIdEnum.Confirmada ||
                StatusId == (int)StatusIdEnum.Reprogramada ||
                StatusId == (int)StatusIdEnum.NoPresentada))
            {
                _logger.LogWarning("Una cita cancelada no se le puede cambiar el estado");
                throw new ConflictException("No se cambiar el estado de una cita cancelada.");
            }

            if (changeStatus.StatusId == (int)StatusIdEnum.NoPresentada
                && (StatusId == (int)StatusIdEnum.Pendiente
                || StatusId == (int)StatusIdEnum.Confirmada
                || StatusId == (int)StatusIdEnum.Cancelada
                || StatusId == (int)StatusIdEnum.Reprogramada))
            {
                throw new ConflictException("No se cambiar el estado de una cita que no fue presentada.");
            }

            changeStatus.StatusId = StatusId;
            changeStatus.UpdatedAt = DateTime.UtcNow;

            await _appointmentRepository.Update(x => x.AppointmentId == AppointmentId, changeStatus, changeStatus);

            _logger.LogInformation("Estado de la cita actualizado correctamente. ID: {AppointmentId}, Nuevo Estado: {StatusId}", AppointmentId, StatusId);

            return _mapper.Map<AppointmentResponse>(changeStatus);
        }
    }
}
