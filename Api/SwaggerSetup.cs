using FastEndpoints.Swagger;

namespace CRMApi;

public static class SwaggerSetup
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen();
    }

    public static void AddVersioning(this IServiceCollection services)
    {
        services.SwaggerDocument(documentOptions =>
        {
            documentOptions.DocumentSettings = s =>
            {
                s.Title = "CRM Api";
                s.DocumentName = "Initial release";
                s.Version = "v0";
            };
        });
        
        services.SwaggerDocument(documentOptions =>
        {
            documentOptions.MaxEndpointVersion = 1;
            documentOptions.DocumentSettings = s =>
            {
                s.Title = "CRM Api";
                s.DocumentName = "Release 1.0";
                s.Version = "v1.0";
            };
        });
    }
}