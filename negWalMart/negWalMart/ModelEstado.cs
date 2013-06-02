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
    public class ModelEstado
    {
        [Required(ErrorMessage="Você deve inserir um código para o Estado")]
        [Display(Name = "Código")]
        [DataType(DataType.Text)]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Informe o nome do País")]
        [Display(Name = "País")]
        [DataType(DataType.Text, ErrorMessage = "O nome do País não pode ser um número")]
        [StringLength(50, ErrorMessage = "O tamanho máximo para o campo \"País\" é de 50 caracteres.")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "Informe o nome do Estado")]
        [Display(Name = "Estado")]
        [DataType(DataType.Text, ErrorMessage="O nome do Estado não pode ser um número" )]
        [StringLength(50, ErrorMessage = "O tamanho máximo para o campo \"Estado\" é de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a Sigla do Estado ex.: SP")]
        [Display(Name = "Sigla")]
        [DataType(DataType.Text, ErrorMessage = "A Sigla não pode ser um número")]
        [StringLength(2, ErrorMessage = "Informe apenas a Sigla do Estado.")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "Informe a Região ex.: Sudeste")]
        [Display(Name = "Região")]
        [DataType(DataType.Text, ErrorMessage = "A Região não pode ser um número")]
        [StringLength(50, ErrorMessage = "O tamanho máximo para o campo \"Região\" é de 50 caracteres.")]
        public string Regiao { get; set; }

        //metodo para listar todos os estados cadastrados
        public List<ModelEstado> Lista()
        {
            //criamos a query para selecionar todo os estados
            string sql = "SELECT Codigo,Pais,Nome,Sigla,Regiao FROM Estado";
            //carregamos a lista dos estados do banco
            SqlDataReader dr = DAO.getDR(sql);
            //variável de retorno
            List<ModelEstado> lst = new List<ModelEstado>();
            //percorre o datareader para preencher a lista de Estados
            while (dr.Read())
            {
                //adiciona um item a lista
                lst.Add(new ModelEstado
                {
                    Codigo = (int)dr["Codigo"],
                    Nome = dr["Nome"].ToString(),
                    Pais = dr["Pais"].ToString(),
                    Regiao = dr["Regiao"].ToString(),
                    Sigla = dr["Sigla"].ToString()
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
            //query para exluir o estado
            string sql = "DELETE FROM estado WHERE codigo = @codigo;";
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
        public ModelEstado seleciona()
        {
            ModelEstado est = null;
            //criamos a query para selecionar todo os estados
            string sql = "SELECT Codigo,Pais,Nome,Sigla,Regiao FROM Estado WHERE Codigo = @codigo";
            //carregamos a lista dos estados do banco
            //cria o comando
            SqlCommand cm = new SqlCommand(sql);
            //adiciona o parâmetro
            cm.Parameters.AddWithValue("@codigo", this.Codigo);
            //executa o parametro
            SqlDataReader dr = DAO.getDR(cm);
            //percorre o datareader para preencher a lista de Estados (neste caso apenas 1)
            while (dr.Read())
            {
                //adiciona um item a lista
                est = new ModelEstado
                {
                    Codigo = (int)dr["Codigo"],
                    Nome = dr["Nome"].ToString(),
                    Pais = dr["Pais"].ToString(),
                    Regiao = dr["Regiao"].ToString(),
                    Sigla = dr["Sigla"].ToString()
                };
            }
            //fecha o datareader
            dr.Close();
            return est;
        }

        public int adiciona()
        {
            int ret = -1;
            //query para adicionar o estado
            string sql = "INSERT INTO estado (Codigo,Pais,Nome,Sigla,Regiao) VALUES(@codigo, @pais, @nome, @sigla, @regiao);";
            //cria o comando
            SqlCommand cm = new SqlCommand(sql);
            //adiciona os parâmetros
            cm.Parameters.AddWithValue("@codigo", this.Codigo);
            cm.Parameters.AddWithValue("@pais", this.Pais);
            cm.Parameters.AddWithValue("@nome", this.Nome);
            cm.Parameters.AddWithValue("@sigla", this.Sigla);
            cm.Parameters.AddWithValue("@regiao", this.Regiao);
            //executa o parametro
            ret = DAO.exQuery(cm);
            //retorna o número de registros afetados
            return ret;
        }

        public int edita()
        {
            int ret = -1;
            //query para editar o estado
            string sql = "UPDATE estado SET Pais=@pais, Nome=@nome, Sigla=@sigla, Regiao=@regiao WHERE Codigo=@codigo;";
            //cria o comando
            SqlCommand cm = new SqlCommand(sql);
            //adiciona os parâmetros
            cm.Parameters.AddWithValue("@codigo", this.Codigo);
            cm.Parameters.AddWithValue("@pais", this.Pais);
            cm.Parameters.AddWithValue("@nome", this.Nome);
            cm.Parameters.AddWithValue("@sigla", this.Sigla);
            cm.Parameters.AddWithValue("@regiao", this.Regiao);
            //executa o parametro
            ret = DAO.exQuery(cm);
            //retorna o número de registros afetados
            return ret;
        }


    }
}