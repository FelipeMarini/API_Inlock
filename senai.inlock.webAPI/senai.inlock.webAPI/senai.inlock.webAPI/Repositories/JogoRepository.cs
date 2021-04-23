using senai.inlock.webAPI.Domains;
using senai.inlock.webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        
        private string stringConexao = "data source=DESKTOP-7SJR3UU\\SQLEXPRESS; initial catalog=Inlock_Games; user id=sa; pwd=senai@132";


        public void AtualizarIdUrlJogo(int id, JogoDomain jogoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string queryUpdateIdUrl = "UPDATE Jogos SET Jogos.NomeJogo = @nomeJogo, Jogos.Descricao = @descricao, Jogos.DataLancamento = @dataLancamento, Jogos.Valor = @valor, Jogos.IdEstudio = @idEstudio WHERE IdJogo = @id";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdUrl, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nomeJogo", jogoAtualizado.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", jogoAtualizado.descricaoJogo);
                    cmd.Parameters.AddWithValue("@dataLancamento", jogoAtualizado.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", jogoAtualizado.valorJogo);
                    cmd.Parameters.AddWithValue("@idEstudio", jogoAtualizado.idEstudio);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        public JogoDomain BuscarPorIdJogo(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySearchById = "SELECT IdJogo, NomeJogo, Descricao, DataLancamento, Valor, Jogos.IdEstudio, Estudios.IdEstudio, Estudios.NomeEstudio FROM Jogos INNER JOIN Estudios ON Jogos.IdEstudio = Estudios.IdEstudio WHERE Jogos.IdJogo = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearchById, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);

                    
                    rdr = cmd.ExecuteReader();

                    
                    if (rdr.Read())
                    {
                       
                        JogoDomain jogoBuscado = new JogoDomain
                        {
                            
                            idJogo = Convert.ToInt32(rdr["IdJogo"]),

                            nomeJogo = rdr["NomeJogo"].ToString(),

                            descricaoJogo = rdr["Descricao"].ToString(),

                            dataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),

                            valorJogo = Convert.ToDouble(rdr["Valor"]),

                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                            estudio = new EstudioDomain
                            {
                                idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                                nomeEstudio = rdr["NomeEstudio"].ToString()
                            }
                        
                        };

                        
                        return jogoBuscado;
                    
                    }
                   
                    
                    return null;
                
                }
            }
        }

        

        public JogoDomain BuscarPorNomeJogo(string nome)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySearchByName = "SELECT IdJogo, NomeJogo, Descricao, DataLancamento, Valor, Jogos.IdEstudio, Estudios.NomeEstudio FROM Jogos INNER JOIN Estudios ON Jogos.IdEstudio = Estudios.IdEstudio WHERE NomeJogo = @nomeJogo";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearchByName, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", nome);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        JogoDomain jogoBuscado = new JogoDomain
                        {
                            idJogo = Convert.ToInt32(rdr["IdJogo"]),

                            nomeJogo = rdr["NomeJogo"].ToString(),

                            descricaoJogo = rdr["Descricao"].ToString(),

                            dataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),

                            valorJogo = Convert.ToDouble(rdr["Valor"]),

                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                            estudio = new EstudioDomain
                            {
                                idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                                nomeEstudio = rdr["NomeEstudio"].ToString()
                            }
                        
                        };
                        
                        return jogoBuscado;
                    }
                    
                    return null;
                
                }
            }
        }

        

        public void CadastrarJogo(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Jogos(NomeJogo, Descricao, DataLancamento, Valor, IdEstudio) VALUES (@nomeJogo, @descricao, @dataLancamento, @valor, @idEstudio)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricaoJogo);
                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", novoJogo.valorJogo);
                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);

                    
                    con.Open();

                    
                    cmd.ExecuteNonQuery();
                }
            
            }
        }

        

        public void DeleteJogo(int id)
        {
            
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string queryDelete = "DELETE Jogos WHERE Jogos.IdJogo = @id";

                
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);

                    
                    con.Open();

                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        public List<JogoDomain> ListarJogos()
        {
            
            List<JogoDomain> listaJogos = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdJogo, NomeJogo, Descricao, DataLancamento, Valor, Jogos.IdEstudio, Estudios.NomeEstudio FROM Jogos INNER JOIN Estudios ON Jogos.IdEstudio = Estudios.IdEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand command = new SqlCommand(querySelectAll, con))
                {
                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr["IdJogo"]),

                            nomeJogo = rdr["NomeJogo"].ToString(),

                            descricaoJogo = rdr["Descricao"].ToString(),

                            dataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),

                            valorJogo = Convert.ToDouble(rdr["Valor"]),

                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                            estudio = new EstudioDomain
                            {
                                idEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                                nomeEstudio = rdr["NomeEstudio"].ToString()
                            }
                        
                        };

                        listaJogos.Add(jogo);
                    }
                }
            }
            
            return listaJogos;
        }


       

        
    }
}