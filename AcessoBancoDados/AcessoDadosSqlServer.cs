using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AcessoBancoDados.Properties;

namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        //Cria a conexão
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //Parâmetros que vão para o banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;
        
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        } 

        public void AdicionarParametros(string nomeParametro,object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }


        //Persistência: Inserir, Alterar, Excluir
        public object ExecutarManipulacao(CommandType objCommandType,string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();

                //Abrir Conexão
                sqlConnection.Open();

                //Criar o comando
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = objCommandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Executa o comando, ou seja, manda o comando ir até o banco de dados
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }


        //Consultar registros no banco de dados
        public DataTable ExecutarConsulta(CommandType objCommandType,string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();

                //Abrir Conexão
                sqlConnection.Open();

                //Criar o comando
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = objCommandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Cria um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //DataTable = Tabela de dados vazia onde vou colocar os dados que vem do banco
                DataTable dt = new DataTable();

                sqlDataAdapter.Fill(dt);

                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
