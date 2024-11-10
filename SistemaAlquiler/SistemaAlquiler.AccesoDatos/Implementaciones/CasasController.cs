using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;






namespace SistemaAlquiler.AccesoDatos.Implementaciones
{
    [Route("api/casas")]
    [ApiController]
    public class CasasController : ControllerBase {
        private readonly DB_Context db;
        public CasasController(DB_Context db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult obtenerTodas()
        {
            var casas = db.Casas.ToList();
            return Ok(casas);
        }

        [HttpGet("{id}")]
        public ActionResult obtenerPorID(int id)
        {
            var casa = db.Casas.Find(id);
            if(casa == null)
                return NotFound();
            return Ok(casa);
        }
    }
}
