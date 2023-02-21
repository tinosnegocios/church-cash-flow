﻿using System.ComponentModel.DataAnnotations;

namespace ChurchCashFlow.ViewModels.Dtos.Address;
public class EditAddressDto
{
    [Required]
    public string Country { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string ZipCode { get; set; }
    [Required]
    public string? District { get; set; }
    [Required]
    public string? Street { get; set; }
    public string? Additional { get; set; }
    [Required]
    public int Number { get; set; }
}
