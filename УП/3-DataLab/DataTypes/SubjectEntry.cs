public record SubjectEntry(int SubjectID, string Subject)
{
    public override string ToString() => $"ID:{SubjectID} {Subject}";
}