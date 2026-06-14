using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Validators;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(
            IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var product =
                await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductDto dto)
        {
            var validator = new CreateProductValidator();

            var result = await validator.ValidateAsync(dto);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            var id =
                await _productService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateProductDto dto)
        {
            await _productService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);

            return NoContent();
        }
    }
}
