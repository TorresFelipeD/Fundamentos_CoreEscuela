using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Curso : ObjetoEscuelaBase
    {
        public TiposJornada TipoJornada { get; set; }
        public List<Asignatura> Asignaturas {get; set;}
        public List<Alumno> Alumnos {get; set;}
    }
}