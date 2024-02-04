using BootstrapBlazor.Components;
using HealthcareMonitoring.Client.Components;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Json;

namespace HealthcareMonitoring.Client.Pages.Doctor;

public partial class Patients
{
    private HealthcareMonitoring.Shared.Domain.Doctor? _doctor;
    private readonly List<int> PageItemsSource = new() { 20, 40, 80 };

    [Inject]
    [NotNull]
    private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
    [Inject]
    [NotNull]
    private IHttpClientFactory? HttpClientFactory { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    private const string Url = "api/Patients";
    private const string UrlDoctor = "api/Doctors";
    private const string Urlappointment = "api/Appointments";


    private async Task<QueryData<HealthcareMonitoring.Shared.Domain.Patient>> OnQueryAsync(QueryPageOptions options)
    {
        var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
        var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var userName = state.User?.Identity?.Name;
        var doctors = await client.GetFromJsonAsync<List<HealthcareMonitoring.Shared.Domain.Doctor>?>(UrlDoctor);
        if (doctors != null)
        {
            _doctor = doctors.FirstOrDefault(i => i.Email == userName);
        }
        var appointments = await client.GetFromJsonAsync<List<HealthcareMonitoring.Shared.Domain.Appointment>?>(Urlappointment);
        var patients = new List<HealthcareMonitoring.Shared.Domain.Patient>();
        if (appointments != null)
        {
            var _appointment = appointments.Where(i => i.DoctorId == _doctor.Id).ToList();
            var patientList = await client.GetFromJsonAsync<List<HealthcareMonitoring.Shared.Domain.Patient>?>(Url);
            if (patientList != null)
            {
                patients.AddRange(patientList.Where(i => _appointment.Any(p => p.PatientId == i.Id)));
            }
        }
        return new QueryData<HealthcareMonitoring.Shared.Domain.Patient>()
        {
            Items = patients,
            TotalCount = patients.Count,
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true
        };
    }

    private async Task OnClickReportButton(HealthcareMonitoring.Shared.Domain.Patient item)
    {
        await DialogService.ShowSaveDialog<ReportDialog>("Medical Report", parametersFactory: dict =>
        {
            dict.Add("Value", item);
        }, configureOption: options =>
        {
            options.ShowFooter = false;
        });
    }

    private async Task OnClickPrescriptionButton(HealthcareMonitoring.Shared.Domain.Patient item)
    {
        await DialogService.ShowSaveDialog<PrescriptionDialog>("Prescription Dialog", parametersFactory: dict =>
        {
            dict.Add("Value", item);
        }, configureOption: options =>
        {
            options.ShowFooter = false;
        });
    }
    private async Task OnClickViewPrescriptionButton(HealthcareMonitoring.Shared.Domain.Patient item)
    {
        await DialogService.ShowSaveDialog<ViewPrescriptionDialog>(" View Prescription Dialog", parametersFactory: dict =>
        {
            dict.Add("Value", item);
        }, configureOption: options =>
        {
            options.ShowFooter = false;
        });
    }
    private async Task OnClickViewMedRDailyButton(HealthcareMonitoring.Shared.Domain.Patient item)
    {
        await DialogService.ShowSaveDialog<ViewMedRDailyDialog>(" View Daily status Dialog", parametersFactory: dict =>
        {
            dict.Add("Value", item);
        }, configureOption: options =>
        {
            options.ShowFooter = false;
        });
    }

}