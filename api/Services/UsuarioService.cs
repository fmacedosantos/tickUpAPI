﻿using api.Models;
using api.Requests;
using MySql.Data.MySqlClient;

namespace api.Services
{
    public class UsuarioService
    {
        private readonly string _connectionString;

        public UsuarioService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string InserirUsuario(Usuario usuario)
        {
            using (var con = FabricaConexao.getConexao(_connectionString))
            {
                try
                {
                    con.Open();
                    var qry = new MySqlCommand(
                        "INSERT INTO usuario (cpf, email, nome, senha, telefone, idade) VALUES (@cpf, @email, @nome, @senha, @telefone, @idade)", con);
                    qry.Parameters.AddWithValue("@cpf", usuario.Cpf);
                    qry.Parameters.AddWithValue("@email", usuario.Email);
                    qry.Parameters.AddWithValue("@nome", usuario.Nome);
                    qry.Parameters.AddWithValue("@senha", usuario.Senha);
                    qry.Parameters.AddWithValue("@telefone", usuario.Telefone);
                    qry.Parameters.AddWithValue("@idade", usuario.Idade);

                    qry.ExecuteNonQuery();
                    return "Usuário inserido com sucesso!";
                }
                catch (Exception ex)
                {
                    return $"Erro ao inserir usuário: {ex.Message}";
                }
            }
        }

        public bool Login(LoginUsuarioRequest loginRequest)
        {
            using (var con = FabricaConexao.getConexao(_connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT COUNT(*) FROM usuario WHERE email = @email AND senha = @senha";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@email", loginRequest.Email);
                        cmd.Parameters.AddWithValue("@senha", loginRequest.Senha);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar login: {ex.Message}");
                    return false;
                }
            }
        }

        public bool VerificarExistenciaPorEmail(string email)
        {
            try
            {
                using (var con = FabricaConexao.getConexao(_connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM usuario WHERE email = @email";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

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

        public bool VerificarExistenciaPorCpf(string cpf)
        {
            try
            {
                using (var con = FabricaConexao.getConexao(_connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM usuario WHERE cpf = @cpf";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@cpf", cpf);

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
