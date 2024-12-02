namespace SistemaAlquiler.API.Utilidades
{
    public class RespuestaGenerica<TObject>
    {
        public bool estado {  get; set; }
        public string? mensaje { get; set; }
        public TObject? objeto { get; set; }    
        public List<TObject>? listaObjeto { get; set; }
    }
}
