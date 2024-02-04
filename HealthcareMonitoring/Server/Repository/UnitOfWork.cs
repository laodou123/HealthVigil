using HealthcareMonitoring.Server.Data;
using HealthcareMonitoring.Server.Models;
using HealthcareMonitoring.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthcareMonitoring.Server.IRepository;

namespace HealthcareMonitoring.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Doctor> _doctors;
        private IGenericRepository<Prescription> _prescriptions;
        private IGenericRepository<Patient> _patients;
        private IGenericRepository<MedicalReport> _medicalReports;
        private IGenericRepository<MedRDaily> _medicalReportsDaily;
        private IGenericRepository<Appointment> _appointments;


        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Doctor> Doctors
            => _doctors ??= new GenericRepository<Doctor>(_context);
        public IGenericRepository<Prescription> Prescriptions
           => _prescriptions ??= new GenericRepository<Prescription>(_context);
        public IGenericRepository<Patient> Patients
           => _patients ??= new GenericRepository<Patient>(_context);
        public IGenericRepository<MedicalReport> MedicalReports
           => _medicalReports ??= new GenericRepository<MedicalReport>(_context);
        public IGenericRepository<MedRDaily> MedRDaily
           => _medicalReportsDaily ??= new GenericRepository<MedRDaily>(_context);
        public IGenericRepository<Appointment> Appointments
           => _appointments ??= new GenericRepository<Appointment>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}