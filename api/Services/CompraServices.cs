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
                JOIN compra ON ingresso.idCompra = compra.idCompra 
                JOIN evento ON ingresso.idEvento = evento.idEvento 
                WHERE compra.email = @email";

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
                throw;
            }

            return ingressos;
        }

    }
}
