List<int> ids = new List<int>();

Dictionary<int, string> nombres = new Dictionary<int, string>();
Dictionary<int, string> apellidos = new Dictionary<int, string>();
Dictionary<int, int> edades = new Dictionary<int, int>();
Dictionary<int, int> numerosCamiseta = new Dictionary<int, int>();
Dictionary<int, string> equipos = new Dictionary<int, string>();
Dictionary<int, string> posiciones = new Dictionary<int, string>();

int siguienteId = 1;

Console.WriteLine("======================================");
Console.WriteLine("   REGISTRO DE JUGADORES DE BALONCESTO");
Console.WriteLine("======================================");
Console.WriteLine("Sistema de administración de jugadores");

bool continuar = true;

while (continuar)
{
    Console.WriteLine("\n========== MENÚ PRINCIPAL ==========");
    Console.WriteLine("1. Agregar jugador");
    Console.WriteLine("2. Modificar jugador");
    Console.WriteLine("3. Listar jugadores");
    Console.WriteLine("4. Buscar jugadores");
    Console.WriteLine("5. Eliminar jugador");
    Console.WriteLine("6. Salir");
    Console.Write("Seleccione una opción: ");

    if (!int.TryParse(Console.ReadLine(), out int opcion))
    {
        Console.WriteLine("Error: debe introducir un número.");
        continue;
    }

    switch (opcion)
    {
        case 1:
            AgregarJugador();
            break;

        case 2:
            ModificarJugador();
            break;

        case 3:
            ListarJugadores();
            break;

        case 4:
            BuscarJugador();
            break;

        case 5:
            EliminarJugador();
            break;

        case 6:
            Console.WriteLine("¿Está seguro que desea salir? (S/N)");
            string respuesta = Console.ReadLine() ?? "";

            if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                continuar = false;
                Console.WriteLine("Cerrando el sistema...");
            }
            break;

        default:
            Console.WriteLine("Opción no válida. Intente nuevamente.");
            break;
    }
}

Console.WriteLine("Gracias por utilizar el sistema.");

string LeerTexto(string mensaje)
{
    string valor;

    do
    {
        Console.Write(mensaje);
        valor = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(valor))
        {
            Console.WriteLine("Este campo no puede estar vacío.");
        }

    } while (string.IsNullOrWhiteSpace(valor));

    return valor.Trim();
}


int LeerNumero(string mensaje, int minimo, int maximo)
{
    int numero;

    while (true)
    {
        Console.Write(mensaje);

        if (int.TryParse(Console.ReadLine(), out numero))
        {
            if (numero >= minimo && numero <= maximo)
            {
                return numero;
            }
        }

        Console.WriteLine($"Introduzca un número entre {minimo} y {maximo}.");
    }
}


void AgregarJugador()
{
    Console.WriteLine("\n========== AGREGAR JUGADOR ==========");

    int id = siguienteId++;

    string nombre = LeerTexto("Digite el nombre: ");
    string apellido = LeerTexto("Digite el apellido: ");

    int edad = LeerNumero("Digite la edad: ", 1, 100);

    int camiseta = LeerNumero("Número de camiseta: ", 0, 99);

    string equipo = LeerTexto("Digite el equipo: ");
    string posicion = LeerTexto("Digite la posición: ");

    ids.Add(id);

    nombres.Add(id, nombre);
    apellidos.Add(id, apellido);
    edades.Add(id, edad);
    numerosCamiseta.Add(id, camiseta);
    equipos.Add(id, equipo);
    posiciones.Add(id, posicion);

    Console.WriteLine("\n¡Jugador registrado exitosamente!");
    Console.WriteLine($"ID asignado: {id}");
}


