using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CRMApi.Features;

public class TemplateEndpoint : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/template/");
        ResponseCache(60);
        Version(0, 1);

        Description(endpoint => endpoint
            .Produces<string>(200)
            .Produces<BadRequest>(400)
            .Produces<NotFound>(404));
    }
    
    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var accountName = request.AccountName;

        if (accountName == "Another terrible account name")
        {
            AddError("Terrible account name found!");
        }

        ThrowIfAnyErrors();
        
        if (accountName is null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        
        var response = new Response
        {
            AccountInfo = accountName,
            Ticks = DateTime.UtcNow.Ticks
        };
        
        await SendOkAsync(response, cancellationToken);
    }
}

public class Request
{
    public string? AccountName { get; set; } 
}

public class Response
{
    public string? AccountInfo { get; set; }
    public long Ticks { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(request => request.AccountName)
            .NotEmpty()
            .NotEqual("Terrible Account Name");
    }
}