using System.Security.Claims;
using HealthcareMonitoring.Server.IRepository;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareMonitoring.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly ApplicationDbContext _context;

        public PatientsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var Patients = await _unitOfWork.Patients.GetAll();
            return Ok(Patients);
        }



		// GET: api/Patients/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Getpatient(int id)
        {

            var patient = await _unitOfWork.Patients.Get(q => q.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Patients.Update(patient);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await patientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> Postpatient(Patient patient)
        {
            await _unitOfWork.Patients.Insert(patient);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("Getpatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepatient(int id)
        {
            var patient = await _unitOfWork.Patients.Get(q => q.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            await _unitOfWork.Patients.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();

        }

        private async Task<bool> patientExists(int id)
        {
            var patient = await _unitOfWork.Patients.Get(q => q.Id == id);
            return patient != null;
        }
    }
}