namespace CRMApi.Features;

/// <summary>
/// A template endpoint to be used as a starting point for new endpoints.
/// The two classes Request and Response are used to define the request and response models.
/// This specific endpoint takes a request with an account name and returns a response with the account name and the current time in ticks.
/// The current time in ticks is used to demonstrate the response caching feature. If the same request is made within 60 seconds, the response will be cached.
/// </summary>
public class TemplateEndpoint : Endpoint<Request, Response>
{
    /// <summary>
    /// The setup method for the endpoint.
    /// </summary>
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
    
    /// <summary>
    /// The method that handles the request, the meat of the endpoint.
    /// Validation is already done at this point, so the request is guaranteed to be valid.
    /// Custom validation can be added here if needed - the AddError method is used to add an error to the response. Any Errors will be added to a list, and will be caught by the ThrowIfAnyErrors method.
    /// Responses are sent using the SendOkAsync, SendBadRequestAsync, and SendNotFoundAsync methods.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
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

/// <summary>
/// The expected input for the endpoint.
/// </summary>
public class Request
{
    public string? AccountName { get; set; } 
}

/// <summary>
/// The output of the endpoint.
/// </summary>
public class Response
{
    public string? AccountInfo { get; set; }
    public long Ticks { get; set; }
}

/// <summary>
/// If Validation is needed for the Request, it can be added here.
/// </summary>
public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(request => request.AccountName)
            .NotEmpty()
            .NotEqual("Terrible Account Name");
    }
}