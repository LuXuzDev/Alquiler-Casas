﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface IUtilidadesServicio
{
    string generarClave();
    string convertirSha256(string clave);
}