using MerchantAPI.Extensions.Mappers;
using MerchantAPI.Filters;
using MerchantAPI.Services;
using MerchantAPI.V1.Models.RequestModels;
using MerchantAPI.V1.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MerchantAPI.V1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

public class MerchantController : ControllerBase
{
    private readonly IService _service;
    private readonly ILogger<MerchantController> _logger;

    public MerchantController(IService service, ILogger<MerchantController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    /// <summary>
    /// Returns one particular merchant in the system
    /// </summary>
    /// <param name="id"></param>
    /// <response code="404">Merchant couldn't be found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MerchantResponseModel),200)]
    public async Task<IActionResult> GetOne(string id)
    {
        var merchant = await _service.GetMerchant(id);
        var response = new MerchantResponseModel(merchant);
        return Ok(response);
    }
    
    /// <summary>
    /// Returns all merchants with filters
    /// </summary>
    /// <response code="404">Merchant couldn't be found</response>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FilterParams filterParams)
    {
        var merchants = await _service.GetAllMerchants(filterParams);
        return Ok(merchants);
    }
    
    /// <summary>
    /// Creates a merchant in the system
    /// </summary>
    /// <response code="201">Creates a merchant in the system</response>
    /// <response code="400">Unable to create merchant due to validation error</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MerchantCreateRequestModel request) 
    {
        var merchant = await MerchantMappers.Create(request);
        var newMerchant = await _service.CreateMerchant(merchant);
        return Created(HttpContext.Request.Path, newMerchant);
    }
    
    /// <summary>
    /// Updates merchant in the system
    /// </summary>
    /// <response code="200">Updates merchant in the system</response>
    /// <response code="400">Unable to update merchant due to validation error</response>
    /// <response code="404">Merchant couldn't be found</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] MerchantUpdateRequestModel request)
    {
        var currentMerchant = await _service.GetMerchant(id);
        var updatedMerchant = await MerchantMappers.Update(currentMerchant, request);
        await _service.UpdateMerchant(id, updatedMerchant);
        return Ok(HttpContext.Request.Path);
    }

    /// <summary>
    /// Updates address of merchant in the system
    /// </summary>
    /// <response code="200">Updates address of merchant in the system</response>
    /// <response code="400">Unable to create merchant due to validation error</response>
    /// <response code="404">Merchant couldn't be found</response>
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAddress([FromRoute] string id, 
        [FromBody] MerchantAddressUpdateRequestModel request)
    {
        await _service.UpdateAddressOfMerchant(id, request);
        return Ok(HttpContext.Request.Path);
    }

    /// <summary>
    /// Deletes one particular merchant in the system
    /// </summary>
    /// <response code="200">Deletes one particular merchant in the system</response>
    /// <response code="404">Merchant couldn't be found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteMerchant(id);
        return Ok(HttpContext.Request.Path);
    }
}