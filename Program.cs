using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using Fundamentos_CoreEscuela.Entidades;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionEvento;
            AppDomain.CurrentDomain.ProcessExit += (o,s) => { Printer.WriteTitle("Terminación Adicional");};
            AppDomain.CurrentDomain.ProcessExit -= AccionEvento;
            AppDomain.CurrentDomain.ProcessExit += AccionEvento;

            Printer.DrawLine();

            EscuelaEngine engine = new EscuelaEngine();
            engine.Inicializar();

            engine.Escuela.ClearPlace();

            Printer.WriteTitle("ESCUELA");
            WriteLine(engine.Escuela.ToString());
            Printer.DrawLine();

            ImprimirCursos(engine.Escuela);
            Printer.DrawLine();

            engine.Escuela.Cursos.RemoveAll((curso) => curso.Nombre.Contains("Vacacional") && curso.TipoJornada == TiposJornada.Tarde);
            ImprimirCursos(engine.Escuela);
            Printer.DrawLine();

            Printer.WriteTitle("Alumnos");

            Printer.DrawLine(50);
            Printer.WriteTitle("Pruebas de Polimorfismo");

            Printer.WriteTitle("Alumno");
            var alumno01 = new Alumno
            {
                Nombre = "Angel Di Maria"
            };
            WriteLine($"Alumno: {alumno01.Nombre}");
            WriteLine($"UniqueID: {alumno01.UniqueId}");

            Printer.WriteTitle("Objeto Base");
            ObjetoEscuelaBase objetoEscuelaBase = alumno01;
            WriteLine($"Alumno: {objetoEscuelaBase.Nombre}");
            WriteLine($"UniqueID: {objetoEscuelaBase.UniqueId}");

            var evaluation1 = new Evaluacion()
            {
                Nombre = "Evaluacion de Matematicas",
                Nota = 4.5f
            };

            Printer.WriteTitle("Evaluacion");
            WriteLine($"Evaluacion: {evaluation1.Nombre}");
            WriteLine($"UniqueID: {evaluation1.UniqueId}");
            WriteLine($"Nota: {evaluation1.Nota}");
            WriteLine($"Tipo: {evaluation1.GetType()}");

            if (objetoEscuelaBase is Alumno)
            {
                Alumno objetoEscuelaBaseAlumno = (Alumno)objetoEscuelaBase;
            }
            //alumno01 = (Alumno)(ObjetoEscuelaBase)evaluation1;
            Alumno objetoEscuelaBaseAlumno2 = objetoEscuelaBase as Alumno;

            var listObjetoEscuela = engine.GetObjetoEscuelaBase();

            var listaILugar = from ob in listObjetoEscuela
                              where ob is IPlace
                              select (IPlace)ob;

            Printer.WriteTitle("Metodo Escuela Base");
            var listObjetoEscuelaV2 = engine.GetObjetoEscuelaBase(
                out int countEvaluations,
                out int countAlumnos,
                out int countSignatures,
                out int countCourses,
                true, true, true, true);

            
            /*
            Al declararse un parametro de salida se puede realizar la creación  de una variable tipo
            "dummy" con el fin de se reciclada.
            e.g. 
                var listObjetoEscuelaV2 = engine.GetObjetoEscuelaBase(
                    out int countEvaluations,
                    out int dummy, //Creación de variable
                    out dummy, //Reutilización de variable no requerida
                    out dummy, //Reutilización de variable no requerida
                    true,true,true,true);
            */

            var listObjetoEscuelaV3 = engine.GetObjetoEscuelaBase(hasEvaluations: true);

            Printer.DrawLine();

            Printer.WriteTitle("GetObjetosEscuela");
            var getDiccObj = engine.GetDiccionarioObjetos();
            Printer.DrawLine();

            Printer.WriteTitle("ImprimirDiccionarioObjetos");
            engine.ImprimirDiccionarioObjetos(getDiccObj);
            Printer.WriteTitle("Fin de la Impresión de Objetos del Diccionario");
            Printer.DrawLine();
        }

        private static void AccionEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Terminando Ejecución");
            Printer.WriteTitle("Ejecución Terminada");
        }

        private static void ImprimirCursos(Escuela escuela)
        {
            if (escuela?.Cursos != null)
            {
                Printer.WriteTitle("CURSOS");
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Id: {curso.UniqueId}    Nombre Curso: {curso.Nombre}    Tipo de Jornada: {curso.TipoJornada}");
                }
            }
            else
            {
                if (escuela == null)
                {
                    Printer.WriteTitle("NO HAY ESCUELAS REGISTRADAS");
                }
                else
                {
                    Printer.WriteTitle("NO HAY CURSOS REGISTRADOS");

                }
            }
        }
    }
}
