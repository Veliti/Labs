// 16 вар

var myClass = new MyClass(true, true);
var myClassCopy = new MyClass(myClass);

var myClass2 = new MyClass2(false, false, true);
var myClass22 = new MyClass2 (myClassCopy, true);


Console.WriteLine(myClass);
Console.WriteLine(myClassCopy);

Console.WriteLine(myClass.NOTofOR2());
Console.WriteLine(myClassCopy.NOTofOR2());

 
Console.WriteLine(myClass2);
Console.WriteLine(myClass22);

Console.WriteLine(myClass2.HowManyTrue());
Console.WriteLine(myClass22.HowManyTrue());

Console.WriteLine(myClass2.NOTofOR2());
Console.WriteLine(myClass22.NOTofOR3());

// output:
// first bool is True second is True
// first bool is True second is True
// False
// False

// first bool is False second is False and third is True
// first bool is True second is True and third is True
// 1 true
// 3 true
// True
// False
