public record AcademicRelation(int StudentID, int SubjectID, int[] Marks)
{
    public override string ToString()
    {
        var marks = "";
        foreach (var item in Marks)
        {
            marks += $" {item}";
        }

        return marks;
    }
}