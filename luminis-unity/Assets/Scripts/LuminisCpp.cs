using System.Runtime.InteropServices;

public static class LuminisCpp {
    [DllImport("LuminisCpp", CallingConvention = CallingConvention.Cdecl)]
    public static extern int sum(int x, int y);
}
