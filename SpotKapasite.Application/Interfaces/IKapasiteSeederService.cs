using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotKapasite.Application.Interfaces
{
    public interface IKapasiteSeederService
    {
        Task SeedDataAsync();
    }
}
