
using RegistroJugadoresBaloncestoPOO.Helpers;

Console.WriteLine("==================================");
Console.WriteLine(" REGISTRO DE JUGADORES DE BALONCESTO");
Console.WriteLine("==================================");

JugadorHelper gestor = new JugadorHelper();

bool continuar = true;

while (continuar)
{
    Console.WriteLine("\n========== MENÚ PRINCIPAL ==========");
    Console.WriteLine("1. Agregar jugador");
    Console.WriteLine("2. Buscar jugador");
    Console.WriteLine("3. Modificar jugador");
    Console.WriteLine("4. Eliminar jugador");
    Console.WriteLine("5. Listar jugadores");
    Console.WriteLine("6. Listar equipos");
    Console.WriteLine("7. Listar posiciones");
    Console.WriteLine("8. Salir");
    Console.WriteLine("====================================");
    Console.Write("Seleccione una opción: ");

    string entrada = Console.ReadLine() ?? "";

    if (!int.TryParse(entrada, out int opcion))
    {
        Console.WriteLine("Error: debe introducir un número.");
        continue;
    }

    try
    {
        switch (opcion)
        {
            case 1:
                gestor.AgregarJugador();
                break;

            case 2:
                gestor.BuscarJugador();
                break;

            case 3:
                gestor.ModificarJugador();
                break;

            case 4:
                gestor.EliminarJugador();
                break;

            case 5:
                gestor.ListarJugadores();
                break;

            case 6:
                gestor.ListarEquipos();
                break;

            case 7:
                gestor.ListarPosiciones();
                break;

            case 8:
                continuar = false;
                Console.WriteLine("Cerrando el sistema...");
                Console.WriteLine("¡Gracias por utilizar la aplicación!");
                break;

            default:
                Console.WriteLine("Opción no válida. Intente nuevamente.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocurrió un error: {ex.Message}");
    }

    if (continuar)
    {
        Console.WriteLine("\nPresione ENTER para continuar...");
        Console.ReadLine();
    }
}
