﻿using Registration.DomainBase.Entities.Registrations;

namespace Registration.Mapper.DTOs.Registration.User;
public class ReadUserDto : ModelDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Church { get; set; } = string.Empty;
    public List<string> UserRoles { get; set; }
}
