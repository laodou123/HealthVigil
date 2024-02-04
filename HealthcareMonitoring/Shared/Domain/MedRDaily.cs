using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareMonitoring.Shared.Domain
{
    public class MedRDaily : BaseDomainModel
    {
        public int bpm { get; set; }
        public int systolicPressure { get; set; }
        public int diastolicPressure { get; set; }
        public int bloodSugarLevel { get; set; }
        public virtual Patient? Patient { get; set; }
        public int? PatientId { get; set; }

    }
}




