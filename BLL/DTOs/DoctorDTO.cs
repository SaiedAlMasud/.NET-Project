using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Specialization { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public decimal ConsultationFee { get; set; }
    }
}
