﻿namespace Registration.Mapper.DTOs.Tithes;
public class ReadTithesDto : ModelDto
{
    public decimal TotalAmount { get; set; }
    public DateTime Day { get; set; }
    public string Competence { get; set; }
    public string Church { get; set; }
    public string Member { get; set; }
    public string OfferingKind { get; set; }
}