using tl2_tp10_2023_SofiaaCruz.Models;

namespace EspacioIUsuarioRepository;

public interface IUsuarioRepository
{
    public void NuevoUsuario(Usuario usuario);
    public void ModificarUsuario(int id, Usuario usuario);
    public List<Usuario> GetAll();
    public Usuario GetById(int id);
    public int Delete(int id);
}