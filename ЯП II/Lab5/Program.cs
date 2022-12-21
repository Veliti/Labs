// var 14

using System.Collections.Generic;
using static Lab5;

// //Task 1
var randomInts = new List<int>(RandomInts(10));

System.Console.WriteLine("before");
Print(randomInts);

System.Console.WriteLine("after");
Print(ReturnRepeatingElements(randomInts));

// //Task 2 
var randomIntsLinkedList = new LinkedList<int>(RandomInts(4));
System.Console.WriteLine("before");
Print(randomIntsLinkedList);

System.Console.WriteLine("after");
Print(SwitchElements(randomIntsLinkedList));

// //Task 3

HashSet<Lang>[] workers = {
new(){Lang.eng, Lang.jpn},
new(){Lang.mandarin, Lang.jpn, Lang.french},
new(){Lang.jpn, Lang.mandarin, Lang.eng},
new(){Lang.eng, Lang.jpn, Lang.french}
};

System.Console.WriteLine(DoTask3(workers));

// //Task 4
var text = "Файл содержит текст на русском языке. С каких букв начинаются слова?";
System.Console.WriteLine(text);
Print(DoTask4(text));

//Task5
DoTask5("Task5Data.txt");
//DoTask5("Task5DataPerfect.txt");
