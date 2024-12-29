//Archivo tipo: controller comun api - api en blanco
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using GestionProductos.Models;


namespace GestionProductos.Controllers
{
    [EnableCors("reglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //Conexion con la base de datos
        private readonly string ConexionSQL;
        public ProductoController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        //Procedimiento para obtener registros existentes de la BD
        [HttpGet]
        [Route("lista")]
        public IActionResult Lista()
        {
            //Crea una lista para almacenar los datos
            List<ProductoModel> lista = new List<ProductoModel>();

            try
            {
                using (var conexion = new SqlConnection(ConexionSQL))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("listaProductos", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                lista.Add(new ProductoModel
                                {
                                    idProducto = Convert.ToInt32(rd["idProducto"]),
                                    codigoBarra = rd["codigoBarra"].ToString(),
                                    nombre = rd["nombre"].ToString(),
                                    marca = rd["marca"].ToString(),
                                    categoria = rd["categoria"].ToString(),
                                    precio = Convert.ToDecimal(rd["precio"])
                                });
                            }
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }
        }
    
        //Procedimiento para obtener un registro especifico de la BD
        [HttpGet]
        [Route("obtener/{idProducto:int}")]
        public ActionResult obtener(int idProducto)
        {
            List<ProductoModel> lista = new List<ProductoModel>();
            ProductoModel productoModel = new ProductoModel();

            try
            {
                using (var conexion = new SqlConnection(ConexionSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listaProductos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read()) {
                            lista.Add(new ProductoModel()
                            {
                                idProducto = Convert.ToInt32(rd["idProducto"]),
                                codigoBarra = rd["codigoBarra"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                marca = rd["marca"].ToString(),
                                categoria = rd["categoria"].ToString(),
                                precio = Convert.ToDecimal(rd["precio"])
                            });
                    }
                }
            }
                productoModel = lista.Where(item => item.idProducto == idProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = productoModel });
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = productoModel });
            }
        }

    }

}
