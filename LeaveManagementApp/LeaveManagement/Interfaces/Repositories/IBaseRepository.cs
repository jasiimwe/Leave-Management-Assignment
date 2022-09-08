using System;
namespace LeaveManagement.Interfaces.Repositories
{
    public interface IBaseRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAll();

        Task<T1> GetById(T2 id);

        Task InsertAsync(T1 entity);

        void Delete(T1 entity);

        void Update(T1 entity);
    }
}

