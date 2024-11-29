using Microsoft.EntityFrameworkCore;
using SpotKapasite.Application.Interfaces;
using SpotKapasite.Domain.Entities;
using SpotKapasite.Domain.Interfaces;
using SpotKapasite.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotKapasite.Application.Services
{
    public class KapasiteService : IKapasiteService
    {
        private readonly IKapasiteRepository _repository;

        public KapasiteService(IKapasiteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Kapasite>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Veriler alınırken bir hata oluştu.", ex);
            }
        }

        public async Task<Kapasite?> GetByIdAsync(int id)
        {
            try
            {
                var kapasite = await _repository.GetByIdAsync(id);
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

        public async Task<List<Kapasite>> FilterByDateAsync(int ay, int yil)
        {
            try
            {
                var allKapasiteler = await _repository.GetAllAsync();
                var result = allKapasiteler
                    .Where(k => k.Ay == ay && k.Yil == yil)
                    .ToList();

                if (!result.Any())
                {
                    throw new Exception($"Ay: {ay} ve Yıl: {yil} için veri bulunamadı.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Filtreleme işlemi sırasında bir hata oluştu.", ex);
            }
        }

        public async Task AddAsync(Kapasite kapasite)
        {
            try
            {
                if (kapasite.KapasiteMiktari <= 0)
                {
                    throw new ArgumentException("Kapasite miktarı sıfırdan büyük olmalıdır.");
                }

                await _repository.AddAsync(kapasite);
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Kapasite eklenirken bir hata oluştu.", ex);
            }
        }

        public async Task<decimal> GetTotalCapacityAsync(int ay, int yil)
        {
            try
            {
                var allKapasiteler = await _repository.GetAllAsync();
                var totalCapacity = allKapasiteler
                    .Where(k => k.Ay == ay && k.Yil == yil)
                    .Sum(k => k.KapasiteMiktari);

                if (totalCapacity == 0)
                {
                    throw new Exception($"Ay: {ay} ve Yıl: {yil} için toplam kapasite bulunamadı.");
                }

                return totalCapacity;
            }
            catch (Exception ex)
            {
                throw new Exception("Toplam kapasite hesaplanırken bir hata oluştu.", ex);
            }
        }

        public async Task<List<Kapasite>> GetByKurumAdiAsync(string kurumAdi)
        {
            try
            {
                var allKapasiteler = await _repository.GetAllAsync();
                var result = allKapasiteler
                    .Where(k => k.KurumAdi.Contains(kurumAdi))
                    .ToList();

                if (!result.Any())
                {
                    throw new Exception($"Kurum adı '{kurumAdi}' ile eşleşen veri bulunamadı.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Kurum adına göre veri filtreleme işlemi sırasında bir hata oluştu.", ex);
            }
        }

        public async Task UpdateAsync(Kapasite kapasite)
        {
            if (kapasite == null || string.IsNullOrWhiteSpace(kapasite.NoktaAdi))
                throw new ValidationException("Geçersiz Kapasite verisi");

            try
            {
                await _repository.UpdateAsync(kapasite);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Veritabanı güncelleme işlemi başarısız oldu.", ex);
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
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"ID: {id} olan kapasite silinirken bir hata oluştu.", ex);
            }
        }
    }
}
