using SpotKapasite.Application.Interfaces;
using SpotKapasite.Domain.Entities;
using SpotKapasite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotKapasite.Application.Services
{
    public class KapasiteSeederService : IKapasiteSeederService
    {
        private readonly IKapasiteRepository _repository;

        public KapasiteSeederService(IKapasiteRepository repository)
        {
            _repository = repository;
        }

        public async Task SeedDataAsync()
        {
            // Get all kapasites from the repository
            var kapasiteler = await _repository.GetAllAsync();

            // If no data is found, load from the JSON file and insert it into the repository
            if (kapasiteler == null || !kapasiteler.Any())
            {
                var jsonFilePath = "Data/spot.json";  // Adjust the file path based on your project setup
                var jsonData = File.ReadAllText(jsonFilePath);

                // Deserialize JSON data to List of Kapasite
                var seedData = JsonSerializer.Deserialize<List<Kapasite>>(jsonData);

                // If data is valid, insert it into the repository
                if (seedData != null)
                {
                    foreach (var kapasite in seedData)
                    {
                        await _repository.AddAsync(kapasite);
                    }
                }
            }
        }
    }
}
