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
            AppDomain.CurrentDomain.ProcessExit -= (o, s) => { Printer.WriteTitle("Terminación Adicional"); };
            AppDomain.CurrentDomain.ProcessExit -= AccionEvento;
            AppDomain.CurrentDomain.ProcessExit += AccionEvento;
            AppDomain.CurrentDomain.ProcessExit -= AccionEvento;

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

            //Se agrega la clase de reporteador
            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var listReporte1 = reporteador.GetListaEscuela();
            var listReporte2 = reporteador.GetListaEvaluacion();
            var listReporte3 = reporteador.GetListaAsignatura();
            var listReporte4 = reporteador.GetListEvaluacionAsig();
            var listReporte5 = reporteador.GetPromedAlumnAsig();

            #region Imprimir Promedio Alumnos
            bool printPromedioAlumnos = false;
            if (printPromedioAlumnos)
            {
                Printer.WriteTitle("Promedio Alumnos");

                foreach (var promedioAlumno in listReporte5)
                {
                    Printer.DrawLine();
                    WriteLine(promedioAlumno.Key.ToUpper());
                    foreach (var promedioAlumnoValue in promedioAlumno.Value)
                    {
                        Printer.DrawLine();
                        WriteLine($"AlumnoId:{promedioAlumnoValue.AlumnoId}");
                        WriteLine($"AlumnoNombre:{promedioAlumnoValue.AlumnoNombre}");
                        WriteLine($"Promedio:{promedioAlumnoValue.Promedio:N2}");
                        Printer.DrawLine();
                    }
                    Printer.DrawLine();
                }
            }
            #endregion

            Printer.WriteTitle("Mejores Promedio Alumnos");
            Printer.DrawLine();
            var mejoresPromedios = reporteador.GetPromedAlumnAsigMejores(2);
            foreach (var promMej in mejoresPromedios)
            {
                Printer.WriteTitle(promMej.Key);
                foreach (var promMejValue in promMej.Value)
                {
                    Printer.DrawLine();
                    WriteLine(@$"
                    AlumnoId:{promMejValue.AlumnoId},
                    AlumnoNombre:{promMejValue.AlumnoNombre},
                    Promedio:{promMejValue.Promedio:N2}
                    ");
                    Printer.DrawLine();
                }
            }
            Printer.DrawLine();
            #region UI Consola
            bool uiConsola = true;
            if (uiConsola)
            {
                Clear();
                Printer.WriteTitle("Captura de una Nueva Evaluacion de Consola");
                //Printer.Beep(750, 1500, 2);
                var newEvalConsole = new Evaluacion();
                string nombreEvaluacion, notaEvaluacionStr;

                WriteLine("Ingrese el nombre de la evaluación");
                Printer.PresioneEnter();
                nombreEvaluacion = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nombreEvaluacion))
                {
                    Printer.WriteTitle("El valor del nombre no puede ser vacio");
                    WriteLine("Saliendo del Programa");
                }
                else
                {
                    newEvalConsole.Nombre = nombreEvaluacion.ToLower();
                    WriteLine("El nombre de la evaluación ha sido ingresado correctamente");
                }

                WriteLine("Ingrese la nota de la evaluación");
                Printer.PresioneEnter();
                notaEvaluacionStr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(notaEvaluacionStr))
                {
                    Printer.WriteTitle("El valor de la nota no puede estar vacia");
                    WriteLine("Saliendo del Programa");
                }
                else
                {
                    try
                    {
                        newEvalConsole.Nota = float.Parse(notaEvaluacionStr);
                        if (newEvalConsole.Nota < 0 || newEvalConsole.Nota > 5)
                        {
                            throw new ArgumentOutOfRangeException("El valor debe estar entre 0 y 5");
                        }
                        WriteLine("La nota de la evaluación ha sido ingresada correctamente");
                    }
                    catch (ArgumentOutOfRangeException argEx)
                    {
                        Printer.WriteTitle(argEx.Message);
                        WriteLine("Saliendo del Programa");
                    }
                    catch
                    {
                        Printer.WriteTitle("Se ha detectado un valor no númerico");
                        WriteLine("Saliendo del Programa");
                    }
                    finally{
                        Printer.WriteTitle("Fatality");
                        Printer.Beep(1200,1000,2);
                    }
                }
            }
            #endregion



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
