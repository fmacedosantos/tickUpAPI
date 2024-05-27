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
                        SELECT Ingresso.idIngresso, Evento.nomeEvento 
                        FROM Ingresso 
                        JOIN Compra ON Ingresso.idCompra = Compra.idCompra 
                        JOIN Evento ON Ingresso.idEvento = Evento.idEvento 
                        WHERE Compra.email = @Email";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

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
            catch (Exception)
            {
                throw;
            }

            return ingressos;
        }
    }
}
