using System.Runtime.InteropServices;

public static class HypatiaCpp {
    [DllImport("HypatiaCpp", CallingConvention = CallingConvention.Cdecl)]
    public static extern int sum(int x, int y);
}
