using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Framework_Lib;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace NSCC_Web_Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Campus>("Campuses");
            builder.EntitySet<AcademicYear>("AcademicYears");
            builder.EntitySet<Applicant>("Applicants");
            builder.EntitySet<Application>("Applications");
            builder.EntitySet<Citizenship>("Citizenships");
            builder.EntitySet<Country>("Countries");
            builder.EntitySet<ProgramChoice>("ProgramChoices");
            builder.EntitySet<Program>("Programs");
            builder.EntitySet<ProvinceState>("ProvinceStates");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
