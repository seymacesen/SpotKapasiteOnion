using SpotKapasite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotKapasite.Application.Interfaces
{
    public interface IKapasiteService
    {
        Task<List<Kapasite>> GetAllAsync();
        Task AddAsync(Kapasite kapasite);
        Task<Kapasite?> GetByIdAsync(int id);
        Task<List<Kapasite>> FilterByDateAsync(int ay, int yil);
        Task<decimal> GetTotalCapacityAsync(int ay, int yil);
        Task<List<Kapasite>> GetByKurumAdiAsync(string kurumAdi);
        Task UpdateAsync(Kapasite kapasite);
        Task DeleteAsync(int id);
    }
}
