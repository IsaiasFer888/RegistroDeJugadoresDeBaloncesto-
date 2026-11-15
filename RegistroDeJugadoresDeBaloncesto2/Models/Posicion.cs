
namespace RegistroJugadoresBaloncesto.Models;

public class Posicion
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Posicion(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}
