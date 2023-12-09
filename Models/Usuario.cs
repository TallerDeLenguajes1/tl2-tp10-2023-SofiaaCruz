using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_SofiaaCruz.ViewModels;
using tl2_tp10_2023_SofiaaCruz.Models;

public enum Roles{administrador, operador}
public class Usuario 
{
    private int id;
    private string nombreUsuario;
    private Roles rol;
    private string password;
    public int Id { get => id; set => id = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public Roles Rol { get => rol; set => rol = value; }
    public string Password { get => password; set => password = value; }
}