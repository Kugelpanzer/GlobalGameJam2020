using System;

public static class Converter
{
    public static int ConvX(int x, int y)
    {
        return x + (int)Math.Floor((double)y / 2);
    }
}
