using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;
using api.Models;

namespace TickUp.Models
{
    public class Evento
    {

        private string assuntoEvento, categoriaEvento, nomeEvento, emailContato, observacoes, horarioInicio, horarioTermino, cpf, email;
        private DateOnly dataInicio, dataTermino;
        private int capacidade;
        private byte[] bytesImagem;

        private string nomeLocal, cep, rua, numero, complemento, bairro, estado, cidade;

        private double valorIngresso;

        public string AssuntoEvento { get => assuntoEvento; set => assuntoEvento = value; }
        public string CategoriaEvento { get => categoriaEvento; set => categoriaEvento = value; }
        public string NomeEvento { get => nomeEvento; set => nomeEvento = value; }
        public string EmailContato { get => emailContato; set => emailContato = value; }
        public string Observacoes { get => observacoes; set => observacoes = value; }
        public DateOnly DataInicio { get => dataInicio; set => dataInicio = value; }
        public DateOnly DataTermino { get => dataTermino; set => dataTermino = value; }
        public string HorarioInicio { get => horarioInicio; set => horarioInicio = value; }
        public string HorarioTermino { get => horarioTermino; set => horarioTermino = value; }
        public int Capacidade { get => capacidade; set => capacidade = value; }
        public byte[] BytesImagem { get => bytesImagem; set => bytesImagem = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Email { get => email; set => email = value; }


        public string NomeLocal { get => nomeLocal; set => nomeLocal = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Rua { get => rua; set => rua = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Complemento { get => complemento; set => complemento = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Cidade { get => cidade; set => cidade = value; }

        public double ValorIngresso { get => valorIngresso; set => valorIngresso = value; }

    }
}