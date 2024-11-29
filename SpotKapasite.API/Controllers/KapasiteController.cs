using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotKapasite.Application.Interfaces;
using SpotKapasite.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SpotKapasite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KapasiteController : ControllerBase
    {
        private readonly IKapasiteService _kapasiteService;

        public KapasiteController(IKapasiteService kapasiteService)
        {
            _kapasiteService = kapasiteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Tüm verileri almak için service metodunu çağırıyoruz
                var result = await _kapasiteService.GetAllAsync();

                // Eğer sonuç null veya boş dönerse, NotFound döndürülür
                if (result == null || !result.Any())
                {
                    return NotFound("Hiç veri bulunamadı.");
                }

                // Eğer işlem başarılıysa, Ok döndürülür
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Hata oluşursa, 500 Internal Server Error döndürülür ve hata mesajı iletilir
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                // Veritabanından belirtilen ID ile kapasiteyi alıyoruz
                var kapasite = await _kapasiteService.GetByIdAsync(id);

                // Eğer kapasite bulunamazsa, NotFound döndürülür
                if (kapasite == null)
                {
                    return NotFound($"ID '{id}' ile eşleşen kapasite bulunamadı.");
                }

                // Eğer işlem başarılıysa, Ok döndürülür
                return Ok(kapasite);
            }
            catch (Exception ex)
            {
                // Hata oluşursa, 500 Internal Server Error döndürülür ve hata mesajı iletilir
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Kapasite kapasite)
        {
            if (kapasite is null)
            {
                throw new ArgumentNullException(nameof(kapasite));
            }

            try
            {
                // Yeni kapasite ekleme işlemi
                await _kapasiteService.AddAsync(kapasite);

                // Başarılı bir şekilde eklendiyse, CreatedAtAction ile başarı durumu döndürülür
                return CreatedAtAction(nameof(GetById), new { id = kapasite.Id }, kapasite);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Hata oluşursa, 500 Internal Server Error döndürülür ve hata mesajı iletilir
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("filterByDate")]
        public async Task<IActionResult> FilterByDate(int ay, int yil)
        {
            try
            {
                // Filtreleme işlemini yapıyoruz
                var result = await _kapasiteService.FilterByDateAsync(ay, yil);

                // Eğer sonuç null veya boş dönerse, NotFound döndürebiliriz
                if (result == null || !result.Any())
                {
                    return NotFound($"Belirtilen ay ({ay}) ve yıl ({yil}) için veri bulunamadı.");
                }

                // Başarılı işlemde 200 OK döndürülür
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Hata oluşursa, 500 Internal Server Error döndürülür ve hata mesajı iletilir
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }


        [HttpGet("total-capacity")]
        public async Task<IActionResult> GetTotalCapacity(int ay, int yil)
        {
            try
            {
                // İşlem başarılı ise sonucu döndür
                var result = await _kapasiteService.GetTotalCapacityAsync(ay, yil);

                // Eğer sonuç null veya boş dönerse, NotFound döndürebiliriz
                if (result == null || result <= 0)
                {
                    return NotFound($"Belirtilen ay ({ay}) ve yıl ({yil}) için kapasite bulunamadı.");
                }

                return Ok(result); // 200 OK döndürülür
            }
            catch (Exception ex)
            {
                // Hata durumunda genel bir hata mesajı döndürülür.
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }


        [HttpGet("ByKurumAdi/{kurumAdi}")]
        public async Task<ActionResult<List<Kapasite>>> GetByKurumAdiAsync(string kurumAdi)
        {
            try
            {
                // Kurum adına göre kapasiteyi alıyoruz
                var result = await _kapasiteService.GetByKurumAdiAsync(kurumAdi);

                // Eğer veri bulunmazsa, NotFound döndürülür
                if (result == null || !result.Any())
                {
                    return NotFound($"Kurum adı '{kurumAdi}' ile eşleşen veri bulunamadı.");
                }

                // Eğer işlem başarılıysa, OK döndürülür
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Hata oluşursa, 500 Internal Server Error döndürülür ve hata mesajı iletilir
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Kapasite kapasite)
        {
            try
            {
                if (id != kapasite.Id)
                {
                    return BadRequest("ID eşleşmedi");
                }

                await _kapasiteService.UpdateAsync(kapasite);
                return NoContent(); // 204 No Content
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _kapasiteService.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
