using System.Net.Http.Json;
using HealthcareMonitoring.Shared.Domain;

namespace HealthcareMonitoring.Client.Services
{
    public class DoctorService
    {
        private readonly HttpClient _httpClient;

        public DoctorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            await _httpClient.PutAsJsonAsync("api/doctor", doctor);
        }

    }
}
