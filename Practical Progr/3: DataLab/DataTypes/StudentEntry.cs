public record StudentEntry(int StudentID, string FullName)
{
    public static readonly string INDICATOR = "STUD_ID";

    public override string ToString() => $"{INDICATOR}:{StudentID} {FullName}";
}