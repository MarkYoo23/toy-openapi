using Swashbuckle.AspNetCore.Annotations;

namespace Toy.OpenApi.Dtos;

public class SampleQueryDto
{
    [SwaggerParameter("Search name keywords", Required = false)]
    public string? Name { get; set; }

    [SwaggerParameter("From 조회", Required = false)]
    public DateTime? From { get; set; }
    
    [SwaggerParameter("To 조회", Required = false)]
    public DateTime? To { get; set; }
}
