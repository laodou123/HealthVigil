using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace HealthcareMonitoring.Client.Pages.Doctor;

public partial class Index
{
    private HealthcareMonitoring.Shared.Domain.Doctor? _doctor;

    [Inject]
    [NotNull]
    private IHttpClientFactory? HttpClientFactory { get; set; }

    [Inject]
    [NotNull]
    private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    private const string Url = "api/Doctors";

    private string _specialization = "";

    private List<SelectedItem> _items = default!;

    private DateTimeRangeValue _rangeValue = new() { Start = DateTime.Today.AddDays(-1), End = DateTime.Today };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var userName = state.User?.Identity?.Name;

        // 从数据库中读取 Doctor Profile
        var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
        var doctors = await client.GetFromJsonAsync<List<HealthcareMonitoring.Shared.Domain.Doctor>?>(Url);
        if (doctors != null)
        {
            _doctor = doctors.FirstOrDefault(i => i.Email == userName);
        }
        else
        {
            _doctor = new() { Email = userName, DoctorSpecialization = DoctorSpecialization.General.ToString() };
            ParseAvailedTime();
            await client.PostAsJsonAsync(Url, _doctor);
        }
        _items = typeof(DoctorSpecialization).ToSelectList();

    }

    void ParseAvailedTime()
    {
        if (_doctor?.DoctorAvailavleTime != null)
        {
            var segs = _doctor.DoctorAvailavleTime.Split('|');
            if (segs.Length == 2)
            {
                if (DateTime.TryParse(segs[0], out var star))
                {
                    _rangeValue.Start = star;
                }
                if (DateTime.TryParse(segs[1], out var end))
                {
                    _rangeValue.End = end;
                }
            }
        }
    }

    private async Task OnSubmit(EditContext context)
    {
        // 写回数据库
        if (_doctor != null)
        {
            _doctor.DoctorAvailavleTime = $"{_rangeValue.Start:yyyy-MM-dd}|{_rangeValue.End:yyyy-MM-dd}";
            var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
            var response = await client.PutAsJsonAsync($"{Url}/{_doctor.Id}", _doctor);
            if (response.IsSuccessStatusCode)
            {
                await ToastService.Success("Save Profile", "Save doctor profile successful");
            }
        }
    }
}