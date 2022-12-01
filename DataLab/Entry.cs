public abstract record Entry(int ID)
{
    bool CompareID(Entry outer) => ID.Equals(outer.ID);
}