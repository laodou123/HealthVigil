using HealthcareMonitoring.Server.IRepository;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareMonitoring.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly ApplicationDbContext _context;

        public AppointmentsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var Appointments = await _unitOfWork.Appointments.GetAll();
            return Ok(Appointments);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {

            var Appointment = await _unitOfWork.Appointments.Get(q => q.Id == id);

            if (Appointment == null)
            {
                return NotFound();
            }

            return Ok(Appointment);
        }
		// GET: api/Appointments/Patient/1
		[HttpGet("Patient/{id}")]
		public async Task<IActionResult> GetPatientPrescriptions(int id)
		{
			List<Appointment> patprescription = new List<Appointment>();
			var appointments = await _unitOfWork.Appointments.GetAll();
			foreach (var app in appointments)
			{
				if (app.PatientId == id)
				{
					patprescription.Add(app);
				}
			}
			return Ok(patprescription);
		}


		// GET: api/Appointments/Patient/1
		[HttpGet("Doctor/{patientId}")]
		public async Task<IActionResult> GetDoctor(int patientId)
		{
			var appointments = await _unitOfWork.Appointments.Get(x=>x.PatientId == patientId);

            if(appointments == null)
            {
				return NotFound();
			}

            var doctor = await _unitOfWork.Doctors.Get(x => x.Id == appointments.DoctorId);

			return Ok(doctor);
		}



		// PUT: api/Appointments/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment Appointment)
        {
            if (id != Appointment.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Appointments.Update(Appointment);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment Appointment)
        {
            await _unitOfWork.Appointments.Insert(Appointment);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetAppointment", new { id = Appointment.Id }, Appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var Appointment = await _unitOfWork.Appointments.Get(q => q.Id == id);
            if (Appointment == null)
            {
                return NotFound();
            }
            await _unitOfWork.Appointments.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();

        }

        private async Task<bool> AppointmentExists(int id)
        {
            var Appointment = await _unitOfWork.Appointments.Get(q => q.Id == id);
            return Appointment != null;
        }
    }
}