using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Service.Implementation;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Menu
{
    public class Main
    {
        IUserService _userService = new UserService();
        public static int LoggedInId;

        public void MainMenu()
        {
            Console.WriteLine("------------WELCOME TO RDT DENTAL LABORATORY -------------");
            Console.WriteLine("Press 1 to Register as a Patient\nPress 2 to Register as a Doctor \nPress 3 to login\nPress 4 to login as the HeadDoctor");
            int options = int.Parse(Console.ReadLine());
            if (options == 1)
            {
                PatientMenu menu = new PatientMenu();
                menu.PatientRegistration();
                MainMenu();
            }
            else if (options == 2)
            {
                DoctorMenu doctorMenu = new DoctorMenu();
                doctorMenu.DoctorRegistration();
                MainMenu();
            }
            else if (options == 3)
            {
                LoginMenu();
            }
            else if (options == 4)
            {
                LoginMenu();
            }
            else
            {
                Console.WriteLine("Enter valid number");
            }
        }

        public void LoginMenu()
        {
            try
            {
                Console.WriteLine("Enter your email:");
                string email = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();

                LoginRequestModel loginRequestModel = new LoginRequestModel
                {
                    Email = email,
                    Password = password,
                };

                var response = _userService.LoginByEmailAndPassword(loginRequestModel);
                if (response != null)
                {
                    if (response.Role == "Patient")
                    {
                        LoggedInId = response.Id;
                        PatientMenu patientMenu = new PatientMenu();
                        patientMenu.Patient();
                        Console.WriteLine("login successfully");
                    }
                    else if (response.Role == "Doctor")
                    {
                        LoggedInId = response.Id;
                        DoctorMenu doctorMenu = new DoctorMenu();
                        doctorMenu.Doctor();
                        Console.WriteLine("login successfully");

                    }
                    else
                    {
                        Console.WriteLine("login failed. Returning to the main menu");
                        Console.WriteLine();
                        MainMenu();
                    }
                    MainMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid input");
                Console.WriteLine();
                MainMenu();
            }
        }
    }
}
