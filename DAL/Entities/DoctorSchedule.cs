using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class DoctorSchedule
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int SlotDurationMinites { get; set; }

    public virtual Doctor IdNavigation { get; set; } = null!;
}
