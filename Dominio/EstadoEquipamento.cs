using Metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class EstadoEquipamento
    {
        public Dictionary<int, bool> DicVentilador = new Dictionary<int, bool>();
        public Dictionary<int, bool> DicLampada = new Dictionary<int, bool>();
        public Dictionary<int, bool> DicUmidificador = new Dictionary<int, bool>();
        public Dictionary<string, Dictionary<int, bool>> DicEstados = new Dictionary<string, Dictionary<int, bool>>();
    }
}
