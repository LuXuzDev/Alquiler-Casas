﻿using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class CrearCasaDTO
{
    public int idCiudad { get; set; }
    public int idUsuario { get; set; }
    public string descripcion { get; set; }
    public double precioNoche { get; set; }
    public double precioMes { get; set; }
    public double areaTotal { get; set; }
    public CrearCaracteristicasDTO caracteristicasDTO { get; set; }


}