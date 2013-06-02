using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;
using negWalMart.Models;

namespace wsWalMart.ws
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICidade" in both code and config file together.
    [ServiceContract]
    public interface ICidade
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/cidades/Add")]
        void AdicionarCidade(ModelCidade cidade);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/cidades/{id}")]
        void AtualizarCidade(ModelCidade cidade);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/cidades/{id}")]
        void ExcluirCidade(int cidade);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/cidades/{id}")]
        ModelCidade ConsultarCidade(int cidade);
    }
}
