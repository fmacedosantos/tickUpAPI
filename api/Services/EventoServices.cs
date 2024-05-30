using api.Models;
using MySql.Data.MySqlClient;
using System;

namespace TickUp.Services
{
    public class EventoService
    {
        private readonly string _connectionString;

        public EventoService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Login(string emailContato, string cpfCnpj)
        {
            using (var con = FabricaConexao.getConexao(_connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM evento WHERE emailContato = @emailContato AND cpf = @cpf";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@emailContato", emailContato);
                        cmd.Parameters.AddWithValue("@cpf", cpfCnpj);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar login do evento: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
