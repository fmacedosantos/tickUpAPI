using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Usuario
    {
        private string emailUser, cpfUser, nomeUser, telefoneUser, senhaUser;
        private int idadeUser;

        public string EmailUser { get => emailUser; set => emailUser = value; }
        public string CpfUser { get => cpfUser; set => cpfUser = value; }
        public string NomeUser { get => nomeUser; set => nomeUser = value; }
        public string TelefoneUser { get => telefoneUser; set => telefoneUser = value; }
        public string SenhaUser { get => senhaUser; set => senhaUser = value; }
        public int IdadeUser { get => idadeUser; set => idadeUser = value; }


        public Usuario(string emailUser, string cpfUser, string nomeUser, string telefoneUser,
                       string senhaUser, int idadeUser)
        {
            this.emailUser = emailUser;
            this.cpfUser = cpfUser;
            this.nomeUser = nomeUser;
            this.telefoneUser = telefoneUser;
            this.senhaUser = senhaUser;
            this.idadeUser = idadeUser;
        }

        public static Usuario Login(string emailUser, string senhaUser)
        {
            MySqlConnection con = FabricaConexao.getConexao("conexaoPadrao");
            try
            {
                con.Open();

                string query = "SELECT * FROM usuarios WHERE email = @email AND senha = @senha";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@email", emailUser);
                    cmd.Parameters.AddWithValue("@senha", senhaUser);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario usuario = new Usuario(
                                reader.GetString("email"),
                                reader.GetString("cpf"),
                                reader.GetString("nome"), 
                                reader.GetString("telefone"),
                                reader.GetString("senha"),
                                reader.GetInt32("idade")
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar login: {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return null; 
        }

        public string InserirUsuario()
        {
            MySqlConnection con = FabricaConexao.getConexao("ConexaoPadrao");
            try
            {
                con.Open();
                MySqlCommand qry = new MySqlCommand( 
                    "INSERT INTO usuarios (email, cpf, nome, telefone, senha, idade) VALUES (@email, @cpf, @nome, @telefone, @senha, @idade)", con);
                qry.Parameters.AddWithValue("@email", emailUser);
                qry.Parameters.AddWithValue("@cpf", cpfUser);
                qry.Parameters.AddWithValue("@nome", nomeUser);
                qry.Parameters.AddWithValue("@telefone", telefoneUser);
                qry.Parameters.AddWithValue("@senha", senhaUser);
                qry.Parameters.AddWithValue("@idade", idadeUser);

                qry.ExecuteNonQuery();

                return "Usuário inserido com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao inserir usuário: {ex.Message}";
            }
            finally
            {
                con.Close();
            }
        }

    }
}
