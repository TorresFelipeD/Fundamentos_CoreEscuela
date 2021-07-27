using System;
using System.Collections.Generic;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            Printer.DibujarLinea();

            EscuelaEngine engine = new EscuelaEngine();
            engine.Inicializar();

            Printer.EscribirTitulo("ESCUELA");
            WriteLine(engine.Escuela.ToString());
            Printer.DibujarLinea();

            ImprimirCursos(engine.Escuela);
            Printer.DibujarLinea();

            engine.Escuela.Cursos.RemoveAll((curso) => curso.Nombre.Contains("Vacacional") && curso.TipoJornada == TiposJornada.Tarde);
            ImprimirCursos(engine.Escuela);
            Printer.DibujarLinea();

            Printer.EscribirTitulo("Alumnos");

        }

        private static void ImprimirCursos(Escuela escuela)
        {
            if (escuela?.Cursos != null)
            {
                Printer.EscribirTitulo("CURSOS");
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Id: {curso.UniqueId}    Nombre Curso: {curso.Nombre}    Tipo de Jornada: {curso.TipoJornada}");
                }
            }
            else
            {
                if (escuela == null)
                {
                    Printer.EscribirTitulo("NO HAY ESCUELAS REGISTRADAS");
                }
                else
                {
                    Printer.EscribirTitulo("NO HAY CURSOS REGISTRADOS");
                    
                }
            }
        }
    }
}
