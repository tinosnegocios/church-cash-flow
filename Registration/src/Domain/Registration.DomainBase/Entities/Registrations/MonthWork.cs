﻿using System.Xml.Linq;

namespace Registration.DomainBase.Entities.Registrations;

public class MonthWork : Entitie
{
    public int YearMonth { get; set; }
    public int ChurchId { get; set; }
    public decimal InitialValue { get; set; }
    public decimal FinalValue { get; set; }
    public Church? Church { get; set; }

    public MonthWork()
    {
    }
    public MonthWork(int id, int yearMonth, int churchId)
    {
        Id = id;
        YearMonth = yearMonth;
        ChurchId = churchId;
    }

    public void SetInitialValue(decimal value) => InitialValue = value;
    public void SetFinalValue(decimal value) => FinalValue = value;
}
