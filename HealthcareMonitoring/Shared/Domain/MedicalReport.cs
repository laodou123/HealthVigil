namespace HealthcareMonitoring.Shared.Domain
{
    public class MedicalReport : BaseDomainModel
    {
        public int heartRate { get; set; }
        public string rhythm { get; set; }
        public string P_wave { get; set; }
        public float PR_Interval { get; set; }
        public float QRS_Complex { get; set; }
        public float QT_Interval { get; set; }
        public float ST_Interval { get; set; }
        public string T_Wave { get; set; }

        public float hb { get; set; }
        public float hct { get; set; }
        public float rbc { get; set; }
        public float wbc { get; set; }
        public float plt { get; set; }

        public float lumarSpine { get; set; }
        public float totalHip { get; set; }
        public float tscoreL { get; set; }
        public float tscoreH { get; set; }

        public float fvc { get; set; }
        public float fev1 { get; set; }
        public float fev1_fvc_ratio { get; set; }
        public float pef { get; set; }
        public float tv { get; set; }

        public string MedicalType { get; set; }
        public string? Diagnosis { get; set; }
    }
}
public enum MedicalType
{
	Cardiologist,
	Pulmonologist,
	Orthopedist,
	General
}