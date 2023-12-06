using System.Data.SQLite;
using EspacioITareaRepository;
using tl2_tp10_2023_SofiaaCruz.Models;


public class TareaRepository : ITareaRepository
{

    private string CadenaDeConexion = "Data Source=DB/kanban.db;Cache=Shared";
    public int AsignarTarea(int idUsuario, int idTarea)
    {
        int filasAfectadas = 0;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Tarea SET id_usuario_asignado = @idUsuario WHERE id = @idTarea";
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
                filasAfectadas = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.Write($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        return filasAfectadas;
    }

    public Tarea CrearTarea(Tarea tarea)
    {
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Tarea(id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES(@idTablero,  @nombre, @estado, @descripcion, @color, @idUsuario)";
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsuario", tarea.IdUsuarioAsignado));
                command.ExecuteNonQuery();
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
        return tarea;
    }

    public List<Tarea> GetAllTareasTablero(int idTablero)
    {
        List<Tarea> listTarea = new List<Tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try 
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tarea WHERE id_tablero = @idTablero";
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["id_tablero"]),
                            Nombre = reader["nombre"].ToString(),
                            Estado = (Estados)Convert.ToInt32(reader["estado"]),
                            Descripcion = reader["descripcion"].ToString(),
                            Color = reader["color"].ToString(),
                            IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                        };
                        listTarea.Add(tarea);
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
        return listTarea;
    }

    public List<Tarea> GetAllTareasUsuario(int idUsuario)
    {
        List<Tarea> listTarea = new List<Tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try 
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuario";
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["id_tablero"]),
                            Nombre = reader["nombre"].ToString(),
                            Estado = (Estados)Convert.ToInt32(reader["estado"]),
                            Descripcion = reader["descripcion"].ToString(),
                            Color = reader["color"].ToString(),
                            IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                        };
                        listTarea.Add(tarea);
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
        return listTarea;
    }

    public List<Tarea> GetAll()
    {
        List<Tarea> listTarea = new List<Tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try 
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tarea";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["id_tablero"]),
                            Nombre = reader["nombre"].ToString(),
                            Estado = (Estados)Convert.ToInt32(reader["estado"]),
                            Descripcion = reader["descripcion"].ToString(),
                            Color = reader["color"].ToString(),
                            IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                        };
                        listTarea.Add(tarea);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        return listTarea;
    }

    public Tarea GetTareaById(int id)
    {
        var tarea = new Tarea();
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tarea WHERE id = @id";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (Estados)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
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
        return tarea;
    }

    public void ModificarTarea(int id, Tarea tarea)
    {
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
       {
        try
        {
            connection.Open();
            using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Tarea SET nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color WHERE id = @id";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            //command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            //command.Parameters.Add(new SQLiteParameter("@idUsuario", tarea.IdUsuarioAsignado));
            command.ExecuteNonQuery();
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

    public int Delete(int id)
    {
        int filasAfectadas = 0;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Tarea WHERE id = @id";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                filasAfectadas = command.ExecuteNonQuery();
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
        return filasAfectadas;
    }
}