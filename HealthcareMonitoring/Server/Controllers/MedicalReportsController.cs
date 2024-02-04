using HealthcareMonitoring.Server.IRepository;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareMonitoring.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalReportsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly ApplicationDbContext _context;

        public MedicalReportsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/MedicalReports
        [HttpGet]
        public async Task<IActionResult> GetMedicalReports()
        {
            var medicalreports = await _unitOfWork.MedicalReports.GetAll();
            return Ok(medicalreports);
        }

        // GET: api/MedicalReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalReport>> GetMedicalReport(int id)
        {

            var medicalreport = await _unitOfWork.MedicalReports.Get(q => q.Id == id);

            if (medicalreport == null)
            {
                return NotFound();
            }

            return Ok(medicalreport);
        }
        // PUT: api/MedicalReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalReport(int id, MedicalReport medicalreport)
        {
            if (id != medicalreport.Id)
            {
                return BadRequest();
            }

            _unitOfWork.MedicalReports.Update(medicalreport);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MedicalReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MedicalReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedicalReport>> PostMedicalReport(MedicalReport medicalreport)
        {
            medicalreport.Id = default;
            await _unitOfWork.MedicalReports.Insert(medicalreport);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetMedicalReport", new { id = medicalreport.Id }, medicalreport);
        }

        // DELETE: api/MedicalReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalReport(int id)
        {
            var medicalreport = await _unitOfWork.MedicalReports.Get(q => q.Id == id);
            if (medicalreport == null)
            {
                return NotFound();
            }
            await _unitOfWork.MedicalReports.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();

        }

        private async Task<bool> MedicalReportExists(int id)
        {
            var medicalreport = await _unitOfWork.MedicalReports.Get(q => q.Id == id);
            return medicalreport != null;
        }
    }
}