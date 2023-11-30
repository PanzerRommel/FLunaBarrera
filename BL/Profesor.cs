using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Profesor
    {
        public static ML.Result Add(ML.Profesor profesor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "ProfesorAdd";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                        cmd.Parameters.AddWithValue("@Email", profesor.Email);
                        cmd.Parameters.AddWithValue("@Usuario", profesor.Usuario);
                        cmd.Parameters.AddWithValue("@Password", profesor.Password);
                        cmd.Parameters.AddWithValue("@Telefono", profesor.Telefono);

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo agregar el registro.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Update(ML.Profesor profesor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "ProfesorUpdate";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdProfesor", profesor.IdProfesor);
                        cmd.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                        cmd.Parameters.AddWithValue("@Email", profesor.Email);
                        cmd.Parameters.AddWithValue("@Usuario", profesor.Usuario);
                        cmd.Parameters.AddWithValue("@Password", profesor.Password);
                        cmd.Parameters.AddWithValue("@Telefono", profesor.Telefono);

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo actualizar el registro. Puede ser que el ID no exista.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Delete(int IdProfesor)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "ProfesorDelete";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdProfesor", IdProfesor);

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo eliminar el registro. Puede ser que el ID no exista.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "ProfesorGetAll";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tableProfesor = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tableProfesor);
                        }

                        if (tableProfesor.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableProfesor.Rows)
                            {
                                ML.Profesor profesor = new ML.Profesor();

                                profesor.IdProfesor = Convert.ToInt32(row["IdProfesor"]);
                                profesor.Nombre = row["Nombre"].ToString();
                                profesor.Email = row["Email"].ToString();
                                profesor.Usuario = row["Usuario"].ToString();
                                profesor.Password = row["Password"].ToString();
                                profesor.Telefono = row["Telefono"].ToString();

                                result.Objects.Add(profesor);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.ToString(); // Cambiado a ex.ToString() para obtener más detalles.
            }

            return result;
        }
        public static ML.Result GetById(int IdProfesor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "ProfesorGetById";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdProfesor", IdProfesor);

                        context.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            result.Objects = new List<object>();

                            while (reader.Read())
                            {
                                ML.Profesor profesor = new ML.Profesor();

                                profesor.IdProfesor = Convert.ToInt32(reader["IdProfesor"]);
                                profesor.Nombre = reader["Nombre"].ToString();
                                profesor.Email = reader["Email"].ToString();
                                profesor.Usuario = reader["Usuario"].ToString();
                                profesor.Password = reader["Password"].ToString();
                                profesor.Telefono = reader["Telefono"].ToString();

                                result.Objects.Add(profesor);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontró el registro con el ID proporcionado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
