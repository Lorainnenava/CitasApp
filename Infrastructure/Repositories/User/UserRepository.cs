using Domain.Entities.User;
using Domain.Interfaces.Infrastructure.User;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.User
{
    /// <summary>
    /// Repositorio para operaciones CRUD sobre el modelo a trabajar.
    /// </summary>
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="dbContext">Contexto de la base de datos para interactuar con la entidad a trabajar.</param>
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Crea un nueva entidad en la base de datos.
        /// </summary>
        /// <param name="request">El modelo que contiene los datos de la entidad a crear.</param>
        /// <returns>La entidad recien creada.</returns>
        public async Task<UserModel> Create(UserModel request)
        {
            // Agrega la entidad
            var entityToCreate = await _dbContext.Users.AddAsync(request);

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();

            // Devolver la entidad creada
            return entityToCreate.Entity;
        }

        /// <summary>
        /// Elimina una entidad de la base de datos basado en una condición.
        /// </summary>
        /// <param name="condition">La condición para encontrar la entidad a eliminar.</param>
        /// <returns>True si la entidad fue eliminada, false si no se encontró la entidad.</returns>
        public async Task<bool> Delete(Expression<Func<UserModel, bool>> condition)
        {
            // Buscar la entidad a eliminar
            var entityToDelete = await _dbContext.Users.FirstOrDefaultAsync(condition);

            // Verificar si la entidad existe
            if (entityToDelete == null)
            {
                // Si no se encuentra el elemento, devolver false
                return false;
            }

            // Eliminar la entidad
            _dbContext.Users.Remove(entityToDelete);

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();

            // Devolver true si la eliminación fue exitosa
            return true;
        }

        /// <summary>
        /// Obtiene todas las entidades que cumplan con una condición opcional.
        /// </summary>
        /// <param name="condition">Una condición opcional para filtrar las entidades.</param>
        /// <returns>Una lista de entidades que cumplen con la condición.</returns>
        public async Task<IEnumerable<UserModel>> GetAll(Expression<Func<UserModel, bool>>? condition = null)
        {
            // Si la condición no es nula, aplicarla en la consulta
            var query = _dbContext.Users.AsQueryable();

            // Si la condición no es nula se aplica en la consulta
            if (condition != null)
            {
                query = query.Where(condition);
            }

            // Obtener el resultado de la consulta de manera asincrónica
            var result = await query.ToListAsync();

            // Retorna la lista de resultados
            return result;
        }

        /// <summary>
        /// Busca una entidad basada en una condición específica.
        /// </summary>
        /// <param name="condition">La condición para buscar la entidad.</param>
        /// <returns>El objeto encontrado.</returns>
        /// <exception cref="Exception">Lanza una excepción si la entidad no es encontrada.</exception>
        public async Task<UserModel> GetByCondition(Expression<Func<UserModel, bool>> condition)
        {
            // Buscar la entidad a eliminar
            var entityToSearch = await _dbContext.Users.FirstOrDefaultAsync(condition);

            // Retorna el elemento buscado si existe, de lo contrario arroja un error
            return entityToSearch ?? throw new Exception("User not found");
        }

        /// <summary>
        /// Actualiza una entidad basada en una condición específica.
        /// </summary>
        /// <param name="condition">La condición para encontrar la entidad a actualizar.</param>
        /// <param name="request">El modelo con los nuevos datos.</param>
        /// <returns>El objeto actualizado.</returns>
        /// <exception cref="Exception">Lanza una excepción si la entidad no es encontrada.</exception>
        public async Task<UserModel> Update(Expression<Func<UserModel, bool>> condition, UserModel request)
        {
            // Buscar la entidad a actualizar
            var entityToUpdate = await _dbContext.Users.FirstOrDefaultAsync(condition);

            // Verificar si la entidad existe
            if (entityToUpdate == null)
            {
                throw new Exception("User not found");
            }

            // Actualizar los valores de la entidad con los nuevos valores
            _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(request);

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();

            // Retornar la entidad actualizado
            return entityToUpdate;
        }
    }
}
