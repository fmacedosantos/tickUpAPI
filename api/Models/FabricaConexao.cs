using MySql.Data.MySqlClient;

namespace api.Models
{
    public class FabricaConexao
    {

        public static MySqlConnection getConexao(string nome)
        {
            return new MySqlConnection(
                Configuration().GetConnectionString(nome));
        }


        private static IConfigurationRoot Configuration()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder().SetBasePath(
                    Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);
            return builder.Build();
        }
    }
}