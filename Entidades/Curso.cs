using System;
using System.Collections.Generic;
using CoreEscuela.Util;
using Fundamentos_CoreEscuela.Entidades;

namespace CoreEscuela.Entidades
{
    public class Curso : ObjetoEscuelaBase, IPlace
    {
        public TiposJornada TipoJornada { get; set; }
        public List<Asignatura> Asignaturas {get; set;}
        public List<Alumno> Alumnos {get; set;}

        public string Address { get; set; }

        public void ClearPlace(){
            Printer.DrawLine();
            Console.WriteLine($"Curso {Nombre} se esta limpiando");
        }
    }
}