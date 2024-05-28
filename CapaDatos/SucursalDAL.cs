using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using System.Collections;
using System.Globalization;

namespace CapaDatos
{
    public class SucursalDAL : Conexion
    {
        public List<Sucursal> Listar()
        {
            List<Sucursal> lista = null;
            //  string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString; 
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //Abro la conexion
                    cn.Open();
                    //Llame al procedure
                    using (SqlCommand cmd = new SqlCommand("SP_ListarSucursales", cn))
                    {
                        //Buena practica (Opcional)->Indicamos que es un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<Sucursal>();
                            Sucursal oSucursal;
                            int posCodigo = drd.GetOrdinal("codigo");
                            int posDescripcion = drd.GetOrdinal("descripcion");
                            int posDireccion = drd.GetOrdinal("direccion");
                            int posIdentificacion = drd.GetOrdinal("identificacion");
                            int posfechaCreacion = drd.GetOrdinal("fechaCreacion");
                            int posidMoneda = drd.GetOrdinal("idMoneda");
                            int posDescripcionMoneda = drd.GetOrdinal("Moneda");
                            while (drd.Read())
                            {
                                oSucursal = new Sucursal();
                                oSucursal.Codigo = drd.IsDBNull(posCodigo) ? 0 :
                                    drd.GetInt32(posCodigo);
                                oSucursal.descripcion = drd.IsDBNull(posDescripcion) ? ""
                                    : drd.GetString(posDescripcion);
                                oSucursal.direccion = drd.IsDBNull(posDireccion) ? ""
                                    : drd.GetString(posDireccion);
                                oSucursal.identificacion = drd.IsDBNull(posIdentificacion) ? ""
                                    : drd.GetString(posIdentificacion);
                                //oSucursal.fechaCreacion = drd.IsDBNull(posfechaCreacion) ? ""
                                //    : drd.GetString(posfechaCreacion);
                                oSucursal.fechaCreacion = drd.IsDBNull(posfechaCreacion)
                                ? string.Empty
                                : drd.GetDateTime(posfechaCreacion).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                                oSucursal.idMoneda = drd.IsDBNull(posidMoneda) ? 0
                                  : drd.GetInt32(posidMoneda);
                                oSucursal.moneda = drd.IsDBNull(posDescripcionMoneda) ? ""
                                    : drd.GetString(posDescripcionMoneda);
                                lista.Add(oSucursal);
                            }
                        }

                    }

                    //Cierro una vez de traer la data
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }

            }
            return lista;


        }

        public Sucursal obtenerSucursalPorCodigo(int Codigo)
        {
            Sucursal oSucursal = null;
            //  string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString; 
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //Abro la conexion
                    cn.Open();
                    //Llame al procedure
                    using (SqlCommand cmd = new SqlCommand("SP_ObtenerSucursalPorCodigo", cn))
                    {
                        //Buena practica (Opcional)->Indicamos que es un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo", Codigo);
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            int posCodigo = drd.GetOrdinal("codigo");
                            int posDescripcion = drd.GetOrdinal("descripcion");
                            int posDireccion = drd.GetOrdinal("direccion");
                            int posIdentificacion = drd.GetOrdinal("identificacion");
                            int posfechaCreacion = drd.GetOrdinal("fechaCreacion");
                            int posidMoneda = drd.GetOrdinal("idMoneda");
                            int posDescripcionMoneda = drd.GetOrdinal("Moneda");
                            while (drd.Read())
                            {
                                oSucursal = new Sucursal();
                                oSucursal.Codigo = drd.IsDBNull(posCodigo) ? 0 :
                                    drd.GetInt32(posCodigo);
                                oSucursal.descripcion = drd.IsDBNull(posDescripcion) ? ""
                                    : drd.GetString(posDescripcion);
                                oSucursal.direccion = drd.IsDBNull(posDireccion) ? ""
                                    : drd.GetString(posDireccion);
                                oSucursal.identificacion = drd.IsDBNull(posIdentificacion) ? ""
                                    : drd.GetString(posIdentificacion);
                                oSucursal.fechaCreacion = drd.IsDBNull(posfechaCreacion)
                                ? string.Empty
                                : drd.GetDateTime(posfechaCreacion).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                                //oSucursal.fechaCreacion = drd.IsDBNull(posfechaCreacion) ? ""
                                //    : drd.GetString(posfechaCreacion);
                                oSucursal.idMoneda = drd.IsDBNull(posidMoneda) ? 0
                                  : drd.GetInt32(posidMoneda);
                                oSucursal.moneda = drd.IsDBNull(posDescripcionMoneda) ? ""
                                    : drd.GetString(posDescripcionMoneda);
                            }
                        }

                    }

                    //Cierro una vez de traer la data
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }

            }
            return oSucursal;


        }

        public int eliminarPorCodigo(int Codigo)
        {
            int rpta = 0;
            //  string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString; 
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //Abro la conexion
                    cn.Open();
                    //Llame al procedure
                    using (SqlCommand cmd = new SqlCommand("SP_EliminarSucursal", cn))
                    {
                        //Buena practica (Opcional)->Indicamos que es un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo", Codigo);
                        rpta = cmd.ExecuteNonQuery();
                        cn.Close();
                    }

                    //Cierro una vez de traer la data

                }
                catch (Exception ex)
                {
                    cn.Close();
                }

            }
            return rpta;


        }


        public int Editar(Sucursal sucursal)
        {
            int rpta = 0;
            //  string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString; 
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //Abro la conexion
                    cn.Open();
                    //Llame al procedure
                    using (SqlCommand cmd = new SqlCommand("SP_EditarSucursal", cn))
                    {
                        //Buena practica (Opcional)->Indicamos que es un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo", sucursal.Codigo);
                        cmd.Parameters.AddWithValue("@descripcion", sucursal.descripcion);
                        cmd.Parameters.AddWithValue("@direccion", sucursal.direccion);
                        cmd.Parameters.AddWithValue("@identificacion", sucursal.identificacion);
                        cmd.Parameters.Add(new SqlParameter("@fechaCreacion", SqlDbType.DateTime));
                        cmd.Parameters["@fechaCreacion"].Value = sucursal.fechaCreacion;
                        cmd.Parameters.AddWithValue("@idMoneda", sucursal.idMoneda);
                        rpta = cmd.ExecuteNonQuery();
                    }

                    //Cierro una vez de traer la data
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }

            }
            return rpta;
        }

        public int guardar(Sucursal sucursal)
        {
            int rpta = 0;
            //  string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString; 
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //Abro la conexion
                    cn.Open();
                    //Llame al procedure
                    using (SqlCommand cmd = new SqlCommand("SP_GuardarSucursal", cn))
                    {
                        //Buena practica (Opcional)->Indicamos que es un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@codigo", sucursal.Codigo);
                        cmd.Parameters.AddWithValue("@descripcion", sucursal.descripcion);
                        cmd.Parameters.AddWithValue("@direccion", sucursal.direccion);
                        cmd.Parameters.AddWithValue("@identificacion", sucursal.identificacion);
                        //cmd.Parameters.AddWithValue("@fechaCreacion", sucursal.fechaCreacion);
                        cmd.Parameters.Add(new SqlParameter("@fechaCreacion", SqlDbType.DateTime));
                        cmd.Parameters["@fechaCreacion"].Value = sucursal.fechaCreacion;
                        cmd.Parameters.AddWithValue("@idMoneda", sucursal.idMoneda);
                        rpta = cmd.ExecuteNonQuery();
                    }

                    //Cierro una vez de traer la data
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }

            }
            return rpta;


        }


        
    }
}
