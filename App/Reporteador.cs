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
    }
}