using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using GestionProductos.Models;
namespace GestionProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgregarController : ControllerBase
    {
        //Conexion con la base de datos
        private readonly string ConexionSQL;
        public AgregarController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        //Procedimiento para agregar registros a la BD
        [HttpPost]
        [Route("Agregar")]
        public IActionResult Agregar([FromBody] ProductoModelAgregar datos)
        {
            try
            {
                using (var conexion = new SqlConnection(ConexionSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("guardarProducto", conexion);
                    cmd.Parameters.AddWithValue("codigoBarra", datos.codigoBarra);
                    cmd.Parameters.AddWithValue("nombre", datos.nombre);
                    cmd.Parameters.AddWithValue("marca", datos.marca);
                    cmd.Parameters.AddWithValue("categoria", datos.categoria);
                    cmd.Parameters.AddWithValue("precio", datos.precio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "REGISTRO GUARDADO CON EXITO" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
