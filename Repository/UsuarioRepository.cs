using System.Data.SQLite;
using EspacioIUsuarioRepository;
using tl2_tp10_2023_SofiaaCruz.Models; //no olvidar
using tl2_tp10_2023_SofiaaCruz.ViewModels;
public class UsuarioRepository : IUsuarioRepository
{
   private string CadenaDeConexion = "Data Source=DB/kanban.db;Cache=Shared"; //Cadena de conexión a la base de datos
    public void NuevoUsuario(Usuario usuario)
    {
        var query = "Insert Into Usuario (nombre_de_usuario,rol, password) VALUES (@nombre_de_usuario, @rol, @password)"; //Definición de la consulta SQL para insertar un nuevo usuario
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)) //La clase SQLiteConnection de utiliza para establecer y gestionar la conexión con una BD SQLite especifica
        {
            try
            {
                connection.Open(); //Apertura de la conexión a la BD
                using (var command = new SQLiteCommand(query, connection)) //SQLiteCommand se utiliza ara especificar comando SQL
                {
                    command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreUsuario));
                    command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                    command.Parameters.Add(new SQLiteParameter("@password", usuario.Password));
                    command.ExecuteNonQuery();
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

    public void ModificarUsuario(int id, Usuario usuario)
    {
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try{
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Usuario SET nombre_de_usuario = @nombre, password = @pass, rol = @rol WHERE id = @id";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreUsuario));
                command.Parameters.Add(new SQLiteParameter("@pass", usuario.Password));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                command.ExecuteNonQuery();
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
    }
    public List<Usuario> GetAll()
    {
        List<Usuario> listUsuarios = new List<Usuario>();

        string query = "SELECT * FROM Usuario";
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try{
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                NombreUsuario = reader["nombre_de_usuario"].ToString(),
                                Rol = (Roles)Convert.ToInt32(reader["rol"]),
                                Password = reader["password"].ToString()
                            };
                            listUsuarios.Add(usuario);
                        }
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
        return listUsuarios;
    }

    public Usuario GetById(int id)
    {
        var usuario = new Usuario();
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Usuario WHERE id = @idUsuario";
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        usuario.Id = id;
                        usuario.NombreUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);
                        usuario.Password = reader["password"].ToString();
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
        return usuario;
    }

    public int Delete(int id)
    {
        int filasAfectadas=0;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion))
        {
            try
            {
                connection.Open();
                using SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Usuario WHERE id = @idUsuario";
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                filasAfectadas = command.ExecuteNonQuery();//metodo que devuelve un entero que representa la cantidad de filas afectadas.
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
        return filasAfectadas;
    }
}