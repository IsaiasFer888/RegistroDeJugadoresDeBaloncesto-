
using RegistroJugadoresBaloncesto.Models;
using RegistroJugadoresBaloncestoPOO.Models;

namespace RegistroJugadoresBaloncestoPOO.Helpers;

public class JugadorHelper
{
    private List<Jugador> jugadores = new List<Jugador>();
    private List<Equipo> equipos = new List<Equipo>();
    private List<Posicion> posiciones = new List<Posicion>();

    public JugadorHelper()
    {
        
        equipos.Add(new Equipo(1, "Lakers", "Los Angeles"));
        equipos.Add(new Equipo(2, "Celtics", "Boston"));
        equipos.Add(new Equipo(3, "Warriors", "San Francisco"));
        equipos.Add(new Equipo(4, "Bulls", "Chicago"));
        equipos.Add(new Equipo(5, "Heat", "Miami"));

       
        posiciones.Add(new Posicion(1, "Base", "Organiza el juego"));
        posiciones.Add(new Posicion(2, "Escolta", "Especialista en anotación"));
        posiciones.Add(new Posicion(3, "Alero", "Jugador versátil"));
        posiciones.Add(new Posicion(4, "Ala-pívot", "Juega cerca del aro"));
        posiciones.Add(new Posicion(5, "Pívot", "Jugador interior"));
    }

   
    public void AgregarJugador()
    {
        Console.WriteLine("\n=== AGREGAR JUGADOR ===");

        string nombre = LeerTexto("Nombre: ");
        string apellido = LeerTexto("Apellido: ");
        int edad = LeerEntero("Edad: ", 1, 100);
        int numero = LeerEntero("Número de camiseta: ", 0, 99);

        ListarEquipos();
        int equipoId = LeerEntero("Seleccione el ID del equipo: ", 1, 5);

        ListarPosiciones();
        int posicionId = LeerEntero("Seleccione el ID de la posición: ", 1, 5);

        int nuevoId = jugadores.Count == 0
            ? 1
            : jugadores.Max(j => j.Id) + 1;

        Jugador nuevoJugador = new Jugador(
            nuevoId, nombre, apellido, edad,
            numero, equipoId, posicionId
        );

        jugadores.Add(nuevoJugador);

        Console.WriteLine("\n¡Jugador registrado exitosamente!");
        Console.WriteLine($"ID asignado: {nuevoId}");
    }

    
    public void ListarJugadores()
    {
        Console.WriteLine("\n=== LISTA DE JUGADORES ===");

        if (jugadores.Count == 0)
        {
            Console.WriteLine("No hay jugadores registrados.");
            return;
        }

        foreach (Jugador jugador in jugadores)
        {
            Equipo equipo = equipos.First(e => e.Id == jugador.EquipoId);
            Posicion posicion = posiciones.First(p => p.Id == jugador.PosicionId);

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"ID: {jugador.Id}");
            Console.WriteLine($"Nombre: {jugador.NombreCompleto}");
            Console.WriteLine($"Edad: {jugador.Edad}");
            Console.WriteLine($"Camiseta: {jugador.NumeroCamiseta}");
            Console.WriteLine($"Equipo: {equipo.Nombre}");
            Console.WriteLine($"Posición: {posicion.Nombre}");
        }

        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Total de jugadores: {jugadores.Count}");
    }

    
    public void BuscarJugador()
    {
        Console.WriteLine("\n=== BUSCAR JUGADOR ===");
        Console.WriteLine("1. Buscar por ID");
        Console.WriteLine("2. Buscar por nombre");
        Console.WriteLine("3. Buscar por equipo");
        Console.WriteLine("4. Buscar por posición");

        int opcion = LeerEntero("Seleccione una opción: ", 1, 4);

        List<Jugador> resultados = new List<Jugador>();

        switch (opcion)
        {
            case 1:
                int id = LeerEntero("Digite el ID: ", 1, int.MaxValue);
                resultados = jugadores.Where(j => j.Id == id).ToList();
                break;

            case 2:
                string nombre = LeerTexto("Digite el nombre: ");
                resultados = jugadores.Where(j =>
                    j.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)
                    || j.Apellido.Contains(nombre, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                break;

            case 3:
                ListarEquipos();
                int equipoId = LeerEntero("ID del equipo: ", 1, 5);
                resultados = jugadores.Where(j => j.EquipoId == equipoId).ToList();
                break;

            case 4:
                ListarPosiciones();
                int posicionId = LeerEntero("ID de la posición: ", 1, 5);
                resultados = jugadores.Where(j => j.PosicionId == posicionId).ToList();
                break;
        }

        if (resultados.Count == 0)
        {
            Console.WriteLine("No se encontraron jugadores.");
            return;
        }

        Console.WriteLine("\n=== RESULTADOS ===");

        foreach (Jugador jugador in resultados)
        {
            jugador.MostrarInformacion();
            Console.WriteLine("--------------------------------");
        }
    }

   
    public void ModificarJugador()
    {
        Console.WriteLine("\n=== MODIFICAR JUGADOR ===");

        int id = LeerEntero("Digite el ID del jugador: ", 1, int.MaxValue);

        Jugador jugador = jugadores.FirstOrDefault(j => j.Id == id);

        if (jugador == null)
        {
            Console.WriteLine("Jugador no encontrado.");
            return;
        }

        Console.WriteLine($"Modificando: {jugador.NombreCompleto}");

        jugador.Nombre = LeerTexto("Nuevo nombre: ");
        jugador.Apellido = LeerTexto("Nuevo apellido: ");
        jugador.Edad = LeerEntero("Nueva edad: ", 1, 100);
        jugador.NumeroCamiseta = LeerEntero("Nuevo número: ", 0, 99);

        ListarEquipos();
        jugador.EquipoId = LeerEntero("Nuevo ID del equipo: ", 1, 5);

        ListarPosiciones();
        jugador.PosicionId = LeerEntero("Nuevo ID de posición: ", 1, 5);

        Console.WriteLine("¡Jugador modificado exitosamente!");
    }

    
    public void EliminarJugador()
    {
        Console.WriteLine("\n=== ELIMINAR JUGADOR ===");

        int id = LeerEntero("Digite el ID del jugador: ", 1, int.MaxValue);

        Jugador jugador = jugadores.FirstOrDefault(j => j.Id == id);

        if (jugador == null)
        {
            Console.WriteLine("Jugador no encontrado.");
            return;
        }

        Console.WriteLine($"Jugador: {jugador.NombreCompleto}");
        Console.Write("¿Está seguro de eliminarlo? (S/N): ");

        string respuesta = Console.ReadLine() ?? "";

        if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            jugadores.Remove(jugador);
            Console.WriteLine("Jugador eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Eliminación cancelada.");
        }
    }

   
    public void ListarEquipos()
    {
        Console.WriteLine("\n=== EQUIPOS DISPONIBLES ===");

        foreach (Equipo equipo in equipos)
        {
            equipo.MostrarInformacion();
        }
    }

    
    public void ListarPosiciones()
    {
        Console.WriteLine("\n=== POSICIONES DISPONIBLES ===");

        foreach (Posicion posicion in posiciones)
        {
            posicion.MostrarInformacion();
        }
    }

   
    private string LeerTexto(string mensaje)
    {
        string valor;

        do
        {
            Console.Write(mensaje);
            valor = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(valor))
            {
                Console.WriteLine("El campo no puede estar vacío.");
            }

        } while (string.IsNullOrWhiteSpace(valor));

        return valor.Trim();
    }

    
    private int LeerEntero(string mensaje, int minimo, int maximo)
    {
        int valor;

        while (true)
        {
            Console.Write(mensaje);

            if (int.TryParse(Console.ReadLine(), out valor)
                && valor >= minimo && valor <= maximo)
            {
                return valor;
            }

            Console.WriteLine($"Digite un número entre {minimo} y {maximo}.");
        }
    }
}

