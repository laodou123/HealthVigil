using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareMonitoring.Server.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthcareMonitoring.Server.Data;
using HealthcareMonitoring.Server.IRepository;
using HealthcareMonitoring.Shared.Domain;

namespace HealthcareMonitoring.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedRDailiesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly ApplicationDbContext _context;

        public MedRDailiesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/MedRDailies
        [HttpGet]
        public async Task<IActionResult> GetMedRDailies()
        {
            var MedRDailies = await _unitOfWork.MedRDaily.GetAll();
            return Ok(MedRDailies);
        }

        // GET: api/MedRDailies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedRDaily>> GetMedRDaily(int id)
        {

            var MedRDaily = await _unitOfWork.MedRDaily.Get(q => q.Id == id);

            if (MedRDaily == null)
            {
                return NotFound();
            }

            return Ok(MedRDaily);
        }
		// GET: api/MedRDailiess/Patient/1
		[HttpGet("Patient/{id}")]
		public async Task<IActionResult> GetPatientPrescriptions(int id)
		{
			List<MedRDaily> patprescription = new List<MedRDaily>();
			var MedRDailiess = await _unitOfWork.MedRDaily.GetAll();
			foreach (var app in MedRDailiess)
			{
				if (app.PatientId == id)
				{
					patprescription.Add(app);
				}
			}
			return Ok(patprescription);
		}
		// PUT: api/MedRDailies/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutMedRDaily(int id, MedRDaily MedRDaily)
        {
            if (id != MedRDaily.Id)
            {
                return BadRequest();
            }

            _unitOfWork.MedRDaily.Update(MedRDaily);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MedRDailyExists(id))
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

        // POST: api/MedRDailies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedRDaily>> PostMedRDaily(MedRDaily MedRDaily)
        {
            await _unitOfWork.MedRDaily.Insert(MedRDaily);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetMedRDaily", new { id = MedRDaily.Id }, MedRDaily);
        }

        // DELETE: api/MedRDailies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedRDaily(int id)
        {
            var MedRDaily = await _unitOfWork.MedRDaily.Get(q => q.Id == id);
            if (MedRDaily == null)
            {
                return NotFound();
            }
            await _unitOfWork.MedRDaily.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();

        }

        private async Task<bool> MedRDailyExists(int id)
        {
            var MedRDaily = await _unitOfWork.MedRDaily.Get(q => q.Id == id);
            return MedRDaily != null;
        }
    }
}