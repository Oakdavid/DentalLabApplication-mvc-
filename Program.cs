// See https://aka.ms/new-console-template for more information
using static DentalLabConsoleApplicationWithAdo.DentalLabDbContext;
using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Menu;
using DentalLabConsoleApplicationWithAdo.Models;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Models.Enum;
using DentalLabConsoleApplicationWithAdo.Repository.Implementation;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using DentalLabConsoleApplicationWithAdo.Service.Implementation;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using System.Globalization;
using System.Xml.Linq;
using DentalLabConsoleApplicationWithAdo;



//DentalLabDbContext dentalLabDbContext = new DentalLabDbContext();
//dentalLabDbContext.Tables();

Main main = new Main();
main.MainMenu();

//DentalServiceService dentalService = new DentalServiceService();
//DentalService service = new DentalService
//{
//    Id = 1,
//    Name = "scalling and polishing",
//    Description = "it the washing",
//    Code = "123ed",
//    Cost = 2000
//};
////dentalService.Create(service);
////var get = dentalService.Get(1);
////var getAll = dentalService.GetAllService();
//var update = dentalService.Update(service);
//Console.WriteLine();

//IAppointmentService appointmentService = new AppointmentService();
//appointmentService.GetAll();
//AppointmentRepository appointmentRepsoitory = new AppointmentRepository();
//Appointment appointment = new Appointment()
//{
//    DateOfAppointment = DateTime.Now,
//    DrNumber = "1",
//    IsDeleted = false
//};
//appointmentRepsoitory.Create(appointment);
//var getAllAppointment = appointmentRepsoitory.GetAll();
//Console.WriteLine();



//UserRepository userRepository = new UserRepository();
//User user = new User()
//{
//    Email = "yuslaw@yahoo.com",
//    Password = "Yuslaw",
//    Role = "Patient"
//};
//userRepository.Create(user);
//userRepository.GetAll();
//var a = userRepository.Get("yuslaw@yahoo.com");
//Console.WriteLine();


//DoctorRepository doctorRepository = new DoctorRepository();
//Doctor doctor = new Doctor()
//{
//    Id = 1,

//    Education = "msc, phd",
//    LicenseNumber = "12345",
//    YearsOfExperience = 10,
//    Specializations = "Scalling and polishing, Postodontics, Extraction",
//    SpecializationDescription = "scalling and polishing is about washing the tooth off plaqe",
//    IsDeleted = false,
//};
////doctorRepository.Create(doctor);
//var doctorGet = doctorRepository.Get("12345");
////var doctorGetAll = doctorRepository.GetAll();
//doctorRepository.Update(doctor);



//PatientRepository patientRepository = new PatientRepository();
//Patient patient = new Patient()
//{

//    IsDeleted = false,
//};
//patientRepository.Create(patient);
//var getByEmail = patientRepository.Get("tola@gmail.com");
////patientRepository.Get("RDT/CARDNO/00/59");
//var getAll = patientRepository.GetAll();
//Console.WriteLine();



//ProfileRepository profileRepository = new ProfileRepository();
//UserRepository userRepository = new UserRepository();
//Profile profile = new Profile
//{
//    FirstName = "lekan",
//    LastName = "Oduntan",
//    Address = "lag",
//    Contact = "456789",
//    DateOfBirth = DateTime.Parse("2010-01-01 01:42:35"),
//    Gender = "male",

//};
//profileRepository.Create(profile);
////userRepository.Create(profile);
//var get = profileRepository.Get("lekan@gmail.com");
//Console.WriteLine();
//var getAll = profileRepository.GetAll();
//Console.WriteLine();


//ReportRepository reportRepository = new ReportRepository();
//Report report = new Report
//{
//    DrName = "Dr Adele",
//    ReportContent = "report content",
//};
//reportRepository.Create(report);
//var get = reportRepository.Get("1");
//var getAll = reportRepository.GetAll();
//reportRepository.Delete("4");
//Console.WriteLine();



//SpecializationRepository specializationRepository = new SpecializationRepository();
//Specialization specialization = new Specialization
//{
//    Id = 1,
//    Name = "Scalling and Polishing and octodontics",
//    Description = "This is washing of the teeth to remove tooth plaqe and",
//    IsDeleted = false,
//};
//specializationRepository.Create(specialization);
//var get = specializationRepository.Get("name");
//var getAll = specializationRepository.GetAll();
//specializationRepository.Delete("scalling and polishing");
//var updateSpecializations = specializationRepository.Update(specialization);

//SpecializationRepository specializationRepository1 = new SpecializationRepository();
//Specialization updatedSpecialization = new Specialization
//{
//    Id = 3,
//    Name = "waterfall",
//    Description = "updated waterfall",
//    IsDeleted = false,
//};
////var update = specializationRepository1.Update(updatedSpecialization);

//IUserRepository userRepository = new UserRepository();
//User user = new User()
//{
//    Email = "david@gmail.com",
//    Password = "david",
//    IsDeleted = false,
//};
//userRepository.Create(user);
//userRepository.Get("david@gmail.com");
//userRepository.GetAll();



//Main main = new Main();
//main.MainMenu();

//IPatientService patientService = new PatientService();
//PatientRequestModelDto patientRequestModelDto = new PatientRequestModelDto()
//{
//    FirstName = "sola",
//    LastName = "odunbanku",
//    Address = "lag",
//    Contact = "0790845454",
//    DateOfBirth = DateTime.Now,
//    Gender = "male",
//    Password = "123",
//    Email = " lag@gmail.com",
//    PatientCardNo = "123w",


