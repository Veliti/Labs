public class MyClass
{
    protected bool _first;
    protected bool _second;

    public MyClass(bool first, bool second)
    {
        _first = first;
        _second = second;
    }

    public MyClass(MyClass myClass)
    {
        this._first = myClass._first;
        this._second = myClass._second;
    }

    public bool NOTofOR2()
    {
        return !(_first | _second);
    }

    public override string ToString()
    {
        return $"first bool is {_first} second is {_second}";
    }
}