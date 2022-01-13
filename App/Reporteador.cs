using System;
using System.Linq;
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

        public IEnumerable<Escuela> GetListaEscuela(){
            IEnumerable<Escuela> listEscuela = null;
            //var listEscuela = _diccionario.GetValueOrDefault(LlavesDiccionarioEnum.Escuela);
            
            if (_diccionario.TryGetValue(LlavesDiccionarioEnum.Escuela, out var lista))
            {
                listEscuela = lista.Cast<Escuela>();
            }
            
            return listEscuela;
        }

        public IEnumerable<Evaluacion> GetListaEvaluacion(){
            IEnumerable<Evaluacion> listEscuela = new List<Evaluacion>();
            //var listEscuela = _diccionario.GetValueOrDefault(LlavesDiccionarioEnum.Evaluacion);
            
            if (_diccionario.TryGetValue(LlavesDiccionarioEnum.Evaluaciones, out var lista))
            {
                return lista.Cast<Evaluacion>();
            }
            
            return listEscuela;
        }
        public IEnumerable<string> GetListaAsignatura(){
            return GetListaAsignatura(out IEnumerable<Evaluacion> listEvaluaciones);
        }

        public IEnumerable<string> GetListaAsignatura(out IEnumerable<Evaluacion> listEvaluaciones){
            listEvaluaciones = GetListaEvaluacion();

            // Investigar Compare o Comparison
            var listAsignatura = (from data in listEvaluaciones
                select data.Asignatura.Nombre).Distinct();

            return listAsignatura;

        }

        public Dictionary<string,IEnumerable<Evaluacion>> GetListEvaluacionAsig(){
            var dicEvalAsig = new Dictionary<string,IEnumerable<Evaluacion>>();
            
            var listAsignatura = GetListaAsignatura(out var listEvaluaciones);

            foreach (var asignatura in listAsignatura)
            {
                var listRange = listEvaluaciones.Where(x => x.Asignatura.Nombre == asignatura);
                
                if (!dicEvalAsig.ContainsKey(asignatura))
                {
                    dicEvalAsig.Add(asignatura,listRange);
                }
            }

            return dicEvalAsig;
        }

        public Dictionary<string, IEnumerable<dynamic>> GetPromedAlumnAsig(){
            var report =  new Dictionary<string, IEnumerable<dynamic>>();
            var dicEvalAsig = GetListEvaluacionAsig();
            foreach (var asigEval in dicEvalAsig)
            {
                var AlumNota = from ev in asigEval.Value
                            select new {
                                ev.Alumno.UniqueId,
                                ev.Nota
                            };
            }

            return report;
        }
    }
}