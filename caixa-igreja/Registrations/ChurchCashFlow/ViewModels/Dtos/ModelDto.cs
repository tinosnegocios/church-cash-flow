﻿namespace ChurchCashFlow.ViewModels.Dtos;

public abstract class ModelDto
{
    public DateTime DateRequest
    {
        get
        {
            return DateTime.UtcNow;
        }
    }
}
