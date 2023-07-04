using System.Net;
using Microsoft.FeatureManagement.FeatureFilters;

namespace FeatureManagement;

public class IpTargetingContextAccessor : ITargetingContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string TargetingContextLookup = "IpTargetingContextAccessor.TargetingContext";

    public IpTargetingContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public ValueTask<TargetingContext> GetContextAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var ipAddress = httpContext.Connection.RemoteIpAddress;

        List<string> groups = new List<string>();
        
        if (IPAddress.IsLoopback(ipAddress))
        {
            groups.Add("localhost");
        }
        else
        {
            groups.Add("internet");
        }
        
        TargetingContext targetingContext = new TargetingContext
        {
            Groups = groups
        };
        
        httpContext.Items[TargetingContextLookup] = targetingContext;
        
        return new ValueTask<TargetingContext>(targetingContext);
    }
}