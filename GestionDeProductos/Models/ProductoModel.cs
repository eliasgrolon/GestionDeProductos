namespace GestionProductos.Models
{
    public class ProductoModel
    {
        public int idProducto { get; set; }
        public string codigoBarra { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string categoria { get; set; }
        public decimal precio { get; set; }
    }
}
