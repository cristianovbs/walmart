using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using negWalMart.Classes;

namespace negWalMart.Models
{
    public class ModelCidade
    {
        [Required(ErrorMessage="Selecione o Estado.")]
        [DataObjectField(true)]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Selecione o Estado.")]
        [Display(Name = "Estado")]
        public int Estado { get; set; }

        [Required(ErrorMessage = "Informe o nome da Cidade.")]
        [Display(Name = "Cidade")]
        [DataType(DataType.Text, ErrorMessage = "O nome da Cidade não pode ser um número")]
        [StringLength(50, ErrorMessage = "O tamanho máximo para o campo \"País\" é de 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Esta cidade é uma Capital?")]
        [Display(Name = "Capital")]
        public bool Capital { get; set; }

        //metodo para listar todos as Cidades cadastradas
        public List<ModelCidade> Lista()
        {
            //criamos a query para selecionar todo as Cidades
            string sql = "SELECT Codigo,Estado,Nome,Capital FROM Cidade";
            //carregamos a lista das cidades
            SqlDataReader dr = DAO.getDR(sql);
            //variável de retorno
            List<ModelCidade> lst = new List<ModelCidade>();
            //percorre o datareader para preencher a lista de cidades
            while (dr.Read())
            {
                //adiciona um item a lista
                lst.Add(new ModelCidade
                {
                    Codigo = (int)dr["Codigo"],
                    Estado = (int)dr["Estado"],
                    Nome = dr["Nome"].ToString(),
                    Capital = (bool)dr["Capital"]
                });
            }
            //fecha o datareader
            dr.Close();
            //retorna a lista
            return lst;
        }
        //metodo para excluir um estado
        public int Excluir()
        {
            int ret = -1;
            //query para exluir a cidade
            string sql = "DELETE FROM cidade WHERE codigo = @codigo;";
            //cria o comando
            SqlCommand cm = new SqlCommand(sql);
            //adiciona o parâmetro
            cm.Parameters.AddWithValue("@codigo", this.Codigo);
            //executa o parametro
            ret = DAO.exQuery(cm);
            //retorna o número de registros afetados
            return ret;
        }

        //selecione o estado
        public ModelCidade seleciona()
        {
            ModelCidade cid = null;
            //criamos a query para selecionar a cidade
            string sql = "SELECT Codigo,Estado,Nome,Capital FROM Cidade WHERE Codigo = @codigo";
            //cria o comando
            SqlCommand cm = new SqlCommand(sql);
            //adiciona o parâmetro
            cm.Parameters.AddWithValue("@codigo", this.Codigo);
            //executa o parametro
            SqlDataReader dr = DAO.getDR(cm);
            //percorre o datareader para preencher a lista de Cidades (neste caso apenas 1)
            while (dr.Read())
            {
                //adiciona um item a lista
                cid = new ModelCidade
                {
                    Codigo = (int)dr["Codigo"],
                    Estado = (int)dr["Estado"],
                    Nome = dr["Nome"].ToString(),
                    Capital = (bool)dr["Capital"]
                };
            }
            //fecha o datareader
            dr.Close();
            return cid;
        }

        public int edita()
        {
            int ret = -1;
            //query para editar a cidade
            string sql = "UPDATE cidade SET Nome=@nome, Estado=@estado, Capital=@capital WHERE Codigo=@codigo;";
            //cria o comando
            SqlCommand cm = new SqlCommand(sql);
            //adiciona os parâmetros
            cm.Parameters.AddWithValue("@codigo", this.Codigo);
            cm.Parameters.AddWithValue("@nome", this.Nome);
            cm.Parameters.AddWithValue("@estado", this.Estado);
            cm.Parameters.AddWithValue("@capital", this.Capital);
            //executa o parametro
            ret = DAO.exQuery(cm);
            //retorna o número de registros afetados
            return ret;
        }

        public int adiciona()
        {
            int ret = -1;
            //query para adicionar a cidade
            string sql = "INSERT INTO cidade (Codigo,Nome,Estado,Capital) VALUES(@codigo, @nome, @estado, @capital);";
            //cria o comando
            SqlCommand cm = new SqlCommand(sql);
            //adiciona os parâmetros
            cm.Parameters.AddWithValue("@codigo", this.Codigo);
            cm.Parameters.AddWithValue("@nome", this.Nome);
            cm.Parameters.AddWithValue("@estado", this.Estado);
            cm.Parameters.AddWithValue("@capital", this.Capital);
            //executa o parametro
            ret = DAO.exQuery(cm);
            //retorna o número de registros afetados
            return ret;
        }


    }
}