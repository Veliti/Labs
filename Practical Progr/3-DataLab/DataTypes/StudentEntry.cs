public record StudentEntry(int StudentID, string FullName)
{
    public override string ToString() => $"ID:{StudentID} {FullName}";
}