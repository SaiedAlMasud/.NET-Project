using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BLL.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public string Status { get; set; } = null!;

        public string Symtoms { get; set; } = null!;

        public decimal FinalAmount { get; set; }

        public string PaymentStatus { get; set; } = null!;
    }
}
