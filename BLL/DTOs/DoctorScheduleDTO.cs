using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class DoctorScheduleDTO
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public int SlotDurationMinites { get; set; }
    }
}
