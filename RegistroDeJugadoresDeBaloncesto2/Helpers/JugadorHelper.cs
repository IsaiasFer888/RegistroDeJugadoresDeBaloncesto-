using Microsoft.Data.SqlClient;
using RegistroDeJugadoresDeBaloncesto2.Data;
using RegistroJugadoresBaloncestoPOO.Models;

namespace RegistroJugadoresBaloncestoPOO.Helpers;

public class JugadorHelper
{
    private readonly DatabaseConnection conexion = new DatabaseConnection();

    public void AgregarJugador()
    {
        Console.WriteLine("\n=== AGREGAR JUGADOR ===");

        string nombre = LeerTexto("Nombre: ");
        string apellido = LeerTexto("Apellido: ");
        int edad = LeerEntero("Edad: ", 1, 100);
        int numero = LeerEntero("Número de camiseta: ", 0, 99);

        ListarEquipos();
        int equipoId = LeerEntero("ID del equipo: ", 1, int.MaxValue);

        ListarPosiciones();
        int posicionId = LeerEntero("ID de la posición: ", 1, int.MaxValue);

        // Creamos el objeto usando tu clase POO Jugador
        Jugador nuevoJugador = new Jugador(0, nombre, apellido, edad, numero);

        using SqlConnection conn = conexion.GetConnection();
        conn.Open();

        string consulta = @"
            INSERT INTO Jugadores
            (Nombre, Apellido, Edad, NumeroCamiseta, EquipoId, PosicionId)
            VALUES
            (@Nombre, @Apellido, @Edad, @Numero, @EquipoId, @PosicionId)";

        using SqlCommand cmd = new SqlCommand(consulta, conn);

        cmd.Parameters.AddWithValue("@Nombre", nuevoJugador.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", nuevoJugador.Apellido);
        cmd.Parameters.AddWithValue("@Edad", nuevoJugador.Edad);
        cmd.Parameters.AddWithValue("@Numero", nuevoJugador.NumeroCamiseta);
        cmd.Parameters.AddWithValue("@EquipoId", equipoId);
        cmd.Parameters.AddWithValue("@PosicionId", posicionId);

        cmd.ExecuteNonQuery();

        Console.WriteLine("\n¡Jugador registrado exitosamente!");
    }

    public void ListarJugadores()
    {
        Console.WriteLine("\n=== LISTA DE JUGADORES ===");

        using SqlConnection conn = conexion.GetConnection();
        conn.Open();

        string consulta = @"
            SELECT J.JugadorId, J.Nombre, J.Apellido, J.Edad,
                   J.NumeroCamiseta, E.Nombre AS Equipo,
                   P.Nombre AS Posicion
            FROM Jugadores J
            INNER JOIN Equipos E ON J.EquipoId = E.EquipoId
            INNER JOIN Posiciones P ON J.PosicionId = P.PosicionId
            ORDER BY J.JugadorId";

        using SqlCommand cmd = new SqlCommand(consulta, conn);
        using SqlDataReader reader = cmd.ExecuteReader();

        int total = 0;

        while (reader.Read())
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"ID: {reader["JugadorId"]}");
            Console.WriteLine($"Nombre: {reader["Nombre"]} {reader["Apellido"]}");
            Console.WriteLine($"Edad: {reader["Edad"]}");
            Console.WriteLine($"Camiseta: {reader["NumeroCamiseta"]}");
            Console.WriteLine($"Equipo: {reader["Equipo"]}");
            Console.WriteLine($"Posición: {reader["Posicion"]}");

            total++;
        }

        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Total de jugadores: {total}");
    }

    public void BuscarJugador()
    {
        Console.WriteLine("\n=== BUSCAR JUGADOR ===");
        Console.WriteLine("1. Buscar por ID");
        Console.WriteLine("2. Buscar por nombre");
        Console.WriteLine("3. Buscar por equipo");
        Console.WriteLine("4. Buscar por posición");

        int opcion = LeerEntero("Seleccione una opción: ", 1, 4);

        using SqlConnection conn = conexion.GetConnection();
        conn.Open();

        string consulta = @"
            SELECT J.JugadorId, J.Nombre, J.Apellido, J.Edad,
                   J.NumeroCamiseta, E.Nombre AS Equipo,
                   P.Nombre AS Posicion
            FROM Jugadores J
            INNER JOIN Equipos E ON J.EquipoId = E.EquipoId
            INNER JOIN Posiciones P ON J.PosicionId = P.PosicionId
            WHERE ";

        switch (opcion)
        {
            case 1:
                consulta += "J.JugadorId = @Valor";
                break;

            case 2:
                consulta += "(J.Nombre LIKE @Valor OR J.Apellido LIKE @Valor)";
                break;

            case 3:
                ListarEquipos();
                consulta += "J.EquipoId = @Valor";
                break;

            case 4:
                ListarPosiciones();
                consulta += "J.PosicionId = @Valor";
                break;
        }

        string valor;

        if (opcion == 2)
        {
            valor = "%" + LeerTexto("Digite el nombre: ") + "%";
        }
        else
        {
            valor = LeerTexto("Digite el valor de búsqueda: ");
        }

        using SqlCommand cmd = new SqlCommand(consulta, conn);
        cmd.Parameters.AddWithValue("@Valor", valor);

        using SqlDataReader reader = cmd.ExecuteReader();

        bool encontrado = false;

        while (reader.Read())
        {
            encontrado = true;

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"ID: {reader["JugadorId"]}");
            Console.WriteLine($"Nombre: {reader["Nombre"]} {reader["Apellido"]}");
            Console.WriteLine($"Edad: {reader["Edad"]}");
            Console.WriteLine($"Camiseta: {reader["NumeroCamiseta"]}");
            Console.WriteLine($"Equipo: {reader["Equipo"]}");
            Console.WriteLine($"Posición: {reader["Posicion"]}");
        }

        if (!encontrado)
            Console.WriteLine("No se encontraron jugadores.");
    }

    public void ModificarJugador()
    {
        Console.WriteLine("\n=== MODIFICAR JUGADOR ===");

        int id = LeerEntero("ID del jugador: ", 1, int.MaxValue);

        using SqlConnection conn = conexion.GetConnection();
        conn.Open();

        string verificar = "SELECT COUNT(*) FROM Jugadores WHERE JugadorId = @Id";

        using SqlCommand cmdVerificar = new SqlCommand(verificar, conn);
        cmdVerificar.Parameters.AddWithValue("@Id", id);

        if ((int)cmdVerificar.ExecuteScalar() == 0)
        {
            Console.WriteLine("Jugador no encontrado.");
            return;
        }

        string nombre = LeerTexto("Nuevo nombre: ");
        string apellido = LeerTexto("Nuevo apellido: ");
        int edad = LeerEntero("Nueva edad: ", 1, 100);
        int numero = LeerEntero("Nuevo número: ", 0, 99);

        ListarEquipos();
        int equipoId = LeerEntero("Nuevo ID del equipo: ", 1, int.MaxValue);

        ListarPosiciones();
        int posicionId = LeerEntero("Nuevo ID de posición: ", 1, int.MaxValue);

        string consulta = @"
            UPDATE Jugadores
            SET Nombre = @Nombre,
                Apellido = @Apellido,
                Edad = @Edad,
                NumeroCamiseta = @Numero,
                EquipoId = @EquipoId,
                PosicionId = @PosicionId
            WHERE JugadorId = @Id";

        using SqlCommand cmd = new SqlCommand(consulta, conn);

        cmd.Parameters.AddWithValue("@Nombre", nombre);
        cmd.Parameters.AddWithValue("@Apellido", apellido);
        cmd.Parameters.AddWithValue("@Edad", edad);
        cmd.Parameters.AddWithValue("@Numero", numero);
        cmd.Parameters.AddWithValue("@EquipoId", equipoId);
        cmd.Parameters.AddWithValue("@PosicionId", posicionId);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();

        Console.WriteLine("¡Jugador modificado exitosamente!");
    }

    public void EliminarJugador()
    {
        Console.WriteLine("\n=== ELIMINAR JUGADOR ===");

        int id = LeerEntero("ID del jugador: ", 1, int.MaxValue);

        using SqlConnection conn = conexion.GetConnection();
        conn.Open();

        string verificar = "SELECT Nombre, Apellido FROM Jugadores WHERE JugadorId = @Id";

        using SqlCommand cmdVerificar = new SqlCommand(verificar, conn);
        cmdVerificar.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = cmdVerificar.ExecuteReader();

        if (!reader.Read())
        {
            Console.WriteLine("Jugador no encontrado.");
            return;
        }

        string nombre = reader["Nombre"].ToString()!;
        string apellido = reader["Apellido"].ToString()!;

        reader.Close();

        Console.WriteLine($"Jugador: {nombre} {apellido}");
        Console.Write("¿Está seguro de eliminarlo? (S/N): ");

        string respuesta = Console.ReadLine() ?? "";

        if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            string consulta = "DELETE FROM Jugadores WHERE JugadorId = @Id";

            using SqlCommand cmd = new SqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

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

        using SqlConnection conn = conexion.GetConnection();
        conn.Open();

        string consulta = "SELECT EquipoId, Nombre, Ciudad FROM Equipos";

        using SqlCommand cmd = new SqlCommand(consulta, conn);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"ID: {reader["EquipoId"]}");
            Console.WriteLine($"Equipo: {reader["Nombre"]}");
            Console.WriteLine($"Ciudad: {reader["Ciudad"]}");
        }
    }

    public void ListarPosiciones()
    {
        Console.WriteLine("\n=== POSICIONES DISPONIBLES ===");

        using SqlConnection conn = conexion.GetConnection();
        conn.Open();

        string consulta = "SELECT PosicionId, Nombre FROM Posiciones";

        using SqlCommand cmd = new SqlCommand(consulta, conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"ID: {reader["PosicionId"]}");
            Console.WriteLine($"Posición: {reader["Nombre"]}");
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
                Console.WriteLine("El campo no puede estar vacío.");
        } while (string.IsNullOrWhiteSpace(valor));
        return valor.Trim();
    }
    private int LeerEntero(string mensaje, int minimo, int maximo)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(Console.ReadLine(), out int valor)
            && valor >= minimo && valor <= maximo)
                return valor;
            Console.WriteLine($"Digite un número entre {minimo} y {maximo}.");
        }
    }
}

