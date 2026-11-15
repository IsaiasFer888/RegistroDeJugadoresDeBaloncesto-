
namespace RegistroJugadoresBaloncestoPOO.Models;

public class Jugador
{
  
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Edad { get; set; }
    public int NumeroCamiseta { get; set; }

  
    public Jugador(int id, string nombre, string apellido,
                   int edad, int numeroCamiseta)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Edad = edad;
        NumeroCamiseta = numeroCamiseta;
    }

    
    public string NombreCompleto => Nombre + " " + Apellido;

    
    public void MostrarInformacion()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Nombre: {NombreCompleto}");
        Console.WriteLine($"Edad: {Edad}");
        Console.WriteLine($"Número de camiseta: {NumeroCamiseta}");
    }
}
