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

                    string query = "SELECT COUNT(*) FROM Ingresso WHERE idIngresso = @IdIngresso";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdIngresso", idIngresso);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw; 
            }
        }
    }
}
