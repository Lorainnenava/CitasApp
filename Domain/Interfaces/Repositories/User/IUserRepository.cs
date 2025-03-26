using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Infrastructure.User
{
    public interface IUserRepository
    {
        Task<UserModel> Create(UserModel request);
        Task<UserModel> GetByCondition(Expression<Func<UserModel, bool>> condition);
        Task<IEnumerable<UserModel>> GetAll(Expression<Func<UserModel, bool>>? condition = null);
        Task<UserModel> Update(Expression<Func<UserModel, bool>> condition, UserModel request);
        Task<bool> Delete(Expression<Func<UserModel, bool>> condition);
    }
}
