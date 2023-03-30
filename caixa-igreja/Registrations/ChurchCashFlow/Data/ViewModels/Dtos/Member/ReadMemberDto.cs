﻿namespace ChurchCashFlow.Data.ViewModels.Dtos.Member;
public class ReadMemberDto : ModelDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime DateBirth { get; set; }
    public string Church { get; set; }
    public string Post { get; set; }
}