void ModificarJugador()
{
    Console.WriteLine("\n========== MODIFICAR JUGADOR ==========");

    if (ids.Count == 0)
    {
        Console.WriteLine("No hay jugadores registrados.");
        return;
    }

    int id = LeerNumero("Digite el ID del jugador: ", 1, int.MaxValue);

    if (!nombres.ContainsKey(id))
    {
        Console.WriteLine("No existe un jugador con ese ID.");
        return;
    }

    Console.WriteLine($"\nJugador: {nombres[id]} {apellidos[id]}");
    Console.WriteLine("Introduzca los nuevos datos:");

    nombres[id] = LeerTexto("Nuevo nombre: ");
    apellidos[id] = LeerTexto("Nuevo apellido: ");
    edades[id] = LeerNumero("Nueva edad: ", 1, 100);
    numerosCamiseta[id] = LeerNumero("Nuevo número de camiseta: ", 0, 99);
    equipos[id] = LeerTexto("Nuevo equipo: ");
    posiciones[id] = LeerTexto("Nueva posición: ");

    Console.WriteLine("\n¡Jugador modificado exitosamente!");
}


void ListarJugadores()
{
    Console.WriteLine("\n========== LISTA DE JUGADORES ==========");

    if (ids.Count == 0)
    {
        Console.WriteLine("No hay jugadores registrados.");
        return;
    }

    foreach (int id in ids)
    {
        MostrarJugador(id);
    }
}


void MostrarJugador(int id)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"ID: {id}");
    Console.WriteLine($"Nombre: {nombres[id]} {apellidos[id]}");
    Console.WriteLine($"Edad: {edades[id]} años");
    Console.WriteLine($"Número de camiseta: {numerosCamiseta[id]}");
    Console.WriteLine($"Equipo: {equipos[id]}");
    Console.WriteLine($"Posición: {posiciones[id]}");
}


void BuscarJugador()
{
    Console.WriteLine("\n========== BUSCAR JUGADOR ==========");
    Console.WriteLine("1. Buscar por ID");
    Console.WriteLine("2. Buscar por nombre");
    Console.WriteLine("3. Buscar por equipo");
    Console.WriteLine("4. Buscar por posición");

    int opcion = LeerNumero("Seleccione el criterio: ", 1, 4);

    if (ids.Count == 0)
    {
        Console.WriteLine("No hay jugadores registrados.");
        return;
    }

    bool encontrado = false;

    switch (opcion)
    {
        case 1:
            int id = LeerNumero("Digite el ID: ", 1, int.MaxValue);

            if (nombres.ContainsKey(id))
            {
                MostrarJugador(id);
                encontrado = true;
            }
            break;

        case 2:
            string nombreBuscado = LeerTexto("Digite el nombre: ");

            foreach (int jugadorId in ids)
            {
                if (nombres[jugadorId].Contains(nombreBuscado,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MostrarJugador(jugadorId);
                    encontrado = true;
                }
            }
            break;

        case 3:
            string equipoBuscado = LeerTexto("Digite el equipo: ");

            foreach (int jugadorId in ids)
            {
                if (equipos[jugadorId].Contains(equipoBuscado,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MostrarJugador(jugadorId);
                    encontrado = true;
                }
            }
            break;

        case 4:
            string posicionBuscada = LeerTexto("Digite la posición: ");

            foreach (int jugadorId in ids)
            {
                if (posiciones[jugadorId].Contains(posicionBuscada,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MostrarJugador(jugadorId);
                    encontrado = true;
                }
            }
            break;
    }

    if (!encontrado)
    {
        Console.WriteLine("No se encontraron jugadores con ese criterio.");
    }
}


void EliminarJugador()
{
    Console.WriteLine("\n========== ELIMINAR JUGADOR ==========");

    if (ids.Count == 0)
    {
        Console.WriteLine("No hay jugadores registrados.");
        return;
    }

    int id = LeerNumero("Digite el ID del jugador: ", 1, int.MaxValue);

    if (!nombres.ContainsKey(id))
    {
        Console.WriteLine("No existe un jugador con ese ID.");
        return;
    }

    Console.WriteLine("\nJugador que desea eliminar:");
    MostrarJugador(id);

    Console.Write("¿Confirma que desea eliminarlo? (S/N): ");
    string respuesta = Console.ReadLine() ?? "";

    if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
    {
        ids.Remove(id);

        nombres.Remove(id);
        apellidos.Remove(id);
        edades.Remove(id);
        numerosCamiseta.Remove(id);
        equipos.Remove(id);
        posiciones.Remove(id);

        Console.WriteLine("¡Jugador eliminado exitosamente!");
    }
    else
    {
        Console.WriteLine("Operación cancelada.");
    }
}

