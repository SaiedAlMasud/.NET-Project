using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateOnly AppointmentDateTime { get; set; }

    public string Status { get; set; } = null!;

    public string Symtoms { get; set; } = null!;

    public decimal FinalAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
