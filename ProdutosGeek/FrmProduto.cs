﻿using AppBoteco.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProdutosGeek
{
    public partial class FrmProduto : Form
    {
        public FrmProduto()
        {
            InitializeComponent();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            List<Produto> produtos = produto.listaproduto();
            dgvProduto.DataSource = produtos;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text == "" && txtPreco.Text == "" && txtQuantidade.Text == "")
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Campos obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    Produto produto = new Produto();
                    if (produto.RegistroRepetido(txtNome.Text, txtTipo.Text) == true)
                    {
                        MessageBox.Show("Produto já existe em nossa base de dados!", "Repetido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNome.Text = "";
                        txtPreco.Text = "";
                        txtQuantidade.Text = "";
                        txtTipo.Text = "";
                        this.ActiveControl = txtNome;
                    }
                    else
                    {
                        int qtde = Convert.ToInt32(txtQuantidade.Text.Trim());
                        if (qtde == 0)
                        {
                            MessageBox.Show("A quantidade não pode ser igual a zero.", "Quantidade", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.ActiveControl = txtQuantidade;
                            return;
                        }
                        else
                        {
                            produto.Inserir(txtNome.Text, txtTipo.Text, qtde, txtPreco.Text);
                            MessageBox.Show("Produto inserido com sucesso!", "Inserção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNome.Text = "";
                            txtQuantidade.Text = "";
                            txtPreco.Text = "";
                            txtTipo.Text = "";
                            this.ActiveControl = txtNome;
                            List<Produto> produtos = produto.listaproduto();
                            dgvProduto.DataSource = produtos;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Inserção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

  

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(txtId.Text.Trim());
                int qtde = Convert.ToInt32(txtQuantidade.Text);
                Produto produto = new Produto();
                produto.Atualizar(Id, txtNome.Text, txtTipo.Text, qtde, txtPreco.Text);
                MessageBox.Show("Produto atualizado com sucesso!", "Edição", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Produto> produtos = produto.listaproduto();
                dgvProduto.DataSource = produtos;
                txtId.Text = "";
                txtNome.Text = "";
                txtTipo.Text = "";
                txtQuantidade.Text = "";
                txtPreco.Text = "";
                this.ActiveControl = txtNome;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(txtId.Text.Trim());
                Produto produto = new Produto();
                produto.Excluir(Id);
                MessageBox.Show("Produto excluído com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Produto> produtos = produto.listaproduto();
                dgvProduto.DataSource = produtos;
                txtId.Text = "";
                txtNome.Text = "";
                txtTipo.Text = "";
                txtQuantidade.Text = "";
                txtPreco.Text = "";
                this.ActiveControl = txtNome;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor digite o ID para localizar o produto!", "Localizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Produto produto = new Produto();
                    int Id = Convert.ToInt32(txtId.Text.Trim());
                    produto.Localizar(Id);
                    txtNome.Text = produto.nome;
                    txtTipo.Text = produto.tipo;
                    txtQuantidade.Text = Convert.ToString(produto.quantidade);
                    txtPreco.Text = Convert.ToString(produto.preco);
                    btnEditar.Enabled = true;
                    btnExcluir.Enabled = true;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Localização", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
