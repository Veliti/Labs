public record SubjectEntry(int SubjectID, string Subject)
{
    public static readonly string INDICATOR = "SUBJ_ID";

    public override string ToString() => $"{INDICATOR}:{SubjectID} {Subject}";
}