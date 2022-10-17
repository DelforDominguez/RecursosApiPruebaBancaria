namespace RecursosApiPruebaBancaria.Modelos
{
    public class ListarReporteMetaVentas
    {
        public string Relacion { get; set; }
        public int IdGerente { get; set; }
        public string Gerente { get; set; }
        public int IdVendedor { get; set; }
        public string Vendedor { get; set; }
        public decimal MetaActual { get; set; }
        public decimal PuntoTotales { get; set; }
        public decimal MontoTotales { get; set; }

    }
}
