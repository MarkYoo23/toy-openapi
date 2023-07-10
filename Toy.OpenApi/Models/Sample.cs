using Swashbuckle.AspNetCore.Annotations;

namespace Toy.OpenApi.Models;

[SwaggerSchema(Required = new[] { "샘플" })]
public class Sample
{
    [SwaggerSchema("샘플 아이디", ReadOnly = true)]
    public int Id { get; set; }
    
    [SwaggerSchema("샘플 이름", ReadOnly = true)]
    public string Name { get; set; } = string.Empty;
}
