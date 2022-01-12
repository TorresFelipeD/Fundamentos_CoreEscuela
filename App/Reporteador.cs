using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;
using Fundamentos_CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlavesDiccionarioEnum, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlavesDiccionarioEnum, IEnumerable<ObjetoEscuelaBase>> dicObjEscuela){
            if (dicObjEscuela == null)
                throw new ArgumentNullException(nameof(dicObjEscuela));

            _diccionario = dicObjEscuela;
        }

        public IEnumerable<Evaluacion> GetListaEvaluaciones(){
            var dicc = new List<Evaluacion>();
            //dicc.AddRange(_diccionario[LlavesDiccionarioEnum.Evaluaciones]);

            return dicc;
        }
    }
}