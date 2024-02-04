using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareMonitoring.Shared.Domain
{
    public class Appointment : BaseDomainModel
    {
        public string Location { get; set; }

        public string Type { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public int? DoctorId { get; set; }
        public virtual Patient? Patient { get; set; }
        public int? PatientId { get; set; }
        public string? Reason { get; set; }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool Delete { get; set; } = false;


    }
}