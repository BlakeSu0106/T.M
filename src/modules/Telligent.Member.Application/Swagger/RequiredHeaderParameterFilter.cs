using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Telligent.Member.Application.Swagger;

public class RequiredHeaderParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Tenant",
            In = ParameterLocation.Header,
            AllowEmptyValue = false,
            Description = "tenant id"
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Company",
            In = ParameterLocation.Header,
            AllowEmptyValue = false,
            Description = "company id"
        });
    }
}