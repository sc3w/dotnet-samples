using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureManagement.Controllers;

[ApiController]
[Route("[controller]")]
[FeatureGate(FeatureFlags.SimpleFeatureFlagController)]
public class FeatureFlagController : ControllerBase
{
    private readonly IFeatureManager _featureManager;

    public FeatureFlagController(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    [HttpGet("Simple")]
    public async Task<IActionResult> Get()
    {
        if (!await _featureManager.IsEnabledAsync(FeatureFlags.SimpleFeatureFlag))
        {
            return NotFound();
        }

        return Ok("Feature is enabled.");
    }

    [HttpGet("SimpleMethod")]
    [FeatureGate(FeatureFlags.SimpleFeatureFlagMethod)]
    public IActionResult GetMethod()
    {
        return Ok("Feature is enabled.");
    }
    
    [HttpGet("Percentage")]
    [FeatureGate(FeatureFlags.PercentageFeatureFlag)]
    public IActionResult GetPercentage()
    {
        return Ok("Feature is enabled.");
    }
    
    [HttpGet("TimeWindow")]
    [FeatureGate(FeatureFlags.TimeWindowFeatureFlag)]
    public IActionResult GetTimeWindow()
    {
        return Ok("Feature is enabled.");
    }
    
    [HttpGet("Targeting")]
    [FeatureGate(FeatureFlags.TargetingFeatureFlag)]
    public IActionResult GetTargetingFeatureFlag()
    {
        return Ok("Feature is enabled.");
    }
}
