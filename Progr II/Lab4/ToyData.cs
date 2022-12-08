public struct ToyData
{
    public string Name {get;}
    public int Price {get;}
    public (int min, int max) AgeRestriction {get;}

    public ToyData(string name, int price, (int min, int max) ageRestriction)
    {
        Name = name;
        Price = price;
        AgeRestriction = ageRestriction;
    }
}