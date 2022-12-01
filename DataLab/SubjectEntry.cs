public record SubjectEntry(int SubjectID, string Subject) : Entry(SubjectID)
{
    public bool CompareSubject(SubjectEntry outer) => Subject.Equals(outer.Subject);
}