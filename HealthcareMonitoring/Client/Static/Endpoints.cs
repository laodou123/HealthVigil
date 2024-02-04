namespace HealthcareMonitoring.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string Doctors = $"{Prefix}/Doctors";
        public static readonly string Patients = $"{Prefix}/Patients";
        public static readonly string Prescriptions = $"{Prefix}/Prescriptions";
        public static readonly string MedRDailies = $"{Prefix}/MedRDailies";
        public static readonly string Appointments = $"{Prefix}/Appointments";
        public static readonly string MedicalReports = $"{Prefix}/MedicalReports";
    }
}