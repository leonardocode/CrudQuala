using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

namespace CapaDatos
{
    public class MonedaDAL : Conexion
    {
        public List<Moneda> Listar()
        {
            List<Moneda> lista = null;
            //  string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString; 
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //Abro la conexion
                    cn.Open();
                    //Llame al procedure
                    using (SqlCommand cmd = new SqlCommand("SP_ListarMonedas", cn))
                    {
                        //Buena practica (Opcional)->Indicamos que es un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<Moneda>();
                            Moneda oMoneda;
                            int posId = drd.GetOrdinal("id");
                            int posNombre = drd.GetOrdinal("moneda");
                            while (drd.Read())
                            {
                                oMoneda = new Moneda();
                                oMoneda.Id = drd.IsDBNull(posId) ? 0 :
                                    drd.GetInt32(posId);
                                oMoneda.descripcionMoneda = drd.IsDBNull(posNombre) ? ""
                                    : drd.GetString(posNombre);
                                lista.Add(oMoneda);
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
    }
}
