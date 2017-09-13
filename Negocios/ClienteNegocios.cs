using AcessoBancoDados;
using ObjetoTransferencia;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class ClienteNegocios
    {
        //Instanciar - Criar um novo objeto baseado em um modelo
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();

        public string Inserir(Cliente cliente)
        {
            try
            { 
                acessoDadosSqlServer.LimparParametros();

                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimiteCompra", cliente.LimiteCompra);

                string idCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();

                return idCliente;
            }catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string Atualizar(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente", cliente.IdCliente);
                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimiteCompra", cliente.LimiteCompra);

                string idCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteAlterar").ToString();

                return idCliente;

            }catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public string Excluir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente", cliente.IdCliente);
                string IdCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();

                return IdCliente;
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }

       public ClienteCollection ConsultarPorNome(string nome)
       {
            try
            {
                ClienteCollection clienteColecao = new ClienteCollection();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome", nome);
                DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                //Percorrer o DataTable e transformar em coleção de cliente
                //Cada linha do DataTable é um cliente
                //Data=dados row=linha
                foreach(DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(row["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(row["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }


                return clienteColecao;
            }catch(Exception ex)
            {
                throw new Exception("Não foi possível consultar o cliente por nome.Detalhes:" + ex.Message);
            }
       }
        
       public ClienteCollection ConsultarPorId(int id)
       {
            try
            {
                ClienteCollection clienteColecao = new ClienteCollection();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@idCliente", id);
                DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorID");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(row["idCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(row["dataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(row["sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(row["limiteCompra"]);

                    clienteColecao.Add(cliente);
                }

                return clienteColecao;

            }catch (Exception ex)
            {
                throw new Exception("Não foi possível consultar o cliente por código.Detalhes:" + ex.Message);
            }
       }  
    }
}
