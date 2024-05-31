using api.Models;
using MySql.Data.MySqlClient;
using System;

namespace api.Services
{
    public class IngressoService
    {
        private readonly string _connectionString;

        public IngressoService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool VerificarExistencia(string idIngresso)
        {
            try
            {
                using (var con = FabricaConexao.getConexao(_connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM ingresso WHERE idIngresso = @idIngresso";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idIngresso", idIngresso);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar o código do ingresso: {ex.Message}"); 
            }
        }

        public List<Ingresso> ObterIngressosPorEmail(string email)
        {
            List<Ingresso> ingressos = new List<Ingresso>();

            try
            {
                using (var con = FabricaConexao.getConexao(_connectionString))
                {
                    con.Open();

                    string query = @"
                SELECT ingresso.idIngresso, evento.nomeEvento
                FROM ingresso
                JOIN evento ON ingresso.idEvento = evento.idEvento
                JOIN usuario ON ingresso.email = usuario.email
                WHERE usuario.email = @email;";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ingresso = new Ingresso
                                {
                                    IdIngresso = reader["idIngresso"].ToString(),
                                    NomeEvento = reader["nomeEvento"].ToString()
                                };

                                ingressos.Add(ingresso);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter ingressos por email", ex);
            }

            return ingressos;
        }
    }
}
