using MvcPersonajes.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcPersonajes.Repositories
{
    public class RepositoryPersonajes: IRepository
    {
        private DataTable tablaPersonajes;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryPersonajes()
        {
            string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.tablaPersonajes = new DataTable();
            string sql = "SELECT * FROM PERSONAJES";
            SqlDataAdapter ad = new SqlDataAdapter(sql, this.cn);
            ad.Fill(this.tablaPersonajes);
        }

        public void EliminarPersonaje(int idPersonaje)
        {
            string sql = "DELETE FROM PERSONAJES WHERE IDPERSONAJE=@ID";
            this.com.Parameters.AddWithValue("@ID", idPersonaje);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }

        public List<Personaje> GetPersonajes()
        {
            var consulta = from datos in this.tablaPersonajes.AsEnumerable()
                           select datos;
            List<Personaje> personajes = new List<Personaje>();

            foreach (var row in consulta)
            {
                Personaje personaje = new Personaje
                {
                    IdPersonaje = row.Field<int>("IDPERSONAJE"),
                    Nombre = row.Field<string>("PERSONAJE"),
                    Imagen = row.Field<string>("IMAGEN")
                };
                personajes.Add(personaje);
            }
            return personajes;
        }

        public Personaje FindPersonaje(int idPersonaje)
        {
            var consulta = from datos in this.tablaPersonajes.AsEnumerable()
                           where datos.Field<int>("IDPERSONAJE") == idPersonaje
                           select datos;
            var row = consulta.First();
            Personaje personaje = new Personaje
            {
                IdPersonaje = row.Field<int>("IDPERSONAJE"),
                Nombre = row.Field<string>("PERSONAJE"),
                Imagen = row.Field<string>("IMAGEN")
            };
            return personaje;
        }

        public void InsertarPersonaje(int id, string nombre, string imagen)
        {
            string sql = "INSERT INTO PERSONAJES VALUES(" +
                "@ID, @NOMBRE, @IMAGEN)";
            this.com.Parameters.AddWithValue("@ID", id);
            this.com.Parameters.AddWithValue("@NOMBRE", nombre);
            this.com.Parameters.AddWithValue("@IMAGEN", imagen);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void ModificarPersonaje(int id, string nombre, string imagen)
        {
            string sql = "UPDATE PERSONAJES SET PERSONAJE = @NOMBRE, IMAGEN = @IMAGEN" +
                " WHERE IDPERSONAJE=@ID";
            this.com.Parameters.AddWithValue("@NOMBRE", nombre);
            this.com.Parameters.AddWithValue("@IMAGEN", imagen);
            this.com.Parameters.AddWithValue("@ID", id);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
