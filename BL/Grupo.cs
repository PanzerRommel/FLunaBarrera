using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BL
{
    public class Grupo
    {
        public static ML.Result Add(ML.Grupo grupo)
        {
            ML.Result result = new ML.Result();
            try
            {
                // Validación de claves foráneas
                ML.Result validMateria = Materia.GetById(grupo.IdMateria);
                ML.Result validProfesor = Profesor.GetById(grupo.IdProfesor);

                if (validMateria.Correct && validProfesor.Correct)
                {
                    using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                    {
                        context.Open();

                        // Inicia una transacción
                        using (SqlTransaction transaction = context.BeginTransaction())
                        {
                            try
                            {
                                string query = "GrupoAdd";
                                using (SqlCommand cmd = new SqlCommand(query, context, transaction))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@Nombre", grupo.Nombregrupo);
                                    cmd.Parameters.AddWithValue("@IdMateria", grupo.IdMateria);
                                    cmd.Parameters.AddWithValue("@IdProfesor", grupo.IdProfesor);

                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        // Confirma la transacción si todo ha ido bien
                                        transaction.Commit();
                                        result.Correct = true;
                                    }
                                    else
                                    {
                                        // Si no se afectaron filas, algo salió mal, realiza un rollback
                                        transaction.Rollback();
                                        result.Correct = false;
                                        result.ErrorMessage = "No se pudo agregar el registro.";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Si hay una excepción, realiza un rollback y establece el error
                                transaction.Rollback();
                                result.Correct = false;
                                result.ErrorMessage = ex.Message;
                            }
                        }
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "La Materia o el Profesor especificados no existen.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Update(ML.Grupo grupo)
        {
            ML.Result result = new ML.Result();
            try
            {
                // Validación de claves foráneas
                ML.Result validMateria = Materia.GetById(grupo.IdMateria);
                ML.Result validProfesor = Profesor.GetById(grupo.IdProfesor);

                if (validMateria.Correct && validProfesor.Correct)
                {
                    using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                    {
                        context.Open();

                        // Inicia una transacción
                        using (SqlTransaction transaction = context.BeginTransaction())
                        {
                            try
                            {
                                string query = "GrupoUpdate";
                                using (SqlCommand cmd = new SqlCommand(query, context, transaction))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@IdGrupo", grupo.IdGrupo);
                                    cmd.Parameters.AddWithValue("@Nombre", grupo.Nombregrupo);
                                    cmd.Parameters.AddWithValue("@IdMateria", grupo.IdMateria);
                                    cmd.Parameters.AddWithValue("@IdProfesor", grupo.IdProfesor);

                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        // Confirma la transacción si todo ha ido bien
                                        transaction.Commit();
                                        result.Correct = true;
                                    }
                                    else
                                    {
                                        // Si no se afectaron filas, algo salió mal, realiza un rollback
                                        transaction.Rollback();
                                        result.Correct = false;
                                        result.ErrorMessage = "No se pudo actualizar el registro. Puede ser que el ID no exista.";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Si hay una excepción, realiza un rollback y establece el error
                                transaction.Rollback();
                                result.Correct = false;
                                result.ErrorMessage = ex.Message;
                            }
                        }
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "La Materia o el Profesor especificados no existen.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdGrupo)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "GrupoDelete";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdGrupo", IdGrupo);

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
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("GrupoGetAll", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            result.Objects = new List<object>();

                            while (reader.Read())
                            {
                                ML.Grupo grupo = new ML.Grupo()
                                {
                                    IdGrupo = Convert.ToInt32(reader["IdGrupo"]),
                                    Nombregrupo = reader["NombreGrupo"].ToString(),

                                    IdMateria = Convert.ToInt32(reader["IdMateria"]),
                                    NombreMateria = reader["NombreMateria"].ToString(),

                                    IdProfesor = Convert.ToInt32(reader["IdProfesor"]),
                                    NombreProfesor = reader["NombreProfesor"].ToString()
                                };

                                result.Objects.Add(grupo);
                            }

                            result.Correct = true;
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



        public static ML.Result GetById(int IdGrupo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "GrupoGetById";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdGrupo", IdGrupo);

                        context.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            result.Objects = new List<object>();

                            while (reader.Read())
                            {
                                ML.Grupo grupo = new ML.Grupo();

                                grupo.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                                grupo.Nombregrupo = reader["NombreGrupo"].ToString();


                                grupo.IdMateria = Convert.ToInt32(reader["IdMateria"]);
                                grupo.NombreMateria = reader["NombreMateria"].ToString();


                                grupo.IdProfesor = Convert.ToInt32(reader["IdProfesor"]);
                                grupo.NombreProfesor = reader["NombreProfesor"].ToString();
                               

                                result.Objects.Add(grupo);
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
