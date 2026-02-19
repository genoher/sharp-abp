using Microsoft.OpenApi;
//using Microsoft.OpenApi.Any;
//using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Text.Json.Nodes;

namespace SharpAbp.Abp.Swashbuckle
{
    public class AbpSwashbuckleEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                if (schema.Enum == null)
                {
                    return;
                }

                schema.Enum.Clear();
                //// schema.Type = JsonSchemaType.String;
                //// schema.Format = null;
                foreach (var name in Enum.GetNames(context.Type))
                {
                    var jsonNodeEnum = JsonNode.Parse($"{name}");
                    if (jsonNodeEnum != null)
                    {
                        schema.Enum.Add(jsonNodeEnum);
                    }
                }
            }
        }
    }
}
