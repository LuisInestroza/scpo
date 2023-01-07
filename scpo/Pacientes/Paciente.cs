using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace scpo.Pacientes
{
    class Paciente
    {
        // Propiedades 
        public int idPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public string identidadPaciente { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int edad { get; set; }
        public string numeroTelefono { get; set; }
        public string genero { get; set; }
        public string estadoCivil { get; set; }
        public string ocupacion { get; set; }  
        public string direccion { get; set; }

        // constructor

        public Paciente() { }


        /// <summary>
        /// /Metodo agredar un nuevo paciente
        /// </summary>
        /// <param name="Paciente"></param>
        /// <returns></returns>
        public static bool Add_Paciente(Paciente Paciente)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "scpo");
            // Ejecutar el Store Procedure
            SqlCommand cmd = conn.EjecutarComando("sp_Add_Paciente");
            cmd.CommandType = CommandType.StoredProcedure;


            // Parametros 

            cmd.Parameters.Add(new SqlParameter("@nombrePaciente", SqlDbType.Text));
            cmd.Parameters["@nombrePaciente"].Value = Paciente.nombrePaciente;

            cmd.Parameters.Add(new SqlParameter("@identidadPaciente", SqlDbType.Char, 15));
            cmd.Parameters["@identidadPaciente"].Value = Paciente.identidadPaciente;

            cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", SqlDbType.Date));
            cmd.Parameters["@fechaNacimiento"].Value = Paciente.fechaNacimiento;

            cmd.Parameters.Add(new SqlParameter("@edad", SqlDbType.Int));
            cmd.Parameters["@edad"].Value = Paciente.edad;

            cmd.Parameters.Add(new SqlParameter("@numeroTelefono", SqlDbType.Char, 9));
            cmd.Parameters["@numeroTelefono"].Value = Paciente.numeroTelefono;

            cmd.Parameters.Add(new SqlParameter("@genero", SqlDbType.Text));
            cmd.Parameters["@genero"].Value = Paciente.genero;

            cmd.Parameters.Add(new SqlParameter("@estadoCivil", SqlDbType.Text));
            cmd.Parameters["@estadoCivil"].Value = Paciente.estadoCivil;

            cmd.Parameters.Add(new SqlParameter("@ocupacion", SqlDbType.Text));
            cmd.Parameters["@ocupacion"].Value = Paciente.ocupacion;

            cmd.Parameters.Add(new SqlParameter("@direccion", SqlDbType.Text));
            cmd.Parameters["@direccion"].Value = Paciente.direccion;

            try
            {
                //Conexion
                conn.EstablecerConexion();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                conn.CerrarConexion();
            }
        }

        public static List<Paciente> ListarPaciente()
        {
            List<Paciente> paciente = new List<Paciente>();

            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scpo");
            string sql;


            // Query para listar todos los pacientes
            sql = @"SELECT nombrePaciente FROM scpo.Paciente ORDER BY CONVERT(varchar,nombrePaciente)";

            // Comando
            SqlCommand cmd = conexion.EjecutarComando(sql);

            try
            {
                // Establecer la conexion
                conexion.EstablecerConexion();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Paciente listaPaciente = new Paciente();
                    listaPaciente.nombrePaciente = rdr.GetString(0);
                    

                    paciente.Add(listaPaciente);
                }

                return paciente;
            }
            catch (Exception)
            {

                return paciente;

            }
            finally
            {

                conexion.CerrarConexion();
            }
        }
        
        /// <summary>
        /// Listar datos de un paciente unico
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static Paciente List_Datos_Paciente_Identidad(string nombre)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scpo");

            string sql;
            Paciente resultado = new Paciente();

            // Query sql
            sql = @"SELECT * FROM scpo.Paciente WHERE nombrePaciente like @nombrePaciente";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            SqlDataReader rdr;

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@nombrePaciente", SqlDbType.Text).Value = nombre;
                    rdr = cmd.ExecuteReader();

                }

                while (rdr.Read())
                {

                    resultado.idPaciente = rdr.GetInt32(0);
                    resultado.nombrePaciente = rdr.GetString(1);
                    resultado.identidadPaciente = rdr.GetString(2);
                    resultado.fechaNacimiento = rdr.GetDateTime(3);
                    resultado.edad = rdr.GetInt32(4);
                    resultado.numeroTelefono = rdr.GetString(5);
                    resultado.genero = rdr.GetString(6);
                    resultado.estadoCivil = rdr.GetString(7);
                    resultado.ocupacion = rdr.GetString(8);
                    resultado.direccion = rdr.GetString(9);




                }

                return resultado;

            }
            catch (Exception)
            {

                return resultado;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        /// <summary>
        /// Metodo para actualizar los datos del paciente en la db
        /// </summary>
        /// <param name="Paciente"></param>
        /// <returns></returns>
        public static bool Update_Paciente(Paciente Paciente)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "scpo");
            // Ejecutar el Store Procedure
            SqlCommand cmd = conn.EjecutarComando("sp_Update_Paciente");
            cmd.CommandType = CommandType.StoredProcedure;


            // Parametros 
            cmd.Parameters.Add(new SqlParameter("@idPaciente", SqlDbType.Int));
            cmd.Parameters["@idPaciente"].Value = Paciente.idPaciente;

            cmd.Parameters.Add(new SqlParameter("@nombrePaciente", SqlDbType.Text));
            cmd.Parameters["@nombrePaciente"].Value = Paciente.nombrePaciente;

            cmd.Parameters.Add(new SqlParameter("@identidadPaciente", SqlDbType.Char, 15));
            cmd.Parameters["@identidadPaciente"].Value = Paciente.identidadPaciente;

            cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", SqlDbType.Date));
            cmd.Parameters["@fechaNacimiento"].Value = Paciente.fechaNacimiento;

            cmd.Parameters.Add(new SqlParameter("@edad", SqlDbType.Int));
            cmd.Parameters["@edad"].Value = Paciente.edad;

            cmd.Parameters.Add(new SqlParameter("@numeroTelefono", SqlDbType.Char, 9));
            cmd.Parameters["@numeroTelefono"].Value = Paciente.numeroTelefono;

            cmd.Parameters.Add(new SqlParameter("@genero", SqlDbType.Text));
            cmd.Parameters["@genero"].Value = Paciente.genero;

            cmd.Parameters.Add(new SqlParameter("@estadoCivil", SqlDbType.Text));
            cmd.Parameters["@estadoCivil"].Value = Paciente.estadoCivil;

            cmd.Parameters.Add(new SqlParameter("@ocupacion", SqlDbType.Text));
            cmd.Parameters["@ocupacion"].Value = Paciente.ocupacion;

            cmd.Parameters.Add(new SqlParameter("@direccion", SqlDbType.Text));
            cmd.Parameters["@direccion"].Value = Paciente.direccion;

            Paciente paciente = new Paciente();
            paciente = Paciente.List_Datos_Paciente_Identidad(paciente.identidadPaciente);

            try
            {

                if(paciente.identidadPaciente == "")
                {
                    MessageBox.Show("Paciente No Existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    //Conexion
                    conn.EstablecerConexion();
                    cmd.ExecuteNonQuery();

                    return true;
                }
                
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                conn.CerrarConexion();
            }
        }

    }

    
    
   
}
