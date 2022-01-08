using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using Fundamentos_CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela.App
{
    public sealed class EscuelaEngine
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
            var rnd = new Random(System.Environment.TickCount);
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {

                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion()
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre}_Ev#{i + 1}",
                                Nota = MathF.Round(5 * (float)rnd.NextDouble(), 2),
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

        public void ImprimirDiccionarioObjetos(Dictionary<LlavesDiccionarioEnum, IEnumerable<ObjetoEscuelaBase>> dic, bool HasEvaluation = false, bool WithSwitch = true)
        {
            foreach (var obj in dic)
            {
                if (HasEvaluation)
                {
                    Printer.WriteTitle(obj.Key.ToString());
                }
                else
                {
                    if (obj.Key.ToString() != LlavesDiccionarioEnum.Evaluaciones.ToString())
                    {
                        Printer.WriteTitle(obj.Key.ToString());
                    }
                }

                foreach (var val in obj.Value)
                {
                    if (WithSwitch)
                    {
                        switch (obj.Key)
                        {
                            case LlavesDiccionarioEnum.Escuela:
                                Console.WriteLine($"{obj.Key}: {val.ToString()}");
                                break;
                            case LlavesDiccionarioEnum.Alumnos:
                                Console.WriteLine($"{obj.Key}: {val.ToString()}");
                                break;
                            case LlavesDiccionarioEnum.Cursos:
                                var curtmp = val as Curso;
                                if(curtmp != null)
                                {
                                    int count = curtmp.Alumnos.Count;
                                    Console.WriteLine("Curso: " + val.Nombre + " Cantidad Alumnos: " + count);
                                }
                                break;
                            case LlavesDiccionarioEnum.Evaluaciones:
                                if (HasEvaluation)
                                    Console.WriteLine($"{obj.Key}: {val.ToString()}");
                                break;
                            default:
                                Console.WriteLine($"{obj.Key}: {val.ToString()}");
                                break;
                        }
                    }
                    else
                    {
                        if (val is Evaluacion)
                        {
                            if (HasEvaluation)
                            {
                                Console.WriteLine($"{val.GetType().Name}: {val.ToString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{val.GetType().Name}: {val.ToString()}");
                        }
                    }

                }
            }
        }

        public Dictionary<LlavesDiccionarioEnum, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var dicc = new Dictionary<LlavesDiccionarioEnum, IEnumerable<ObjetoEscuelaBase>>();
            dicc.Add(LlavesDiccionarioEnum.Escuela, new[] { Escuela });
            dicc.Add(LlavesDiccionarioEnum.Cursos, Escuela.Cursos);

            var lstAlumnos = new List<Alumno>();
            var lstAsignaturas = new List<Asignatura>();
            var listEvaluacion = new List<Evaluacion>();

            Escuela.Cursos.ForEach(curso =>
            {
                lstAsignaturas.AddRange(curso.Asignaturas);
                lstAlumnos.AddRange(curso.Alumnos);
                lstAlumnos.ForEach(alumno =>
                {
                    listEvaluacion.AddRange(alumno.Evaluaciones);
                });
            });

            dicc.Add(LlavesDiccionarioEnum.Asignaturas, lstAsignaturas);
            dicc.Add(LlavesDiccionarioEnum.Alumnos, lstAlumnos);
            dicc.Add(LlavesDiccionarioEnum.Evaluaciones, listEvaluacion);

            return dicc;
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelaBase(
                    bool hasEvaluations = true,
                    bool hasAlumnos = true,
                    bool hasSignatures = true,
                    bool hasCourses = true
                    )
        {
            return GetObjetoEscuelaBase(out int blank, out blank, out blank, out blank,
            hasEvaluations, hasAlumnos, hasSignatures, hasCourses);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelaBase(
            out int countEvaluations,
            out int countAlumnos,
            out int countSignatures,
            out int countCourses,
            bool hasEvaluations = true,
            bool hasAlumnos = true,
            bool hasSignatures = true,
            bool hasCourses = true
            )
        {
            var listOb = new List<ObjetoEscuelaBase>();
            listOb.Add(Escuela);

            countCourses = countSignatures = countAlumnos = countEvaluations = 0;

            if (hasCourses)
            {
                listOb.AddRange(Escuela.Cursos);
                countCourses += Escuela.Cursos.Count();

                foreach (var curso in Escuela.Cursos)
                {
                    countSignatures += curso.Asignaturas.Count;
                    countAlumnos += curso.Alumnos.Count;

                    if (hasSignatures)
                        listOb.AddRange(curso.Asignaturas);

                    if (hasAlumnos)
                        listOb.AddRange(curso.Alumnos);

                    if (hasEvaluations)
                    {
                        foreach (var alumno in curso.Alumnos)
                        {
                            listOb.AddRange(alumno.Evaluaciones);
                            countEvaluations += alumno.Evaluaciones.Count();
                        }

                    }
                }
            }
            return listOb;
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelaBase()
        {
            var listOb = new List<ObjetoEscuelaBase>();
            listOb.Add(Escuela);
            listOb.AddRange(Escuela.Cursos);

            foreach (var curso in Escuela.Cursos)
            {
                listOb.AddRange(curso.Asignaturas);
                listOb.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    listOb.AddRange(alumno.Evaluaciones);
                }
            }
            return listOb;
        }
    }
}