using System.Data.SqlClient;
using System.Data.SQLite;
//using EspacioITableroRepository;
using tl2_tp10_2023_SofiaaCruz.Models;
namespace tl2_tp10_2023_SofiaaCruz.Repositorio;

public class TableroRepository : ITableroRepository
{
     private string CadenaDeConexion = "Data Source=DB/kanban.db;Cache=Shared";
    public Tablero CrearTablero(Tablero tablero)
    {
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try 
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "Insert Into Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@id, @nombre, @descripcion)";
                command.Parameters.Add(new SQLiteParameter("@id", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        return tablero;
    }

    public List<Tablero> GetAllTableros()
    {
        List<Tablero> listTablero = new List<Tablero>();
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tablero";
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var tablero = new Tablero
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString()
                        };
                        listTablero.Add(tablero);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        return listTablero;
    }

    public List<Tablero> GetAllTablerosUsuario(int id)
    {
        List<Tablero> listTablero = new List<Tablero>();
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tablero WHERE id_usuario_propietario = @id";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var tablero = new Tablero
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdUsuarioPropietario = id,
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString()
                        };
                        listTablero.Add(tablero);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        return listTablero;
    }

    public Tablero GetById(int id)
    {
        var tablero = new Tablero();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tablero WHERE id =@id";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        return tablero;
    }

    public void ModificarTablero(int id, Tablero tablero)
    {
       using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
       {
        try
        {
            connection.Open();
            using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Tablero SET nombre = @nombre, descripcion = @descripcion WHERE id = @id";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            if(command.ExecuteNonQuery() == 0)
            {
                throw new Exception ("No se pudo realizar la modiificación");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
       }
    }

    public void Delete(int id)
    {
        int filasAfectadas = 0;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE  FROM Tablero WHERE id = @id";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                filasAfectadas = command.ExecuteNonQuery();
                if(filasAfectadas == 0)
                {
                    throw new Exception ("No se pudo eliminar el tablero");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}