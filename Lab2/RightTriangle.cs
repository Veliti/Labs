class RightTriangle
{
    double _x, _y;
    
    public RightTriangle(double x, double y)
    {
        _x = x;
        _y = y;
    }

    // Получаем площадь
    public double GetArea()
    {
        return _x * _y * 0.5;
    }
    
    // перегруз ToString
    public override string ToString()
    {
        return $"X: {_x} Y: {_y}";
    }

    // оператор ++
    public static RightTriangle operator ++(RightTriangle triangle)
    {
        triangle._x *= 2;
        triangle._y *= 2;
        return triangle;
    }

    // оператор --
    public static RightTriangle operator --(RightTriangle triangle)
    {
        triangle._x /= 2;
        triangle._y /= 2;
        return triangle;
    }

    // неявное в дабл
    public static explicit operator double(RightTriangle triangle) => triangle.GetArea();

    // явное в булл
    public static implicit operator bool(RightTriangle triangle)
    {
        if(triangle._x >= 0 && triangle._y >= 0) return true;

        return false; 
    }

    // оператор меньше или ровняется
    public static bool operator <=(RightTriangle triangle1, RightTriangle triangle2)
    {
        return triangle1.GetArea() <= triangle2.GetArea();
    }

    // оператор больше или ровняется
    public static bool operator >=(RightTriangle triangle1, RightTriangle triangle2)
    {
        return triangle1.GetArea() >= triangle2.GetArea();
    }


}