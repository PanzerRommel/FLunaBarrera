using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Metadata.Edm;

namespace BL
{
    public class Materia
    {
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            {
                try
                {
                    using(SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                    {
                        string query = "MateriaAdd"; //Conexion con el StoredProcedure
                        using(SqlCommand cmd = new SqlCommand(query, context))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Agregar parámetros del procedimiento almacenado
                            cmd.Parameters.AddWithValue("@Nombre", materia.Nombre);
                            cmd.Parameters.AddWithValue("@Descripcion", materia.Descripcion);

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
        }
        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "MateriaUpdate";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdMateria", materia.IdMateria);
                        cmd.Parameters.AddWithValue("@Nombre", materia.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", materia.Descripcion);

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
        public static ML.Result Delete(int IdMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "MateriaDelete";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdMateria", IdMateria);

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
                using(SqlConnection connection = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "MateriaGetAll";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tableMateria = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tableMateria);
                        }

                        if(tableMateria.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach(DataRow row in tableMateria.Rows)
                            {
                                ML.Materia materia = new ML.Materia();

                                materia.IdMateria = Convert.ToInt32(row["IdMateria"]);
                                materia.Nombre = row["Nombre"].ToString();
                                materia.Descripcion = row["Descripcion"].ToString();

                                result.Objects.Add(materia);
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
        public static ML.Result GetById(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "MateriaGetById";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdMateria", IdMateria);

                        context.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            result.Objects = new List<object>();

                            while (reader.Read())
                            {
                                ML.Materia materia = new ML.Materia();

                                materia.IdMateria = Convert.ToInt32(reader["IdMateria"]);
                                materia.Nombre = reader["Nombre"].ToString();
                                materia.Descripcion = reader["Descripcion"].ToString();

                                result.Objects.Add(materia);
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