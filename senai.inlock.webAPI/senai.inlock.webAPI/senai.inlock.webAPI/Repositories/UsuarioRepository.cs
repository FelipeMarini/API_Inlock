using senai.inlock.webAPI.Domains;
using senai.inlock.webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        
        private string stringConexao = "data source=DESKTOP-7SJR3UU\\SQLEXPRESS; initial catalog=Inlock_Games; user id=sa; pwd=senai@132";

        
        public void AtualizarIdUrlUsuario(int id, UsuarioDomain usuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
               
                string queryUpdateIdUrl = "UPDATE Usuarios SET Email = @email, Senha = @senha, IdTipoUsuario = @idTipoUsuario WHERE IdUsuario = @id";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdUrl, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@email", usuarioAtualizado.email);
                    cmd.Parameters.AddWithValue("@senha", usuarioAtualizado.senha);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", usuarioAtualizado.idTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain BuscarPorEmailSenhaUsuario(string email, string senha)
        {
            
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryLogin = "SELECT IdUsuario, Email, Senha, Usuarios.IdTipoUsuario, TiposUsuario.Titulo FROM Usuarios INNER JOIN TiposUsuario ON Usuarios.IdTipoUsuario = TiposUsuario.IdTipoUsuario WHERE Usuarios.Email = @email AND Senha = @senha";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryLogin, con))
                {
                    
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    
                    rdr = cmd.ExecuteReader();

                    
                    if (rdr.Read())
                    {
                        
                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                   
                            idUsuario = Convert.ToInt32(rdr["IdUsuario"]),

                            email = rdr["Email"].ToString(),

                            senha = rdr["Senha"].ToString(),

                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                            tipoUsuario = new TipoUsuarioDomain
                            {
                                idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                                tituloTipoUsuario = rdr["Titulo"].ToString()
                            }
                        };

                        
                        return usuarioBuscado;
                    
                    }
                    
                    
                    return null;
                }
            }
        }

        

        public UsuarioDomain BuscarPorIdUsuario(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySearchById = "SELECT IdUsuario, Email, Senha, Usuarios.IdTipoUsuario, TiposUsuario.Titulo FROM Usuarios INNER JOIN TiposUsuario ON Usuarios.IdTipoUsuario = TiposUsuario.IdTipoUsuario WHERE Usuarios.IdUsuario = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearchById, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);

                    
                    rdr = cmd.ExecuteReader();

                    
                    if (rdr.Read())
                    {
                        
                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                            
                            idUsuario = Convert.ToInt32(rdr["IdUsuario"]),

                            email = rdr["Email"].ToString(),

                            senha = rdr["Senha"].ToString(),

                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                            tipoUsuario = new TipoUsuarioDomain
                            {
                                idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                                tituloTipoUsuario = rdr["Titulo"].ToString()
                            }
                        };

                        
                        return usuarioBuscado;
                    
                    }
                    
                    
                    return null;
                
                }
            }
        }


        public void CadastrarUsuario(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Usuarios(Email, Senha, IdTipoUsuario) VALUES (@email, @senha, @idTipoUsuario)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    
                    cmd.Parameters.AddWithValue("@email", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@senha", novoUsuario.senha);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.idTipoUsuario);

                    
                    con.Open();

                    
                    cmd.ExecuteNonQuery();
                
                }
            }
        }

        
        public void DeleteUsuario(int id)
        {
            
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string queryDelete = "DELETE Usuarios WHERE Usuarios.IdUsuario = @id";

                
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);

                    
                    con.Open();

                    
                    cmd.ExecuteNonQuery();
                }
            
            }
        
        }

        
        public List<UsuarioDomain> ListarUsuarios()
        {
            List<UsuarioDomain> listaUsuario = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdUsuario, Email, Senha, Usuarios.IdTipoUsuario, TiposUsuario.Titulo FROM Usuarios INNER JOIN TiposUsuario ON Usuarios.IdTipoUsuario = TiposUsuario.IdTipoUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr["IdUsuario"]),

                            email = rdr["Email"].ToString(),

                            senha = rdr["Senha"].ToString(),

                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                            tipoUsuario = new TipoUsuarioDomain
                            {
                                idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                                tituloTipoUsuario = rdr["Titulo"].ToString()
                            }
                        };

                        listaUsuario.Add(usuario);
                    
                    }
                }
            }
            
            return listaUsuario;
        }

        

        
    }
}