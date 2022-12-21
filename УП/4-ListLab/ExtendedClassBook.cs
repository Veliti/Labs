using System.Collections.Generic;
using System.Linq;
using System;

public class ExtendedClassBook : ClassBook
{
    public void AddRandomStudents(List<string> names, List<string> lastNames, int amount)
    {
        var random = new Random();
        var maxID = _students.Data.MaxBy(x => x.StudentID)?.StudentID ?? 0;
        for (int i = 0; i < amount; i++)
        {
            _students.Add(new StudentEntry(maxID++, $"{names[random.Next(0, names.Count)]} {lastNames[random.Next(0, lastNames.Count)]}"));
        }
    }

    public void PurgeStudent(int studentID)
    {
        RemoveStudent(studentID);
        foreach (var index in _relation.FindEntrys(x => x.StudentID == studentID)!)
        {
            _relation.Remove(index);
        }
    }

    public string[] ListStudentsAverageMark()
    {
        var strings = new List<string>();
        foreach (var student in _students.Data)
        {
            var averages = from relation in _relation.Data
                        group relation by relation.StudentID == student.StudentID into marks
                        from mark in marks
                        select mark.Marks.Sum() / mark.Marks.Length;

            strings.Add($"{student.ToString()} Average: {averages.Sum() / averages.Count()}");
        }
        return strings.ToArray();
    }

    
}