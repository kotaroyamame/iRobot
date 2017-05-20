//=============================
// 出力関連
//=============================
// 文字列の出力
protected void OutputString(string str)
{
    ObjectBase.GetObject("MdiInstance").AddMessage(str);
}

// 整数の出力
protected void OutputInteger(int i)
{
    ObjectBase.GetObject("MdiInstance").AddMessage(i.ToString());
}

// 浮動小数の出力
protected void OutputFloat(double f)
{
    ObjectBase.GetObject("MdiInstance").AddMessage(f.ToString());
}

//=============================
// 数学関連
//=============================
// sin
protected double Sin(double a)
{
    return Math.Sin(a);
}

// cos
protected double Cos(double d)
{
    return Math.Cos(d);
}

// tan
protected double Tan(double a)
{
    return Math.Tan(a);
}

// Asin
protected double Asin(double d)
{
    return Math.Asin(d);
}

// Acos
protected double Acos(double d)
{
    return Math.Acos(d);
}

// Atan
protected double Atan(double d)
{
    return Math.Atan(d);
}

//=============================
// 乱数
//=============================
private Random _Random = new Random((int)DateTime.Now.Ticks);

// 乱数の取得
protected int GetRandomValue()
{
    return _Random.Next();
}
