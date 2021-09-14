using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DrawLine(int len = 100)
        {
            WriteLine(new string('=', len));
        }

        public static void WriteTitle(string titulo)
        {
            DrawLine((titulo.Length) * 2);
            WriteLine(titulo.ToUpper());
            DrawLine((titulo.Length) * 2);
        }
    }
}