using api.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace api.Services
{
    public class CompraService
    {
        private readonly string _connectionString;

        public CompraService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<string> ObterIngressosPorEmail(string email)
        {
            List<string> idsIngressos = new List<string>();

            try
            {
                using (var con = FabricaConexao.getConexao(_connectionString))
                {
                    con.Open();

                    string query = "SELECT idIngresso FROM Ingresso JOIN Compra ON Ingresso.idCompra = Compra.idCompra WHERE Compra.email = @Email";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string idIngresso = reader["idIngresso"].ToString();
                                idsIngressos.Add(idIngresso);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw; 
            }

            return idsIngressos;
        }
    }
}
