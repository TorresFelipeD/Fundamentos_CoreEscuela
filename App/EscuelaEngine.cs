using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using static System.Console;

namespace CoreEscuela.App
{
    public class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {
        }

        public void Inicializar()
        {
            Escuela = new Escuela(
                NombreEscuela: "Platzi",
                AñoGraduacion: 2012,
                Pais: "Colombia",
                Ciudad: "Bogota",
                TipoEscuela: TiposEscuela.Primaria
                );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();
        }

        private void CargarEvaluaciones()
        {
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);

                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion(){
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre}_Ev#{i + 1}",
                                Nota = (float)(5 * rnd.NextDouble()),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                List<Asignatura> listaAsignatura = new List<Asignatura>(){
                    new Asignatura() { Nombre = "Matemáticas"},
                    new Asignatura() { Nombre = "Educación Física"},
                    new Asignatura() { Nombre = "Castellano"},
                    new Asignatura() { Nombre = "Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignatura;
            }
        }

        private List<Alumno> GenerarAlumnos(int cantidad = 40)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno() { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((x) => x.UniqueId).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                new Curso(){ Nombre = "101",TipoJornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "201",TipoJornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "301",TipoJornada = TiposJornada.Mañana }
            };

            Escuela.Cursos.Add(new Curso() { Nombre = "102", TipoJornada = TiposJornada.Tarde });
            Escuela.Cursos.Add(new Curso() { Nombre = "202", TipoJornada = TiposJornada.Tarde });
            Escuela.Cursos.Add(new Curso() { Nombre = "302", TipoJornada = TiposJornada.Tarde });

            List<Curso> otrosCursos = new List<Curso>(){
                new Curso(){ Nombre = "401",TipoJornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "501",TipoJornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "601",TipoJornada = TiposJornada.Mañana }
            };

            List<Curso> cursosVacacionales = new List<Curso>(){
                new Curso(){ Nombre = "401.Vacacional",TipoJornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "501.Vacacional",TipoJornada = TiposJornada.Tarde },
                new Curso(){ Nombre = "601.Vacacional",TipoJornada = TiposJornada.Tarde }
            };
            Escuela.Cursos.AddRange(otrosCursos);
            Escuela.Cursos.AddRange(cursosVacacionales);

            Random rdm = new Random();

            foreach (var curso in Escuela.Cursos)
            {
                int cantRandom = rdm.Next(10, 30);
                curso.Alumnos = GenerarAlumnos(cantRandom);
            }
        }
    }
}