using BootstrapBlazor.Components;
using HealthcareMonitoring.Client.Pages.Doctor;
using HealthcareMonitoring.Client.Static;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;


namespace HealthcareMonitoring.Client.Components;

public partial class PrescriptionDialog
{
    [Inject]
    [NotNull]
    private IHttpClientFactory? HttpClientFactory { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Parameter]
    [NotNull]
    public Patient? Value { get; set; }

    [CascadingParameter]
    [NotNull]
    private Func<Task>? OnCloseAsync { get; set; }

    private List<Prescription> prescriptions = new List<Prescription>();
    private Prescription newPrescription = new Prescription();
    private IList<Doctor> doctors;
    private string? docName;


    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>

    protected override async Task OnInitializedAsync()
    {

        await base.OnInitializedAsync();
        prescriptions.Add(new Prescription());
        doctors = await _client.GetFromJsonAsync<IList<Doctor>>($"{Endpoints.Doctors}");
        var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var userName = state.User?.Identity?.Name;
        if (doctors != null)
        {
            foreach (var doc in doctors)
            {
                System.Console.WriteLine($"Pat id: {doc.Id}");
                if (doc.Email == userName)
                {
                    docName = doc.DoctorName;
                    System.Console.WriteLine($"Pat id: {doc.DoctorName}");
                }
            }
        }


    }

    private async Task OnSubmit(EditContext context)
    {
        if (prescriptions.Any())
        {
            var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
            foreach (var prescription in prescriptions)
            {
                prescription.MedicinePrescriptionDoctor = docName;
                prescription.PatientId = Value.Id;
                var result = await client.PostAsJsonAsync("api/Prescriptions", prescription);
                if (result.IsSuccessStatusCode)
                {
                    await OnCloseAsync();
                    await ToastService.Success("added Prescription", "added prescription successful");
                }
            }
            
        }
    }
    private void AddPrescription()
    {
        var newPrescriptionCopy = new Prescription
        {
            MedicineName = newPrescription.MedicineName,
            MedicineQuantity = newPrescription.MedicineQuantity,
            MedicineUsage = newPrescription.MedicineUsage,
            MedicineDescription = newPrescription.MedicineDescription
        };

        prescriptions.Add(newPrescriptionCopy);
        newPrescription = new Prescription(); // Reset the form
    }


}
