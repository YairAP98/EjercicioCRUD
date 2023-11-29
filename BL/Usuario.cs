using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                
                using (SqlConnection connection = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string query = "SELECT IdUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, RFC FROM Usuario";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUsuario = new DataTable();

                    adapter.Fill(tablaUsuario);


                    if (tablaUsuario.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in tablaUsuario.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.Nombre = row[1].ToString();
                            usuario.ApellidoPaterno = row[2].ToString();
                            usuario.ApellidoMaterno = row[3].ToString();
                            usuario.RFC = row[4].ToString();


                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string query = "SELECT IdUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, RFC from Usuario where   IdUsuario= @IdUsuario";

                    SqlCommand cmd = new SqlCommand(query, context);
                    SqlParameter[] collection = new SqlParameter[1];

                    collection[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    collection[0].Value = usuario.IdUsuario;
                    cmd.Parameters.AddRange(collection);


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaUsuario = new DataTable();
                    adapter.Fill(tablaUsuario);

                    //Lo siguiente es pasarle el parametro

                    if (tablaUsuario.Rows.Count > 0)
                    {
                        DataRow row = tablaUsuario.Rows[0];


                        usuario.IdUsuario = int.Parse(row[0].ToString());
                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();
                        usuario.ApellidoMaterno = row[3].ToString();
                        usuario.RFC = row[4].ToString();


                        result.Object = usuario;
                        result.Correct = true;




                    }
                    else { result.Correct = false; }
                }
            }

            catch (Exception)
            {
                result.Correct = false;
                result.Message = "No existe un registro con el IdUsuario ingresado ";
                throw;
            }

            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
               
                using (SqlConnection connection = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    //Creamos un string que contengan las instrucciones que deseamos que se ejecuten
                    string query = "INSERT INTO Usuario VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @RFC)";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    SqlParameter[] collection = new SqlParameter[4];

                    collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    collection[0].Value = usuario.Nombre;
                    collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    collection[1].Value = usuario.ApellidoPaterno;
                    collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoMaterno;
                    collection[3] = new SqlParameter("@RFC", SqlDbType.VarChar);
                    collection[3].Value = usuario.RFC;
                   

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filasAfectaddas = cmd.ExecuteNonQuery();
                    if (filasAfectaddas > 0)
                    {
                        result.Correct = true;
                        result.Message = "Datos agregados de manera exitosa.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al agregar el usuario.";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string queryDelete = "DELETE FROM Usuario WHERE [IdUsuario] = @IdUsuario";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = queryDelete;

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    collection[0].Value = usuario.IdUsuario;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar el registro.";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;

            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string queryUpdate = "UPDATE Usuario SET [Nombre] = @Nombre, [ApellidoPaterno] = @ApellidoPaterno ,[ApellidoMaterno] = @ApellidoMaterno, [RFC] = @RFC, WHERE [IdUsuario] = @IdUsuario";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = queryUpdate;

                    SqlParameter[] collection = new SqlParameter[5];
                    collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    collection[0].Value = usuario.Nombre;
                    collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    collection[1].Value = usuario.ApellidoPaterno;
                    collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoMaterno;
                    collection[3] = new SqlParameter("@RFC", SqlDbType.VarChar);
                    collection[3].Value = usuario.RFC;
                    collection[4] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    collection[4].Value = usuario.IdUsuario;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                        result.Message = "Acualización correcta.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al actualizar.";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;

            }
            return result;
        }
    }
}
