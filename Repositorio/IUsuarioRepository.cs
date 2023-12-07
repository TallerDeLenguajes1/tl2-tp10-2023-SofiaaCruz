using tl2_tp10_2023_SofiaaCruz.Models;
using tl2_tp10_2023_SofiaaCruz.ViewModels;
namespace tl2_tp10_2023_SofiaaCruz.Repositorio;

public interface IUsuarioRepository
{
    public void NuevoUsuario(Usuario usuario);
    public void ModificarUsuario(int id, Usuario usuario);
    public List<Usuario> GetAll();
    public Usuario GetById(int id);
    public int Delete(int id);
}