using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Veiculos.Models;

namespace Veiculos
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuração e serviços de API Web
            // Configure a API Web para usar somente a autenticação de token de portador.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.EnableCors();

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<AssinaturaModel>("Assinatura");
            builder.EntitySet<PessoaModel>("Pessoa");
            builder.EntitySet<FotoModel>("Foto");
            builder.EntitySet<MultasModel>("Multas");
            builder.EntitySet<VeiculoModel>("Veiculo");
            builder.EntitySet<PessoaModel>("Pessoa");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            // Rotas de API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    }
}
