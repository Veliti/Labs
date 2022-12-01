public record AcademicRelation(int AcademicID, int StudentID, int SubjectID) : Entry(AcademicID)
{
    public bool CompareStudentID(StudentEntry other) => StudentID.Equals(StudentID);
    public bool CompareSubjectID(SubjectEntry other) => SubjectID.Equals(SubjectID);
}