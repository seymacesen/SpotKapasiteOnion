using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotKapasite.Domain.Entities
{
    public class Kapasite
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("kapasite_adi")]
        public string KapasiteAdi { get; set; }

        [JsonPropertyName("lisans_no")]
        public string LisansNo { get; set; }

        [JsonPropertyName("nokta_kodu")]
        public string NoktaKodu { get; set; }

        [JsonPropertyName("kurumadi")]
        public string KurumAdi { get; set; }

        [JsonPropertyName("noktaadi")]
        public string NoktaAdi { get; set; }

        [JsonPropertyName("kapasite")]
        public decimal KapasiteMiktari { get; set; }

        [JsonPropertyName("fiyat")]
        public decimal Fiyat { get; set; }

        [JsonPropertyName("donem_tur")]
        public string DonemTur { get; set; }

        [JsonPropertyName("ay")]
        public int Ay { get; set; }

        [JsonPropertyName("yil")]
        public int Yil { get; set; }
    }
}
