using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DrawLine(int len = 100)
        {
            WriteLine(new string('=', len));
        }

        public static void PresioneEnter()
        {
            WriteLine("Presione ENTER para continuar...");
        }

        public static void WriteTitle(string titulo)
        {
            DrawLine((titulo.Length) * 2);
            WriteLine(titulo.ToUpper());
            DrawLine((titulo.Length) * 2);
        }

        public static void Beep(int hz = 2000, int tiempo=500, int cantidad =1)
        {
            while (cantidad-- > 0)
            {
                System.Console.Beep(hz, tiempo);
            }
        }
    }
}