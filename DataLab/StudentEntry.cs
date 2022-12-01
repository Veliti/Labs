public record StudentEntry(int StudentID, string FullName) : Entry(StudentID)
{
    public bool CompareFullName(StudentEntry outer) => FullName.Equals(outer.FullName);
}