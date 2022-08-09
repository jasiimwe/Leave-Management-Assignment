using System;
namespace LeaveManagement.Models.Repository
{
    public interface IRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAll();

        Task<T1> GetById(T2 id);

        Task<T1> Insert(T1 entity);

        Task Delete(T2 id);

        Task Update(T1 entity);

    }
}
