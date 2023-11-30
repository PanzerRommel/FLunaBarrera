using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "AlumnoAdd";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                        cmd.Parameters.AddWithValue("@NumeroBoleta", alumno.NombreBoleta);
                        cmd.Parameters.AddWithValue("@UserName", alumno.UserName);
                        cmd.Parameters.AddWithValue("@Password", alumno.Password);
                        cmd.Parameters.AddWithValue("@Email", alumno.Email);
                        cmd.Parameters.AddWithValue("@Telefono", alumno.Telefono);

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
        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "AlumnoUpdate";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdAlumno", alumno.IdAlumno);
                        cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                        cmd.Parameters.AddWithValue("@NumeroBoleta", alumno.NombreBoleta);
                        cmd.Parameters.AddWithValue("@UserName", alumno.UserName);
                        cmd.Parameters.AddWithValue("@Password", alumno.Password);
                        cmd.Parameters.AddWithValue("@Email", alumno.Email);
                        cmd.Parameters.AddWithValue("@Telefono", alumno.Telefono);

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
        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "AlumnoDelete";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdAlumno", IdAlumno);

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
                    string query = "AlumnoGetAll";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tableAlumno = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tableAlumno);
                        }

                        if (tableAlumno.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableAlumno.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.IdAlumno = Convert.ToInt32(row["IdAlumno"]);
                                alumno.Nombre = row["Nombre"].ToString();
                                alumno.NombreBoleta = row["NumeroBoleta"].ToString();
                                alumno.UserName = row["UserName"].ToString();
                                alumno.Password = row["Password"].ToString();
                                alumno.Email = row["Email"].ToString();
                                alumno.Telefono = row["Telefono"].ToString();

                                result.Objects.Add(alumno);
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
                result.ErrorMessage = ex.ToString();
            }

            return result;
        }
        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "AlumnoGetById";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdAlumno", IdAlumno);

                        context.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            result.Objects = new List<object>();

                            while (reader.Read())
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.IdAlumno = Convert.ToInt32(reader["IdAlumno"]);
                                alumno.Nombre = reader["Nombre"].ToString();
                                alumno.NombreBoleta = reader["NumeroBoleta"].ToString();
                                alumno.UserName = reader["UserName"].ToString();
                                alumno.Password = reader["Password"].ToString();
                                alumno.Email = reader["Email"].ToString();
                                alumno.Telefono = reader["Telefono"].ToString();

                                result.Objects.Add(alumno);
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
