using Microsoft.EntityFrameworkCore;
using SpotKapasite.Domain.Entities;
using SpotKapasite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotKapasite.Infrastructure.Repositories
{
    public class KapasiteRepository : IKapasiteRepository
    {
        private readonly KapasiteDbContext _context;

        public KapasiteRepository(KapasiteDbContext context)
        {
            _context = context;
        }

        public async Task<List<Kapasite>> GetAllAsync()
        {
            try
            {
                return await _context.Kapasiteler.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Veritabanından tüm veriler alınırken bir hata oluştu.", ex);
            }
        }

        public async Task<Kapasite?> GetByIdAsync(int id)
        {
            try
            {
                var kapasite = await _context.Kapasiteler.FindAsync(id);
                if (kapasite == null)
                {
                    throw new KeyNotFoundException($"ID '{id}' ile eşleşen kapasite bulunamadı.");
                }

                return kapasite;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Kapasite verisi alınırken bir hata oluştu.", ex);
            }
        }

        public async Task AddAsync(Kapasite kapasite)
        {
            try
            {
                await _context.Kapasiteler.AddAsync(kapasite);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Kapasite eklenirken veritabanı hatası oluştu.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Kapasite eklenirken bir hata oluştu.", ex);
            }
        }

        public async Task UpdateAsync(Kapasite kapasite)
        {
            try
            {
                _context.Kapasiteler.Update(kapasite);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Kapasite güncellenirken veritabanı hatası oluştu.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Kapasite güncellenirken bir hata oluştu.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var kapasite = await _context.Kapasiteler.FindAsync(id);
                if (kapasite != null)
                {
                    _context.Kapasiteler.Remove(kapasite);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"ID '{id}' ile eşleşen kapasite bulunamadı.");
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Kapasite silinirken veritabanı hatası oluştu.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Kapasite silinirken bir hata oluştu.", ex);
            }
        }
    }
}
