using GestionProductos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GestionProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EliminarController : ControllerBase
    {
        //Conexion con la base de datos
        private readonly string ConexionSQL;
        public EliminarController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        [HttpDelete]
        [Route("eliminar/{idProducto:int}")]
        public IActionResult eliminar(int idProducto)
        {
            try
            {
                using (var conexion = new SqlConnection(ConexionSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("eliminarProducto", conexion);
                    cmd.Parameters.AddWithValue("idProducto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ELIMINADO CON EXITO" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
