using tl2_tp10_2023_SofiaaCruz.Models;


namespace EspacioITareaRepository;

public interface ITareaRepository
{
    public Tarea CrearTarea(Tarea tarea);
    public void ModificarTarea(int id, Tarea tarea);
    public Tarea GetTareaById( int id);
    public List<Tarea> GetAllTareasUsuario(int idUsuario);
    public List<Tarea> GetAllTareasTablero(int idTablero);
    public List<Tarea> GetAll();
    public int Delete(int id);
    public int AsignarTarea(int idUsuario, int idTarea);
}