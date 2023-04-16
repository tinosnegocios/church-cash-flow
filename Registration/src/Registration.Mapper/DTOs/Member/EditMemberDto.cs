﻿using Flunt.Notifications;
using Flunt.Validations;

namespace Registration.Mapper.DTOs.Member;
public class EditMemberDto : ModelDto
{
    public string Name { get; set; }
    public DateTime DateBirth { get; set; }
    public int ChurchId { get; set; }
    public int PostId { get; set; }

    public void Validate()
    {
        DateTime t;
        if (!DateTime.TryParse(DateBirth.ToString(), out t))
            AddNotification("DateBirth", "The DateBirth is a invalid datetime");

        AddNotifications(new Contract<Notification>()
            .IsLowerThan(Name, 50, "Name", "Name should have no more than 50 chars")
            .IsGreaterThan(Name, 2, "Name", "Name should have at least 3 chars")
            .IsGreaterThan(ChurchId, 0, "ChurchId", "Church Id cannot empty")
            .IsGreaterThan(PostId, 0, "PostId", "PostId Id cannot empty")
            );
    }
}