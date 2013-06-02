using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using negWalMart.Models;

namespace wsWalMart.ws
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Cidade" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Cidade.svc or Cidade.svc.cs at the Solution Explorer and start debugging.
    public class Cidade : ICidade
    {  
        string arquivo = "C:\\tmp\\xmlCidade_{0}.xml";

        //método para serializar a classe em XML
        public void Serializa(ModelCidade b)
        {
            //vamos serializar a classe em arquivo
            XmlSerializer serializer = new XmlSerializer(typeof(ModelCidade));
            TextWriter tw = new StreamWriter(String.Format(arquivo, b.Codigo));
            serializer.Serialize(tw, b);
            tw.Close();

        }

        public ModelCidade Deserializa(int cidade)
        {
            //vamos deserializar o xml em objeto
            XmlSerializer serializer = new XmlSerializer(typeof(ModelCidade));
            TextReader tr = new StreamReader(String.Format(arquivo, cidade));
            ModelCidade b = (ModelCidade)serializer.Deserialize(tr);
            tr.Close();
            return b;
        }

        public void AdicionarCidade(ModelCidade cidade)
        {
            ModelCidade cid = new ModelCidade();
            cid.Codigo = cidade.Codigo;
            cid.Capital = cidade.Capital;
            cid.Estado = cidade.Estado;
            cid.Nome = cidade.Nome;
            //escreve o xml no disco
            Serializa(cid);
        }

        public void AtualizarCidade(ModelCidade cidade)
        {
            ModelCidade cid = new ModelCidade();
            cid.Codigo = cidade.Codigo;
            cid.Capital = cidade.Capital;
            cid.Estado = cidade.Estado;
            cid.Nome = cidade.Nome;
            //escreve o xml no disco
            Serializa(cid);
        }

        public void ExcluirCidade(int cidade)
        {
            //criauma instância da classe
            ModelCidade cid = new ModelCidade();
            //passa o código da cidade
            cid.Codigo = cidade;
            //se o arquivo existe, iremo excluir
            if (System.IO.File.Exists(String.Format(arquivo, cid.Codigo)))
                System.IO.File.Delete(String.Format(arquivo, cid.Codigo));
        }

        public ModelCidade ConsultarCidade(int cidade)
        {
            //cria uma instância da classe Cidade
            ModelCidade cid = null;
            //verifica se o arquivo existe e se existir iremos ler e deserializar.
            if (System.IO.File.Exists(String.Format(arquivo, cidade)))
                cid = Deserializa(cidade);
            //retorna a classe
            return cid;
        }
    }
}
