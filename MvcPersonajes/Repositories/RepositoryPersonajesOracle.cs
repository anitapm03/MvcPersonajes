using MvcPersonajes.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;

namespace MvcPersonajes.Repositories
{
    public class RepositoryPersonajesOracle: IRepository
    {
        private DataTable tablaPersonajes;
        private OracleConnection cn;
        private OracleCommand com;

        public RepositoryPersonajesOracle()
        {
            string connectionString =
                @"Data Source=LOCALHOST:1521/XE;Persist Security Info=True;User Id=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            string sql = "SELECT * FROM PERSONAJES";
            OracleDataAdapter ad = new OracleDataAdapter(sql, this.cn);
            this.tablaPersonajes = new DataTable();
            ad.Fill(this.tablaPersonajes);
        }

        public void EliminarPersonaje(int idPersonaje)
        {
            string sql = "DELETE FROM PERSONAJES WHERE IDPERSONAJE=:ID";
            OracleParameter pamId = new OracleParameter(":ID", idPersonaje);
            this.com.Parameters.Add(pamId);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
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

        public void InsertarPersonaje(int id, string nombre, string imagen)
        {
            string sql = "INSERT INTO PERSONAJES VALUES(" +
                ":ID, :NOMBRE, :IMAGEN)";
            
            OracleParameter pamId = new OracleParameter(":ID", id);
            this.com.Parameters.Add(pamId);
            OracleParameter pamNombre = new OracleParameter(":NOMBRE", nombre);
            this.com.Parameters.Add(pamNombre);
            OracleParameter pamImagen = new OracleParameter(":IMAGEN", imagen);
            this.com.Parameters.Add(pamImagen);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void ModificarPersonaje(int id, string nombre, string imagen)
        {
            string sql = "UPDATE PERSONAJES SET PERSONAJE = :NOMBRE, IMAGEN = :IMAGEN" +
                " WHERE IDPERSONAJE=:ID";
            
            OracleParameter pamNombre = new OracleParameter(":NOMBRE", nombre);
            this.com.Parameters.Add(pamNombre);
            OracleParameter pamImagen = new OracleParameter(":IMAGEN", imagen);
            this.com.Parameters.Add(pamImagen);
            OracleParameter pamId = new OracleParameter(":ID", id);
            this.com.Parameters.Add(pamId);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
