using Microsoft.AspNetCore.Mvc;
using PTirta.DTOs;

namespace PTirta.Controllers.Api
{
    [ApiController]
    [Route("api/v1/portfolios")]
    [Produces("application/json")]
    public class PortfoliosApiController : ControllerBase
    {
        private static readonly List<PortfolioDto> PortfolioData = new()
        {
            new PortfolioDto
            {
                Id = 1,
                Title = "DED Pengendalian Banjir & Normalisasi Sungai",
                Description = "Perencanaan tanggul pelindung, perkuatan tebing sungai, dan pemodelan hidraulika genangan 2 dimensi untuk mengurangi risiko limpasan di kawasan permukiman.",
                Category = "banjir",
                CategoryLabel = "Pengendalian Banjir",
                Location = "Jawa Barat",
                Year = "2023",
                Status = "Selesai",
                ImageUrl = "/assets/img/hero-carousel/hero-carousel-1.jpg"
            },
            new PortfolioDto
            {
                Id = 2,
                Title = "Perencanaan Detail Desain (DED) Embung Konservasi",
                Description = "Kajian geoteknik, hidrologi tampungan, dan desain teknis embung multifungsi guna menampung limpasan air hujan untuk pasokan irigasi dan konservasi air tanah.",
                Category = "sda",
                CategoryLabel = "Perencanaan SDA",
                Location = "Jawa Tengah",
                Year = "2023",
                Status = "Selesai",
                ImageUrl = "/assets/img/hero-carousel/hero-carousel-2.jpg"
            },
            new PortfolioDto
            {
                Id = 3,
                Title = "Perencanaan Flood Early Warning System (FEWS)",
                Description = "Implementasi sensor level air dan curah hujan berbasis telemetri GSM/IoT, dilengkapi algoritma peringatan dini banjir bagi masyarakat hilir sungai.",
                Category = "banjir",
                CategoryLabel = "Pengendalian Banjir",
                Location = "Banten",
                Year = "2024",
                Status = "Selesai",
                ImageUrl = "/assets/img/hero-carousel/hero-carousel-3.jpg"
            },
            new PortfolioDto
            {
                Id = 4,
                Title = "Kajian Neraca Air & Dokumen Rekomtek SIPPA",
                Description = "Analisis ketersediaan air andalan permukaan tanah, debit pemanfaatan pabrik, serta penyusunan kelengkapan teknis izin pemanfaatan sumber daya air resmi.",
                Category = "kajian",
                CategoryLabel = "Kajian & Perizinan",
                Location = "Karawang",
                Year = "2024",
                Status = "Selesai",
                ImageUrl = "/assets/img/constructions-1.jpg"
            },
            new PortfolioDto
            {
                Id = 5,
                Title = "Master Plan Drainase & Kolam Retensi Perkotaan",
                Description = "Kajian komprehensif alur drainase primer dan sekunder perkotaan, didukung integrasi kolam retensi serta sumur resapan guna memitigasi genangan jalan.",
                Category = "irigasi",
                CategoryLabel = "Drainase & Irigasi",
                Location = "Kota Bandung",
                Year = "2022",
                Status = "Selesai",
                ImageUrl = "/assets/img/constructions-2.jpg"
            },
            new PortfolioDto
            {
                Id = 6,
                Title = "Penyusunan AKNOP & Manual OP Daerah Irigasi",
                Description = "Inventarisasi kondisi fisik saluran irigasi, estimasi biaya kebutuhan nyata operasi & pemeliharaan (AKNOP), serta penyusunan pedoman manual OP bagi juru pengairan.",
                Category = "irigasi",
                CategoryLabel = "Drainase & Irigasi",
                Location = "Jawa Barat",
                Year = "2023",
                Status = "Selesai",
                ImageUrl = "/assets/img/features-2.jpg"
            }
        };

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? category, [FromQuery] string? search)
        {
            var query = PortfolioData.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(category) && category.ToLower() != "all")
            {
                query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         p.Description.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         p.Location.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(query.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = PortfolioData.FirstOrDefault(p => p.Id == id);
            if (item == null)
            {
                return NotFound(new { message = $"Proyek dengan ID {id} tidak ditemukan." });
            }
            return Ok(item);
        }
    }
}

