class Pedido
{
    private string direcEntrega;
    private string descArticulo;
    public string DirecEntrega
    {
        set
        {
            direcEntrega = value.ToUpper();
        }
    }
    public string DescArticulo
    {
        set
        {
            descArticulo = value.ToUpper();
        }
    }
    public string Detallar()
    {
        return "Direccion: " + direcEntrega + "\nArtículo: " + descArticulo;
    }
}