using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Alumno
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }
        public List<Evaluacion> Evaluaciones { get; set; }
        public Alumno()
        {
            this.UniqueId = Guid.NewGuid().ToString();
            Evaluaciones = new List<Evaluacion>();
        }
    }
}