
namespace tl2_tp10_2023_SofiaaCruz.Repositorio;

public interface ITableroRepository
{
    public Tablero CrearTablero(Tablero tablero);
    public void ModificarTablero(int id, Tablero tablero);
    public Tablero GetById(int id);
    public List<Tablero> GetAllTableros();
    public List<Tablero> GetAllTablerosUsuario(int id);
    public int Delete(int id);
}