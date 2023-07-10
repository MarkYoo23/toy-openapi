using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Toy.OpenApi.Dtos;
using Toy.OpenApi.Models;

namespace Toy.OpenApi.Controllers;

[ApiExplorerSettings(GroupName = "v1")]
// OpenAPI : API가 요청 및 응답에서 처리하는 content-types를 식별
[Produces("application/json")]
[Consumes("application/json")]
[SwaggerTag("Create, read, update and delete sample")]
// .Net Core 라우트 설정
[ApiController]
[Route("[controller]s")]
// <snippet_ClassDeclaration>
public class SampleController : ControllerBase
{
    private readonly ILogger<SampleController> _logger;
    private readonly List<Sample> _samples;

    public SampleController(
        ILogger<SampleController> logger)
    {
        _logger = logger;
        _samples = new List<Sample> { new() { Id = 1, Name = "First sample" } };
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    // Swagger Desc
    [SwaggerOperation(
        Summary = "Creates a new product",
        Description = "...",
        OperationId = "CreateProduct",
        Tags = new[] { "Sample" }
    )]
    [SwaggerResponse(200, "The product was created", typeof(Sample))]
    public IActionResult Get(
        [FromQuery] SampleQueryDto requestQuery)
    {
        var query = _samples.ToImmutableArray();

        if (requestQuery.Name != null)
        {
            query = query.Where(sample => sample.Name.Contains(requestQuery.Name)).ToImmutableArray();
        }

        var dtos = query.ToArray();
        return Ok(dtos);
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    // Swagger Desc
    [SwaggerOperation(
        Summary = "Creates a new sample",
        Description = "...",
        OperationId = "CreateProduct",
        Tags = new[] { "Sample" }
    )]
    [SwaggerResponse(200, "The product was created", typeof(Sample))]
    public IActionResult Put(
        [FromQuery, SwaggerRequestBody("The sample payload", Required = true)]
        SampleCreateDto requestBody)
    {
        var sample = new Sample()
        {
            Id = 1,
            Name = requestBody.Name,
        };

        _samples.Add(sample);

        return Created(new Uri($"{Request.Path}/{sample.Id}"), sample);
    }
}