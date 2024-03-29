﻿using Flunt.Notifications;
using Flunt.Validations;
using Entitie = Registration.DomainBase.Entities.Registrations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Registration.DomainBase.Entities.Registrations;

namespace Registration.Mapper.DTOs.Registration.User;

public class EditUserDto : ModelDto
{
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(8, MinimumLength = 6, ErrorMessage = "Name must be between 6 and 8 characters")]
    public string PasswordHash { get; set; } = string.Empty;
    [JsonIgnore]
    public string Code { get; set; } = string.Empty;
    public int ChurchId { get; set; }
    public HashSet<int> RoleIds { get; set; }
    public Entitie.Church? Church { get; set; }
    public List<Entitie.UserRole>? UserRoles { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .IsLowerThan(Name, 50, "Name", "Name should have no more than 50 chars")
            .IsGreaterThan(Name, 2, "Name", "Name should have at least 3 chars")
            .IsNotNullOrEmpty(PasswordHash, "PassWord", "The password can not be empty")
        );
    }

}
