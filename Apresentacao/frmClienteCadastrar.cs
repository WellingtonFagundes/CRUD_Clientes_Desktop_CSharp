using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apresentacao
{
    public partial class frmClienteCadastrar : Form
    {
        AcaoNaTela _acaoSelecionado;

        public frmClienteCadastrar(AcaoNaTela acaoNaTela,Cliente cliente)
        {
            InitializeComponent();

            //Coletando a ação selecionada pelo usuário
            this._acaoSelecionado = acaoNaTela;

            if (acaoNaTela == AcaoNaTela.INSERIR)
            {
                this.Text = "Inserir Cliente";

            }else if (acaoNaTela == AcaoNaTela.ALTERAR)
            {
                this.Text = "Alterar Cliente";
                
                PreencherDados(cliente);
                    
            }else if (acaoNaTela == AcaoNaTela.CONSULTAR)
            {
                this.Text = "Consultar Cliente";
                
                PreencherDados(cliente);

                txtNome.ReadOnly = true;
                txtNome.TabStop = false;

                txtLimiteCompra.ReadOnly = true;
                txtLimiteCompra.TabStop = false;

                dtpDataNascimento.Enabled = false;
                dtpDataNascimento.TabStop = false;

                rbMasc.Enabled = false;
                rbFem.Enabled = false;

                btnSalvar.Visible = false;
                btnCancelar.Text = "Fechar";
                btnCancelar.Focus();
            }

        }

        /// <summary>
        /// Preenchimento dos Dados
        /// </summary>
        /// <param name="cliente"></param>
        private void PreencherDados(Cliente cliente)
        {
            txtCodigo.Text = cliente.IdCliente.ToString();
            txtNome.Text = cliente.Nome;
            txtLimiteCompra.Text = cliente.LimiteCompra.ToString();
            dtpDataNascimento.Value = cliente.DataNascimento;
            if (cliente.Sexo == true)
            {
                rbMasc.Checked = true;
            }
            else
            {
                rbFem.Checked = true;
            }
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //Verificar se é inserção ou alteração
            if (_acaoSelecionado == AcaoNaTela.INSERIR)
            {
                Cliente cliente = new Cliente();
                
                cliente.Nome = txtNome.Text;
                cliente.LimiteCompra = Convert.ToDecimal(txtLimiteCompra.Text);

                if (rbMasc.Checked == true)
                {
                    cliente.Sexo = true;
                }else
                {
                    cliente.Sexo = false;
                }
                cliente.DataNascimento = dtpDataNascimento.Value;

                ClienteNegocios negocios = new ClienteNegocios();
                string retorno = negocios.Inserir(cliente);

                //Tenta converter para inteiro
                try
                {
                    int idCliente = Convert.ToInt32(retorno);

                    MessageBox.Show("Cliente inserido com sucesso.Codigo: " + idCliente.ToString(), "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Fecha a janela e avisa a tela anterior com o Grid o estado que foi concebido
                    this.DialogResult = DialogResult.Yes;

                }catch
                {
                    MessageBox.Show("Não foi possível incluir cliente.Detalhes: " + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }

            }
            else if (_acaoSelecionado == AcaoNaTela.ALTERAR)
            {
                Cliente cliente = new Cliente();

                cliente.IdCliente = Convert.ToInt32(txtCodigo.Text);
                cliente.Nome = txtNome.Text;
                cliente.DataNascimento = dtpDataNascimento.Value;
                cliente.LimiteCompra = Convert.ToDecimal(txtLimiteCompra.Text);
                if (rbMasc.Checked)
                {
                    cliente.Sexo = true;
                }
                else
                {
                    cliente.Sexo = false;
                }

                ClienteNegocios negocios = new ClienteNegocios();
                string retorno = negocios.Atualizar(cliente);

                try
                {
                    int idCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente alterado com sucesso!!", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Suporte para a tela com o Grid
                    this.DialogResult = DialogResult.Yes;
                }catch
                {
                    MessageBox.Show("Erro ao alterar o cliente.Detalhes - " + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
