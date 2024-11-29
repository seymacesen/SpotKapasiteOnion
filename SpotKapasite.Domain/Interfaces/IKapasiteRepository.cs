using SpotKapasite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotKapasite.Domain.Interfaces
{
    public interface IKapasiteRepository
    {
        Task<List<Kapasite>> GetAllAsync();
        Task<Kapasite> GetByIdAsync(int id);
        Task AddAsync(Kapasite kapasite);
        Task UpdateAsync(Kapasite kapasite);
        Task DeleteAsync(int id);
    }
}
