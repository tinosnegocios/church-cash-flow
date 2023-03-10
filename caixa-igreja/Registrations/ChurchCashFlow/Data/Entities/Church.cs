namespace ChurchCashFlow.Data.Entities;
public class Church : Entitie
{
    public string Name { get; set; }
    public bool? Active { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }

    public IList<User> Users { get; set; }

    public Church(int id, string name, int addressId)
    {
        Id = id;
        Name = name;
        AddressId = addressId;
    }

    public Church()
    {
    }

    public void AddAddress(Address address)
    {
        Address= address;
    }
}