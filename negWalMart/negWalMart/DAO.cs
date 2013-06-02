using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace negWalMart.Classes
{
    public static class DAO
    {
        //variável de conexão
        private static SqlConnection cn = null;

        public static void verificaDAO()
        {
            //verifica se a conexão já não existe
            if (cn == null)
            {
                //cria uma nova conexão com o banco de dados
                cn = new SqlConnection("Data Source=localhost\\sqlexpress;Initial Catalog=TesteWallMart;Persist Security Info=True;User ID=sa;Password=1234");
            }
        }

        public static SqlDataReader getDR(string sql = "")
        {
            SqlDataReader dr = null;
            //verifica se a query está vazia
            if (sql != string.Empty)
            {
                //arumar isso
                verificaDAO();
                //verifica se a conexão foi aberta e se não foi, abriremos
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                //cria um SQLCommand
                SqlCommand cm = new SqlCommand(sql, cn);
                //define o tipo de comando (Procedure/Text
                cm.CommandType = CommandType.Text;
                //faz a carga dos dados
                dr = cm.ExecuteReader();
                //fecha
                cm = null;
            }
            //retorna
            return dr;
        }

        public static SqlDataReader getDR(SqlCommand cm = null)
        {
            SqlDataReader dr = null;

            //verifica se a query está vazia
            if (cm != null)
            {
                //arumar isso
                verificaDAO();
                //verifica se a conexão foi aberta e se não foi, abriremos
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                //define o tipo de comando (Procedure/Text
                cm.CommandType = CommandType.Text;
                //define o timeout para zero
                cm.CommandTimeout = 0;
                //definimos a conexão
                cm.Connection = cn;
                //faz a carga dos dados
                dr = cm.ExecuteReader();
                //fecha
                cm = null;
            }
            //retorna
            return dr;
        }

        public static int exQuery(SqlCommand cm)
        {
            //variável que retorna quantos registros foram afetados
            int registrosAfetados = -1;
            //arumar isso
            verificaDAO();
            //verifica se a conexão foi aberta e se não foi, abriremos
            if (cn.State != ConnectionState.Open)
                cn.Open();
            //inicia uma transação
            SqlTransaction t = cn.BeginTransaction();
            //definimos a conexão
            cm.Connection = cn;
            try
            {
                //associa a transação
                cm.Transaction = t;
                //executa a query
                registrosAfetados = cm.ExecuteNonQuery();
                //comita os dados se não houve erro
                t.Commit();
            }
            catch
            {
                registrosAfetados = -99;
                //desfaz as operações no banco caso tenha ocorrido um erro
                t.Rollback();
            }
            return registrosAfetados;
        }
    }
}