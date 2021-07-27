using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DibujarLinea(int len = 100)
        {
            WriteLine(new string('=', len));
        }

        public static void EscribirTitulo(string titulo)
        {
            DibujarLinea((titulo.Length) * 2);
            WriteLine(titulo.ToUpper());
            DibujarLinea((titulo.Length) * 2);
        }
    }
}