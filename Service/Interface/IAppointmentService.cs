using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Interface
{
    public interface IAppointmentService
    {
        AppointmentDto Create(AppointmentRequestModel obj);
        AppointmentDto Get(string refNumber);
        List<AppointmentDto> GetAll();
        public void ToString(AppointmentDto obj);
    }
}
