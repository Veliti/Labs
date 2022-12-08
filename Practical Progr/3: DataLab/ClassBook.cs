using System;
using System.Collections.Generic;
using System.Linq;


public class ClassBook : System.IDisposable
{
    DataBase<StudentEntry> _students;
    DataBase<SubjectEntry> _subjects;
    DataBase<AcademicRelation> _relation;

    public ClassBook(Logger? logger = null)
    {
        _students = new DataBase<StudentEntry>(logger);
        _subjects = new DataBase<SubjectEntry>(logger);
        _relation = new DataBase<AcademicRelation>(logger);
    }

    public void AddStudent(int studentID, string fullName)
    {
        if(_students.FindEntry(x => x.StudentID == studentID) != -1)
            throw new System.Exception($"{studentID} already exist");
            
        _students.Add(new StudentEntry(studentID, fullName));
    }

    public void AddSubject(int subjectID, string subject)
    {
        if(_subjects.FindEntry(x => x.SubjectID == subjectID) != -1)
            throw new System.Exception($"{subjectID} already exist");

        _subjects.Add(new SubjectEntry(subjectID, subject));
    }

    public void RemoveStudent(int studentID) => _students.Remove(_students.FindEntry(x => x.StudentID == studentID));

    public void RemoveSubject(int subjectID) => _subjects.Remove(_subjects.FindEntry(x => x.SubjectID == subjectID));

    public void RemoveMarks(int studentID, int subjectID) => _relation.Remove(_relation.FindEntry(x => x.StudentID == studentID && x.SubjectID == subjectID));

    public void UpdateMarks(int studentID, int subjectID, int[] newMarks)
    {
        var index = _relation.FindEntry(x => x.StudentID == studentID && x.SubjectID == subjectID);
        if (index == -1)
        {
            _relation.Add(new AcademicRelation(studentID, subjectID, newMarks));
        }
        else
        {
            _relation.Replace(index, _relation[index] with {Marks = _relation[index].Marks.Concat(newMarks).ToArray()});
        }
    }

    public string[] ListByStudent()
    {
        var list = new List<string>();
        foreach (var student in _students.Data)
        {
            list.Add(student.ToString());
            foreach (var subject in _subjects.Data)
            {
                var index = _relation.FindEntry(x => x.StudentID == student.StudentID && x.SubjectID == subject.SubjectID);
                if (index != -1)
                { 
                var relation = _relation[index];
                list.Add($"     {subject.ToString()} {relation.ToString()}");
                }
            }
        }
        return list.ToArray();
    }

    public string[] ListBySubject()
    {
        var list = new List<string>();
        foreach (var subject in _subjects.Data) 
        {
            list.Add(subject.ToString());
            foreach (var student in _students.Data)
            {
                var index = _relation.FindEntry(x => x.StudentID == student.StudentID && x.SubjectID == subject.SubjectID);
                if (index != -1)
                {
                var relation = _relation[index];
                list.Add($"     {subject.ToString()} {relation.ToString()}");   
                }
            }
        }
        return list.ToArray();
    }

    void IDisposable.Dispose()
    {
        _relation.Dispose();
        _students.Dispose();
        _subjects.Dispose();
    }
}