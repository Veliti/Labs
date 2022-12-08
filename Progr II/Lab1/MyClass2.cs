
// MyClass2 это как MyClass только с ещё 1 булом
class MyClass2 : MyClass
{
    private bool _third;

    public MyClass2(bool first, bool second, bool third) : base(first, second)
    {
        _third = third;
    }

    public MyClass2(MyClass myClass, bool third) : base(myClass)
    {
        _third = third;
    }

    // 1) патрен матчинг в c#
    public string HowManyTrue()
    {
        return (_first, _second, _third) switch
        {
            (true, true, true) => "3 true",
            var (f, s, t) when Convert.ToInt32(f) + Convert.ToInt32(s) + Convert.ToInt32(t) == 2 => "2 true",
            var (f, s, t) when Convert.ToInt32(f) + Convert.ToInt32(s) + Convert.ToInt32(t) == 1 => "1 true",
            _ => "All false",
        };
    }

    // 2) NOTofOR() для 3х булов
    public bool NOTofOR3()
    {
        return !(_first | _second | _third);
    }

    // 3) ToString() для отображения 3х булов
    public override string ToString()
    {
        return base.ToString() + $" and third is {_third}";
    }
}