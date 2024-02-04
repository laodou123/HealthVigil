using BootstrapBlazor.Components;
using HealthcareMonitoring.Client.Static;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;


namespace HealthcareMonitoring.Client.Components;

public partial class ViewMedRDailyDialog
{
    [Parameter]
    [NotNull]
    public Patient? Value { get; set; }

    private IList<MedRDaily>? med;
    private List<DateTime?> dateMedR;
    private List<MedRDaily> filteredMedRDaily = new List<MedRDaily>();
    MedRDaily medR = new MedRDaily();
    List<DateTime?> sortedList;
    DateTime? latestdate;
    private DateTime today = new DateTime();
    private DateTime startDate = new DateTime();


    protected async override Task OnInitializedAsync()
    {

       
        dateMedR = new List<DateTime?>();

        med = await _client.GetFromJsonAsync<IList<MedRDaily>>($"{Endpoints.MedRDailies}/Patient/{Value.Id}");
        if (med != null)
        {
            foreach (var medd in med)
            {
                System.Console.WriteLine(medd.DateCreated.ToString());
                DateTime? datetemp = medd.DateCreated;
                dateMedR.Add(datetemp);
                

            }
        }

        sortedList = dateMedR.OrderBy(dt => dt).ToList();
        if (sortedList.Any())
        {
            latestdate = sortedList.Last();
            System.Console.WriteLine($"time id: {latestdate}");
            

        }
        if (latestdate != null)
        {
                foreach (var meddd in med)
                {
                    if (meddd.DateCreated == latestdate)
                    {
                        medR = meddd;

                    }
            }

        }

        // Calculate the date range for the past 14 days (including today)
        today = DateTime.Now;
        System.Console.WriteLine($"today id: {today}");
        startDate = today.AddDays(-13); // 14 days ago
        System.Console.WriteLine($"start id: {startDate}");
        // Filter records within the date range
        var past14DaysRecords = med.Where(record => record.DateCreated.Value >= startDate && record.DateCreated.Value <= today);

        if (past14DaysRecords.Any())
        {
            filteredMedRDaily = past14DaysRecords
            .GroupBy(record => record.DateCreated.Value.Date)
            .Select(group => group.OrderByDescending(record => record.DateCreated.Value.Date).Last())
            .ToList();
        }
        else
        {
            System.Console.WriteLine("errorrrrr12345");
        }

    }
    string FormatAsdaymonth(object value)
    {
        if (value != null)
        {
            return Convert.ToDateTime(value).ToString("dd/M");
        }

        return string.Empty;
    }

}



