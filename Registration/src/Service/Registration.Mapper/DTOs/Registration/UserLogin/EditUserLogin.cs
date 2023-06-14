﻿using System.ComponentModel.DataAnnotations;

namespace Registration.Mapper.DTOs.Registration.UserLogin;
public class EditUserLogin
{
    [Required]
    public string Code { get; set; } = string.Empty;
    [Required]
    public string PassWord { get; set; } = string.Empty;
}