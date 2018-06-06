using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class EstadoEquipamento
    {
        public Dictionary<int, bool> Numero = new Dictionary<int, bool>();
        public Dictionary<string, Dictionary<int, bool>> Equipamento = new Dictionary<string, Dictionary<int, bool>>();
    }
}
