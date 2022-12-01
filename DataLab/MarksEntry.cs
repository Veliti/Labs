using System.Collections.Generic;

public record MarksEntry(int AcademicID, List<int> Marks) : Entry(AcademicID);