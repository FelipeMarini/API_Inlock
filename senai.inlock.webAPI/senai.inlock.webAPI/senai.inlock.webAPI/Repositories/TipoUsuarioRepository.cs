using senai.inlock.webAPI.Domains;
using senai.inlock.webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private string stringConexao = "data source=DESKTOP-7SJR3UU\\SQLEXPRESS; initial catalog=Inlock_Games; user id=sa; pwd=senai@132";

        public TipoUsuarioDomain BuscarPorIdTipoUsuario(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySearchById = "SELECT IdTipoUsuario, Titulo FROM TiposUsuario WHERE TiposUsuario.IdTipoUsuario = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearchById, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);

                    
                    rdr = cmd.ExecuteReader();

                    
                    if (rdr.Read())
                    {
                        
                        TipoUsuarioDomain tipoUsuarioBuscado = new TipoUsuarioDomain
                        {
                          
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                            
                            tituloTipoUsuario = rdr["Titulo"].ToString()
                        };

                       
                        return tipoUsuarioBuscado;
                    
                    }
                    
                    
                    return null;
                
                }
            }
        }

        

        public List<TipoUsuarioDomain> ListarTiposUsuarios()
        {
            
            List<TipoUsuarioDomain> listaTipoUsuario = new List<TipoUsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdTipoUsuario, Titulo FROM TiposUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TipoUsuarioDomain tipoUsuario = new TipoUsuarioDomain()
                        {
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                            tituloTipoUsuario = rdr["Titulo"].ToString()
                        };

                        listaTipoUsuario.Add(tipoUsuario);
                    }
                }
            }
            
            return listaTipoUsuario;
        }

        
    }
}