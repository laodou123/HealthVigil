// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HealthcareMonitoring.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthcareMonitoring.Server.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string DoctorName { get; set; }
            public string DoctorPhoneNumber { get; set; }
            public string DoctorSpecialization { get; set; }
            public DateTime DoctorAvailavleTime { get; set; }

            public string DoctorExperience { get; set; }
            public string DoctorNationality { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            //var DoctorName = await _userManager.GetDoctorNameAsync(user);
            //var DoctorPhoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //var DoctorSpecialization = await _userManager.GetSpecializationAsync(user);
            //var DoctorAvailableTime = await _userManager.GetAvailavleTimeAsync(user);
            //var DoctorExperience = await _userManager.GetExperienceAsync(user);
            //var DoctorNationality = await _userManager.GetNationalityAsync(user);

            Username = userName;

            Input = new InputModel
            {
                //DoctorName = DoctorName,
                //DoctorPhoneNumber = DoctorPhoneNumber,
                //DoctorSpecialization = DoctorSpecialization,
                //DoctorAvailavleTime = DoctorAvailableTime,
                //DoctorExperience = DoctorExperience,
                //DoctorNationality = DoctorNationality
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            //var DoctorName = await _userManager.GetDoctorNameAsync(user);
            //if (Input.DoctorName != DoctorName)
            //{
            //    var setDoctorNameResult = await _userManager.SetDoctorNameAsync(user, Input.DoctorName);
            //    if (!setDoctorNameResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set DoctorName.";
            //        return RedirectToPage();
            //    }
            //}

            //var DoctorPhoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.DoctorPhoneNumber != DoctorPhoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.DoctorPhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}
            //var DoctorSpecialization = await _userManager.GetSpecializationAsync(user);
            //if (Input.DoctorSpecialization != DoctorSpecialization)
            //{
            //    var setDoctorSpecialization = await _userManager.SetSpecializationAsync(user, Input.DoctorSpecialization);
            //    if (!setDoctorSpecialization.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set DoctorSpecialization.";
            //        return RedirectToPage();
            //    }
            //}
            //var DoctorAvailavleTime = await _userManager.GetAvailavleTimeAsync(user);
            //if (Input.DoctorAvailavleTime != DoctorAvailavleTime)
            //{
            //    var setDoctorAvailavleTime = await _userManager.SetAvailavleTimeAsync(user, Input.DoctorAvailavleTime);
            //    if (!setDoctorAvailavleTime.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set DoctorAvailavleTime.";
            //        return RedirectToPage();
            //    }
            //}
            //var DoctorExperience = await _userManager.GetExperienceAsync(user);
            //if (Input.DoctorExperience != DoctorExperience)
            //{
            //    var setDoctorExperience = await _userManager.SetExperienceAsync(user, Input.DoctorExperience);
            //    if (!setDoctorExperience.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set DoctorExperience.";
            //        return RedirectToPage();
            //    }
            //}
            //var DoctorNationality = await _userManager.GetNationalityAsync(user);
            //if (Input.DoctorNationality != DoctorNationality)
            //{
            //    var setDoctorNationality = await _userManager.SetNationalityAsync(user, Input.DoctorNationality);
            //    if (!setDoctorNationality.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set DoctorNationality.";
            //        return RedirectToPage();
            //    }
            //}
           

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
