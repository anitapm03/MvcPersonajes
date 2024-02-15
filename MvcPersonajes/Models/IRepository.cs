namespace MvcPersonajes.Models
{
    public interface IRepository
    {
        List<Personaje> GetPersonajes();

        Personaje FindPersonaje(int idPersonaje);

        void ModificarPersonaje
            (int id, string nombre, string imagen);

        void InsertarPersonaje
            (int id, string nombre, string imagen);

        void EliminarPersonaje(int idPersonaje);
    }
}
