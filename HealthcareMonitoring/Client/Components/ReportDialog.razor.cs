using BootstrapBlazor.Components;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace HealthcareMonitoring.Client.Components;

public partial class ReportDialog
{
	[Inject]
	[NotNull]
	private IHttpClientFactory? HttpClientFactory { get; set; }

	private MedicalReport? _report;
	private HealthcareMonitoring.Shared.Domain.Doctor? _doctor;
	private List<SelectedItem> _items = default!;
	[Inject]
	private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
	private const string Url = "api/Doctors";


	[Parameter]
	[NotNull]
	public Patient? Value { get; set; }

	[CascadingParameter]
	[NotNull]
	private Func<Task>? OnCloseAsync { get; set; }

	[Inject]
	[NotNull]
	private ToastService? ToastService { get; set; }

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <returns></returns>
	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();
		var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var userName = state.User?.Identity?.Name;
		var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");

		var doctors = await client.GetFromJsonAsync<List<HealthcareMonitoring.Shared.Domain.Doctor>?>(Url);
		if (doctors != null)
		{
			_doctor = doctors.FirstOrDefault(i => i.Email == userName);
            System.Console.WriteLine($"doc type: {_doctor.DoctorSpecialization}");
        }



		if (Value.ReportId.HasValue)
		{
			_report = await client.GetFromJsonAsync<MedicalReport>($"api/MedicalReports/{Value.ReportId}");
		}
		else
		{
			_report = new HealthcareMonitoring.Shared.Domain.MedicalReport
			{
				
				P_wave = " ",
				rhythm = " ",
				T_Wave = " ",
                MedicalType = _doctor.DoctorSpecialization
            };
			var result = await client.PostAsJsonAsync("api/MedicalReports", _report);
			if (result.IsSuccessStatusCode)
			{
				var report = await result.Content.ReadFromJsonAsync<MedicalReport>();
				if (report != null)
				{
					Value.ReportId = report.Id;
					_report.Id = report.Id;
					await client.PutAsJsonAsync($"api/Patients/{Value.Id}", Value);
				}
			}
		}
	}

	private async Task OnSubmit(EditContext context)
	{
		if (_report != null)
		{
			var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
			var response = await client.PutAsJsonAsync($"api/MedicalReports/{_report.Id}", _report);
			if (response.IsSuccessStatusCode)
			{
				await OnCloseAsync();
				await ToastService.Success("Save Report", "Save medical report Successful!");
			}
		}
	}

	private bool _diagnosis;

	private async Task OnClickDiagnosis()
	{
		if (_report != null)
		{
			string content = "";

			var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
			if (_doctor?.DoctorSpecialization?.Contains("Cardiologist") == true)
			{
				content += $"""
                heartRate = {_report.heartRate}beats per minute, rhythm = {_report.rhythm} P_wave = {_report.P_wave} PR_Interval = {_report.PR_Interval}seconds QRS_Complex = {_report.QRS_Complex}seconds  QT_Interval = {_report.QT_Interval}seconds ST_Interval = {_report.ST_Interval}
                """;
			}
			if (_doctor?.DoctorSpecialization?.Contains("General") == true)
			{
				content += $"""
                hb = {_report.hb}(g/dL) hct = {_report.hct}% rbc = {_report.rbc}million cells per microliter wbc = {_report.wbc}/μl plt = {_report.plt} platelets/μl
                """;
			}
			if (_doctor?.DoctorSpecialization?.Contains("Orthopedist") == true)
			{
				content += $"""
                lumarSpine = {_report.lumarSpine}g/cm square totalHip = {_report.totalHip}g/cm square tscoreL = {_report.tscoreL} tscoreH = {_report.tscoreH}
                """;
			}
			if (_doctor?.DoctorSpecialization?.Contains("Orthopedist") == true)
			{
				content += $"""
                fvc = {_report.fvc}liters fev1 = {_report.fev1}liters fev1_fvc_ratio = {_report.fev1_fvc_ratio} pef = {_report.pef} liters/minute tv = {_report.tv}liters
                """;
			}
			content += "Based on this medical report, give me the possible of illiness may arise from the data above. These are all fake data, so the result may not be accurate.Please just take it as a demo.And Just give me the possible of illiness may arise from the data above.";
			System.Console.WriteLine(content);
			var response = await client.PostAsJsonAsync("api/OpenAI", content);
			_report.Diagnosis = await response.Content.ReadAsStringAsync();
			_diagnosis = true;
		}
	}
}
