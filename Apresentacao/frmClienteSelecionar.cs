using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Apresentacao
{
    public partial class frmClienteSelecionar : Form
    {
        public frmClienteSelecionar()
        {
            InitializeComponent();

            //Não gerar linhas automaticamente
            dgvPrincipal.AutoGenerateColumns = false;
            
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            ClienteNegocios clienteNegocios = new ClienteNegocios();

            ClienteCollection clienteColecao = new ClienteCollection();
            clienteColecao = clienteNegocios.ConsultarPorNome(txtPesquisa.Text);


            dgvPrincipal.DataSource = null;
            dgvPrincipal.DataSource = clienteColecao;

            dgvPrincipal.Update();
            dgvPrincipal.Refresh();
        }


        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado.");

                return;
            }else
            {
                if (MessageBox.Show("Deseja excluir o cliente selecionado?","Clientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Cliente clienteSelecionado = new Cliente();
                    //Coleta o cliente selecionado no DataGridView sempre será apenas um
                    clienteSelecionado = (dgvPrincipal.SelectedRows[0].DataBoundItem as Cliente);

                    ClienteNegocios clienteNegocios = new ClienteNegocios();
                    string retorno = clienteNegocios.Excluir(clienteSelecionado);

                    //Verifica se excluiu com sucesso
                    //Se o retorno for número é porque deu certo, senão é mensagem de erro
                    try
                    {
                        int idCliente = Convert.ToInt32(retorno);
                        MessageBox.Show("Cliente excluído com sucesso!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        AtualizarGrid();
                        
                    }catch
                    {
                        MessageBox.Show("Não foi possível excluir.Detalhes: " + retorno,"Erro",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            frmClienteCadastrar frm = new frmClienteCadastrar(AcaoNaTela.INSERIR,null);
            DialogResult dialog = frm.ShowDialog();
            //Atualiza o Grid quando for OK na tela de cadastro
            if (dialog == DialogResult.Yes)
            {
                AtualizarGrid();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (dgvPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado.","Cliente",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                return;
            }
            else
            {
                Cliente clienteSelecionado = new Cliente();
                //Coleta o cliente selecionado no DataGridView sempre será apenas um
                clienteSelecionado = (dgvPrincipal.SelectedRows[0].DataBoundItem as Cliente);

                frmClienteCadastrar frm = new frmClienteCadastrar(AcaoNaTela.ALTERAR,clienteSelecionado);
                DialogResult dialog = frm.ShowDialog();

                if (dialog == DialogResult.Yes)
                {
                    AtualizarGrid();
                }
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (dgvPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado.");

                return;
            }
            else
            {
                Cliente clienteSelecionado = new Cliente();
                //Coleta o cliente selecionado no DataGridView sempre será apenas um
                clienteSelecionado = (dgvPrincipal.SelectedRows[0].DataBoundItem as Cliente);

                frmClienteCadastrar frm = new frmClienteCadastrar(AcaoNaTela.CONSULTAR,clienteSelecionado);
                frm.ShowDialog();
            }
        }
    }
}
