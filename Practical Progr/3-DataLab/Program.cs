// var 6
using System;
using System.Collections.Generic;
using static System.Console;
using static ConsoleHelper;

internal class Program
{
    const string help =
    @"
    Commands:
    list (by: student / subject) [search]
    add (what: student (ID  FullName) / subject (ID  Subject) / marks (StudentID  SubjectID  Marks)) 
    remove (student/subject ID or (Student ID  Subject ID) for Marks)
    help
    exit
    ";
    const string wrong = "wrong command";

    private static void Main(string[] args)
    {
        ConsoleApp();
    }

    private static void ConsoleApp()
    {
        WriteLine(help);
        var logger = new Logger();
        using (var classBook = new ClassBook(logger))
        {
            bool working = true;
            while (working)
            {
                var words = new Queue<string>(ReadLine()!.Split(' ', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries));

                try
                {
                    switch (words.Dequeue())
                    {
                        case "list":
                            switch (words.Dequeue())
                            {
                                case "student":
                                    words.TryDequeue(out var search);
                                    Print(classBook.ListByStudent(), search);
                                    break;
                                case "subject":
                                    words.TryDequeue(out var search2);
                                    Print(classBook.ListBySubject(), search2);
                                    break;
                                default:
                                    WriteLine(wrong);
                                    break;
                            }

                            break;
                        case "add":
                            switch (words.Dequeue())
                            {
                                case "student":
                                    classBook.AddStudent(int.Parse(words.Dequeue()), EnumerableToString(words));
                                    break;
                                case "subject":
                                    classBook.AddSubject(int.Parse(words.Dequeue()), EnumerableToString(words));
                                    break;
                                case "marks":
                                    classBook.UpdateMarks(int.Parse(words.Dequeue()), int.Parse(words.Dequeue()), ParseMarks(words));
                                    break;
                                default:
                                    WriteLine(wrong);
                                    break;
                            }
                            break;
                        case "remove":
                            switch (words.Dequeue())
                            {
                                case "student":
                                    classBook.RemoveStudent(int.Parse(words.Dequeue()));
                                    break;
                                case "subject":
                                    classBook.RemoveSubject(int.Parse(words.Dequeue()));
                                    break;
                                case "marks":
                                    classBook.RemoveMarks(int.Parse(words.Dequeue()), int.Parse(words.Dequeue()));
                                    break;
                                default:
                                    WriteLine(wrong);
                                    break;
                            }
                            break;
                        case "help":
                            WriteLine(help);
                            break;
                        case "exit":
                            working = false;
                            break;
                        default:
                            WriteLine(wrong);
                            break;
                    }
                }
                catch (System.Exception e)
                {
                    WriteLine(e.Message);
                }

            }
        }
    }
}