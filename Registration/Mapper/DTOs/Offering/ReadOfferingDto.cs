﻿namespace Registration.Mapper.DTOs.Offering;
public class ReadOfferingDto : ModelDto
{
    public DateTime Day { get;  set; }
    public int AdultQuantity { get;  set; }
    public int ChildrenQuantity { get;  set; }
    public decimal TotalAmount { get;  set; }
    public string PreacherMemberName { get;  set; }
    public string MeetingKind { get;  set; }
    public string OfferingKind { get;  set; }
    public string Church { get;  set; }
}
