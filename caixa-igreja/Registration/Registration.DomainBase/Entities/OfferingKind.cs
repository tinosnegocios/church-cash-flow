﻿namespace Registration.DomainBase.Entities;
public class OfferingKind : Entitie
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public OfferingKind(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}