using Microsoft.AspNetCore.Mvc;
using PTirta.DTOs;

namespace PTirta.Controllers.Api
{
    [ApiController]
    [Route("api/v1/services")]
    [Produces("application/json")]
    public class ServicesApiController : ControllerBase
    {
        private static readonly List<ServiceDto> ServicesData = new()
        {
            new ServiceDto
            {
                Id = 1,
                Title = "Analisa Hidrologi dan Hidroklimatologi",
                Description = "Studi komprehensif terkait fluktuasi curah hujan harian, intensitas hujan jam-jaman, debit andalan sungai, dan neraca air wilayah DAS.",
                Category = "perencanaan",
                CategoryLabel = "Perencanaan Teknis",
                IconClass = "bi bi-cloud-rain-heavy",
                ImageUrl = "/assets/img/alt-services.jpg"
            },
            new ServiceDto
            {
                Id = 2,
                Title = "Analisa & Pengendalian Banjir",
                Description = "Pemodelan hidraulika genangan banjir, pemetaan risiko daerah rawan, serta perumusan struktur pengendali banjir terpadu.",
                Category = "perencanaan",
                CategoryLabel = "Perencanaan Teknis",
                IconClass = "bi bi-water",
                ImageUrl = "/assets/img/features-2.jpg"
            },
            new ServiceDto
            {
                Id = 3,
                Title = "Perencanaan Flood Early Warning System",
                Description = "Desain dan arsitektur sistem peringatan dini banjir otomatis berbasis telemetri real-time untuk mitigasi bencana kawasan hilir.",
                Category = "perencanaan",
                CategoryLabel = "Perencanaan Teknis",
                IconClass = "bi bi-broadcast",
                ImageUrl = "/assets/img/features-3.jpg"
            },
            new ServiceDto
            {
                Id = 4,
                Title = "Perencanaan Bangunan Air & Irigasi",
                Description = "Desain teknis (DED) bendung, embung penampung, tanggul penahan banjir, saluran pelimpah, serta jaringan irigasi primer hingga tersier.",
                Category = "perencanaan",
                CategoryLabel = "Perencanaan Teknis",
                IconClass = "bi bi-building",
                ImageUrl = "/assets/img/constructions-1.jpg"
            },
            new ServiceDto
            {
                Id = 5,
                Title = "Penyusunan Kelengkapan SIPPA / Rekomtek",
                Description = "Penyusunan berkas teknis permohonan Rekomendasi Teknis (Rekomtek) dan Surat Izin Pengambilan dan Pemanfaatan Air (SIPPA).",
                Category = "kajian",
                CategoryLabel = "Kajian & Perizinan",
                IconClass = "bi bi-file-earmark-check",
                ImageUrl = "/assets/img/features-1.jpg"
            },
            new ServiceDto
            {
                Id = 6,
                Title = "Penyusunan Manual Operasi & Pemeliharaan",
                Description = "Penyusunan pedoman baku operasional pintu air, embung, waduk, pompa banjir, dan pemeliharaan berkala infrastruktur SDA.",
                Category = "op",
                CategoryLabel = "Operasi & Pemeliharaan",
                IconClass = "bi bi-journal-bookmark",
                ImageUrl = "/assets/img/kerja4.jpg"
            },
            new ServiceDto
            {
                Id = 7,
                Title = "Penyusunan Dokumen AKNOP",
                Description = "Perhitungan Analisis Kebutuhan Nyata Operasi dan Pemeliharaan (AKNOP) untuk estimasi anggaran berkelanjutan sarana dan prasarana.",
                Category = "op",
                CategoryLabel = "Operasi & Pemeliharaan",
                IconClass = "bi bi-calculator",
                ImageUrl = "/assets/img/constructions-3.jpg"
            },
            new ServiceDto
            {
                Id = 8,
                Title = "Kajian Biaya Jasa Pengelolaan SDA (BJPSDA)",
                Description = "Kajian tarif dan analisis pembiayaan rasional dalam pemanfaatan serta konservasi sumber daya air antar pemangku kepentingan.",
                Category = "kajian",
                CategoryLabel = "Kajian & Perizinan",
                IconClass = "bi bi-cash-coin",
                ImageUrl = "/assets/img/services.jpg"
            },
            new ServiceDto
            {
                Id = 9,
                Title = "Penyusunan Dokumen Lingkungan (AMDAL / UKL-UPL)",
                Description = "Studi dampak lingkungan dan dokumen pengelolaan serta pemantauan lingkungan hidup untuk proyek-proyek infrastruktur sipil keairan.",
                Category = "kajian",
                CategoryLabel = "Kajian & Perizinan",
                IconClass = "bi bi-tree",
                ImageUrl = "/assets/img/about.jpg"
            },
            new ServiceDto
            {
                Id = 10,
                Title = "Penyusunan LARAP (Pengadaan Tanah & Pemukiman)",
                Description = "Dokumen Land Acquisition and Resettlement Action Plan untuk mitigasi sosial dan kepastian hukum pelepasan lahan pembangunan.",
                Category = "kajian",
                CategoryLabel = "Kajian & Perizinan",
                IconClass = "bi bi-geo-alt",
                ImageUrl = "/assets/img/constructions-2.jpg"
            },
            new ServiceDto
            {
                Id = 11,
                Title = "Pelatihan & Capacity Development",
                Description = "Peningkatan kompetensi staf teknis pemerintah dan swasta dalam pemodelan hidrologi, GIS keairan, serta operasi sarana SDA.",
                Category = "op",
                CategoryLabel = "Pelatihan & SDM",
                IconClass = "bi bi-mortarboard",
                ImageUrl = "/assets/img/features-4.jpg"
            }
        };

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? category, [FromQuery] string? search)
        {
            var query = ServicesData.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(category) && category.ToLower() != "all")
            {
                query = query.Where(s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         s.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(query.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var service = ServicesData.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound(new { message = $"Layanan dengan ID {id} tidak ditemukan." });
            }
            return Ok(service);
        }
    }
}

