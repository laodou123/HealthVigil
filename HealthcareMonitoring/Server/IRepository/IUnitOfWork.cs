using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareMonitoring.Shared.Domain;

namespace HealthcareMonitoring.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Doctor> Doctors { get; }
        IGenericRepository<Prescription> Prescriptions { get; }
        IGenericRepository<Patient> Patients { get; }
        IGenericRepository<MedRDaily> MedRDaily { get; }
        IGenericRepository<MedicalReport> MedicalReports { get; }
        IGenericRepository<Appointment> Appointments { get; }
        
    }
}