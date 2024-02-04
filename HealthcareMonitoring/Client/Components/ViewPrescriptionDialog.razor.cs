using BootstrapBlazor.Components;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;


namespace HealthcareMonitoring.Client.Components;

public partial class ViewPrescriptionDialog
{
    private readonly List<int> PageItemsSource = new() { 20, 40, 80 };
    [Inject]
    [NotNull]
    private IHttpClientFactory? HttpClientFactory { get; set; }


    private IList<Prescription>? prescriptions;
    private Prescription newPrescription = new Prescription();
    private const string Url = "api/Prescriptions/Patient";

    [Parameter]
    [NotNull]
    public Patient? Value { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }


    private Table<Prescription> _tablePrescription = default!;
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>

    protected override async Task OnInitializedAsync()
    {

        await base.OnInitializedAsync();
        var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
        prescriptions = await client.GetFromJsonAsync<IList<HealthcareMonitoring.Shared.Domain.Prescription>?>($"{Url}/{Value.Id}");


    }
    private async Task<QueryData<HealthcareMonitoring.Shared.Domain.Prescription>> OnQueryAsync(QueryPageOptions options)
    {
        var client = HttpClientFactory.CreateClient("HealthcareMonitoring.ServerAPI");
        prescriptions = await client.GetFromJsonAsync<IList<HealthcareMonitoring.Shared.Domain.Prescription>?>($"{Url}/{Value.Id}");


        return new QueryData<HealthcareMonitoring.Shared.Domain.Prescription>()
        {
            Items = prescriptions,
            TotalCount = prescriptions.Count,
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true
        };
    }
    private async Task OnClickEditPrescriptionButton(HealthcareMonitoring.Shared.Domain.Prescription item)
    {
        await DialogService.ShowSaveDialog<EditPrescriptionDialog>(" View Prescription Dialog", parametersFactory: dict =>
        {
            dict.Add("Value", item);
            dict.Add("OnValueChanged", new Func<Task>(async () =>
            {
                await _tablePrescription.QueryAsync();
            }));
        }, configureOption: options =>
        {
            options.ShowFooter = false;
        });
    }

    private async Task OnClickDeletePrescriptionButton(HealthcareMonitoring.Shared.Domain.Prescription item)
    {
        await DialogService.ShowSaveDialog<DeletePrescriptionDialog>(" Delete Prescription Dialog", parametersFactory: dict =>
        {
            dict.Add("Value", item);
            dict.Add("OnValueChanged", new Func<Task>(async () =>
            {
                await _tablePrescription.QueryAsync();
            }));
        }, configureOption: options =>
        {
            options.ShowFooter = false;
        });

    }

}



