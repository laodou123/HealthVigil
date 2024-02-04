using BootstrapBlazor.Components;
using HealthcareMonitoring.Client.Pages.Doctor;
using HealthcareMonitoring.Client.Static;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;


namespace HealthcareMonitoring.Client.Components;

public partial class DeletePrescriptionDialog
{
    
    [Inject]
    [NotNull]
    private IHttpClientFactory? HttpClientFactory { get; set; }


    private Prescription Prescription = new Prescription();
    private const string Url = "api/Prescriptions";

    [Parameter]
    [NotNull]
    public Prescription? Value { get; set; }
    [CascadingParameter]
    [NotNull]
    private Func<Task>? OnCloseAsync { get; set; }
    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Parameter]
    public Func<Task>? OnValueChanged { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>


    private async Task OnSubmit(EditContext context)
    {
        var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
        var result = await client.DeleteAsync($"{Url}/{Value.Id}");
        if (result.IsSuccessStatusCode)
        {
            await OnCloseAsync();
            await ToastService.Success("deleted Prescription", "deleted prescription successful");
            if (OnValueChanged != null)
            {
                await OnValueChanged();
            }
        }


    }

}