//};
//patientService.Create(patientRequestModelDto);
//Console.WriteLine();
//var patient = patientService.GetAll();
//Console.WriteLine();



//IAppointmentService appointments = new AppointmentService();
//AppointmentRequestModel appointment = new AppointmentRequestModel()
//{
//    PatientComplain = "i have something in my mouth that is stuck",
//    AppointmentType = AppointmentType.VirtualAppointment,
//    DateOfAppointment = DateTime.Now,
//    DrNumber = "123",
//    PatientId = 1,
//    CardNo = "1",
//    RefNumber = "1234",
//    AppointmentId = 1

//};

//appointments.Create(appointment);
//var appointmentAll = appointments.GetAll();
//var appointmentGet = appointments.Get("RDT/DENTAL/00/14RDT/DENTAL/00/30");


//IConsultationService consultation = new ConsultationService();
//ConsultationRequestModel consultationRequestModel = new ConsultationRequestModel()
//{

//    PhysicalConsult = "physical",
//    VirtualConsult = "virtual"
//};
//consultation.Create(consultationRequestModel);
//var getall = consultation.GetAll();
//Console.WriteLine();

//ConsultationRepository consultationRepository = new ConsultationRepository();     // error in my connection
//Consultation consultation = new Consultation
//{
//    Price = 22,
//    Location = "hub",
//    VirtualConsult = "virtual",
//    PhysicalConsult = "null",
//    IsDeleted = false,

//};
//consultationRepository.Create(consultation);
//var get = consultationRepository.GetAll();
//Console.WriteLine();c







//IConsultationService consultationService = new ConsultationService();
//ConsultationRequestModel consultationRequestModel1 = new ConsultationRequestModel()
//{
//    PhysicalConsult = "Physical",

//};
//consultationService.Create(consultationRequestModel1);
//Console.WriteLine();
//var get = consultationService.GetAll();



//IDoctorService doctorService = new DoctorService();
//DoctorRequestRegistrationDto doctors = new DoctorRequestRegistrationDto()
//{
//    Education = "phd",
//    Email = "odun@gmail.com",
//    LicenseNumber = "43dsdffdf44",
//    Specializations = "Denture",
//    YearsOfExperience = 7,
//    SpecializationDescription = "it is more than the way you see it",
//    Password = "adele",

//    FirstName = "Adele",
//    LastName = "Oduntan",
//    Address = "Abeokuta",
//    Contact = "0701123242",
//    DateOfBirth = DateTime.Parse("2010-01-01 01:42:35"),
//    Gender = "male",

//};
//doctorService.Create(doctors);
//var getAll = doctorService.GetAll();
//Console.WriteLine();




//IProfileService profileService = new ProfileService();
//DateTime date = DateTime.Parse(Console.ReadLine());
//ProfileRequestRegistrationModel profileRequestRegistrationModel = new ProfileRequestRegistrationModel()
//{
//    FirstName = "kay",
//    LastName = "odunatn",
//    Address = "lagos",
//    Contact = "y7u89o0",
//    DateOfBirth =date,
//};
//profileService.Create(profileRequestRegistrationModel);
//var get = profileService.Get("kay@gmail.com");
//var getAll = profileService.GetAll();

//IReportService reportService = new ReportService();
//ReportRequestModelDto reportRequestModelDto = new ReportRequestModelDto()
//{
//    ReportContent = " after the diagonises you will need to see a dental doctor to removet the tooth",

//};
//reportService.Create(reportRequestModelDto);
//var get = reportService.GetAll();
////Console.WriteLine();

//IUserService userService = new UserService();
//CreateUserRequestModel createUserRequestModel = new CreateUserRequestModel()
//{
//    Email = "lunar@gmail.com",
//    Password = "lunar"
//};
//userService.Create(createUserRequestModel);
//var user = userService.GetAll();
//Console.WriteLine();

//ToValidateDate(date)

//bool ToValidateDate(string date)
//{
//    bool result = true;
//    try
//    {
//        DateTime tDate = Convert.ToDateTime(date);
//    }
//    catch (Exception ex)
//    {
//        result = false;
//    }
//    return result;
//}

//bool ToValidate()
//{
//    DateTime expectedDate;
//    if (!DateTime.TryParse("0/27/2012", out expectedDate))
//    {
//        Console.Write("Luke I am not your datetime.... NOOO!!!!!!!!!!!!!!");
//    }
//}

//Console.WriteLine("Please enter a date of birth in this format (YYYY-MM-DD format)");
//DateTime dateOffBirth = DateTime.Parse(Console.ReadLine());
//ValidateDate(dateOffBirth);

//void ValidateDate(DateTime date)
//{
//    bool validDateTime = false;

//    while (!validDateTime)
//    {
//        Console.WriteLine("Please enter a date and time (YYYY-MM-DD format):");
//        string userInput = Console.ReadLine();
//        string expectedFormat = "yyyy-MM-dd";
//        if (DateTime.TryParseExact(userInput, expectedFormat, null, DateTimeStyles.None, out DateTime result))
//        {
//            Console.WriteLine($"Valid datetime: {result}");
//            validDateTime = true;
//        }
//        else
//        {
//            Console.WriteLine("Invalid datetime format. Please enter the date in YYYY-MM-DD format");
//            break;
//        }
//    }
//}




