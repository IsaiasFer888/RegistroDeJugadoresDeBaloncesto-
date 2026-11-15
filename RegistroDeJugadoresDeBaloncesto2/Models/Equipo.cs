
namespace RegistroJugadoresBaloncesto.Models;

public class Equipo
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Equipo(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}

