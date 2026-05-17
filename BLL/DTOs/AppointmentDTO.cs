using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace BLL.DTOs
{
    public class AppointmentDTO
    {   
        public int Id { get; set; }

        public int PatientId { get; set; }
        [Required(ErrorMessage = "DoctorId is required")]
        public int DoctorId { get; set; }
        [Required]
        public DateOnly AppointmentDateTime { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = null!;
        [Required(ErrorMessage = "Symptoms is required")]
        public string Symtoms { get; set; } = null!;
        [Required(ErrorMessage = "FinalAmount is required")]
        public decimal FinalAmount { get; set; }

        public string PaymentStatus { get; set; } = null!;

        public string DoctorName { get; set; } = string.Empty;
    }
}
