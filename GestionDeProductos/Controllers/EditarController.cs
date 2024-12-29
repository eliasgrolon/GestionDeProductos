using GestionProductos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GestionProductos.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EditarController : ControllerBase
    {
        //Conexion con la base de datos
        private readonly string ConexionSQL;
        public EditarController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        //Procedimiento para editar registros existentes de la bd
        [HttpPut]
        [Route("Editar")]
        public IActionResult editar([FromBody] ProductoModel datos)
        {
            try
            {
                using (var conexion = new SqlConnection(ConexionSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("editarProducto", conexion);
                    cmd.Parameters.AddWithValue("idProducto", datos.idProducto == 0 ? DBNull.Value : datos.idProducto);
                    cmd.Parameters.AddWithValue("codigoBarra", datos.codigoBarra is null ? DBNull.Value : datos.codigoBarra);
                    cmd.Parameters.AddWithValue("nombre", datos.nombre is null ? DBNull.Value : datos.nombre);
                    cmd.Parameters.AddWithValue("marca", datos.marca is null ? DBNull.Value : datos.marca);
                    cmd.Parameters.AddWithValue("categoria", datos.categoria is null ? DBNull.Value : datos.categoria);
                    cmd.Parameters.AddWithValue("precio", datos.precio == 0 ? DBNull.Value : datos.precio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "EDITADO CON EXITO" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
