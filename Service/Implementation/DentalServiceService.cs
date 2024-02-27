using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Models.Enum;
using DentalLabConsoleApplicationWithAdo.Repository.Implementation;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Implementation
{
    public class DentalServiceService : IDentalServiceService
    {
        IDentalServiceRepository _dentalServiceRepository = new DentalServiceRepository();
        public DentalService Create(DentalService dentalService)
        {
            if (dentalService == null)
            {
                Console.WriteLine("No Services found");
                return null;
            }
            DentalService service = new DentalService
            {
                Id = dentalService.Id,
                Name = dentalService.Name,
                Description = dentalService.Description,
                Code = dentalService.Code,
                Cost = dentalService.Cost
            };
            _dentalServiceRepository.Create(service);
            return service;
        }

        public DentalService Get(int id)
        {
            var dentalService = _dentalServiceRepository.Get(id);
            if (dentalService != null && !dentalService.IsDeleted)
            {

                return new DentalService
                {
                    Id = dentalService.Id,
                    Name = dentalService.Name,
                    Description = dentalService.Description,
                    Code = dentalService.Code,
                    Cost = dentalService.Cost
                };
            }
            return null;
        }

        public IEnumerable<DentalService> GetAllService()
        {
            var dentalService = _dentalServiceRepository.GetAllService();
            var listOfService = new List<DentalService>();
            foreach (var service in listOfService)
            {
                if (service != null)
                {
                    DentalService dentalServices = new DentalService
                    {
                        Id = service.Id,
                        Name = service.Name,
                        Description = service.Description,
                        Code = service.Code,
                        Cost = service.Cost
                    };
                    listOfService.Add(dentalServices);
                }
            }
            return listOfService;
        }

        public DentalService Update(DentalService dentalService)
        {
            var existingService = _dentalServiceRepository.Get(dentalService.Id);
            if (existingService != null)
            {
                existingService.Id = dentalService.Id;
                existingService.Name = dentalService.Name;
                existingService.Description = dentalService.Description;
                existingService.Code = dentalService.Code;
                existingService.Cost = dentalService.Cost;

                _dentalServiceRepository.Update(dentalService);
                return dentalService;
            }
            return null;
        }

        public void ToString(DentalService obj)
        {
            Console.WriteLine($"Id: {obj.Id}, Name: {obj.Name}, Description: {obj.Description}, Code: {obj.Code}, Cost: {obj.Cost}");
        }
    }
}
