using senai.inlock.webAPI.Domains;
using senai.inlock.webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        
        private string stringConexao = "data source=DESKTOP-7SJR3UU\\SQLEXPRESS; initial catalog=Inlock_Games; user id=sa; pwd=senai@132";

        
        public void AtualizarIdUrlEstudio(int id, EstudioDomain estudioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string queryUpdateIdUrl = "UPDATE Estudios SET NomeEstudio = @nomeEstudio WHERE IdEstudio = @id";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdUrl, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nomeEstudio", estudioAtualizado.nomeEstudio);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        
        public EstudioDomain BuscarPorIdEstudio(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySearchById = "SELECT IdEstudio, NomeEstudio FROM Estudios WHERE Estudios.IdEstudio = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearchById, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);

                    
                    rdr = cmd.ExecuteReader();

                    
                    if (rdr.Read())
                    {
                        
                        EstudioDomain estudioBuscado = new EstudioDomain
                        {
                           
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                            nomeEstudio = rdr["NomeEstudio"].ToString(),
                        };

                        return estudioBuscado;
                    }
                    
                    return null;
                
                }
            }
        }

        

        public EstudioDomain BuscarPorNomeEstudio(string nomeEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySearchByName = "SELECT IdEstudio, NomeEstudio FROM Estudios WHERE NomeEstudio = @nomeEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearchByName, con))
                {
                    cmd.Parameters.AddWithValue("@nomeEstudio", nomeEstudio);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        EstudioDomain estudioBuscado = new EstudioDomain
                        {
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                            nomeEstudio = rdr["NomeEstudio"].ToString()
                        };
                        
                        return estudioBuscado;
                    }
                    
                    return null;
                
                }
            }
        }

        

        public void CadastrarEstudio(EstudioDomain novoEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Estudios(NomeEstudio) VALUES (@nomeEstudio)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    
                    cmd.Parameters.AddWithValue("@nomeEstudio", novoEstudio.nomeEstudio);

                    
                    con.Open();

                    
                    cmd.ExecuteNonQuery();
                }
            
            }
        
        }

        
        
        public void DeleteEstudio(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string queryDelete = "DELETE Estudios WHERE Estudios.IdEstudio = @id";

                
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
               
                    cmd.Parameters.AddWithValue("@id", id);

                    
                    con.Open();

                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        public List<EstudioDomain> ListarEstudios()
        {
            List<EstudioDomain> listaEstudios = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdEstudio, NomeEstudio FROM Estudios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain estudio = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                            nomeEstudio = rdr["NomeEstudio"].ToString()
                        };

                        listaEstudios.Add(estudio);
                    }
                }
            }
            
            return listaEstudios;
        }

        
    }
}