﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    [Serializable]
    public class EstadoDispositivoGrupo
    {
        //public ArrayList _listaDispositivos = new ArrayList();
        public Dictionary<string, InfoDispositivo> _Dispositivos = new Dictionary<string, InfoDispositivo>();
    }
}
