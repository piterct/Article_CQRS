using Article.Shared.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Article.Infra.DataContext
{
    public class ArticleDataContext : IDisposable
    {

        private readonly IOptions<ArticleSettings> _config;
        public SqlConnection Connection { get; set; }
        public ArticleDataContext(IOptions<ArticleSettings> config)
        {
            _config = config;
            Connection = new SqlConnection(_config.Value.ConnectionString);
            Connection.Open();
        }
        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
