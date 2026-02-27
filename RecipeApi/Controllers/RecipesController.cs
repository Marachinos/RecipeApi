using Microsoft.AspNetCore.Mvc;
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Services;

namespace RecipeApi.Controllers;

[ApiController]
[Route("api/recipes")]

public class RecipesController : ControllerBase
{
    private readonly IRecipeService _service;
    public RecipesController(IRecipeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> GetAll()
    {
        var recipes = await _service.GetAllAsync();
        return Ok(recipes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Recipe>> GetById(int id)
    {
        var recipe = await _service.GetByIdAsync(id);

        if (recipe is null) return NotFound();

        return Ok(recipe);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<Recipe>>> Search([FromQuery] string q)
    {
        var results = await _service.SearchAsync(q);

        return Ok(results);
    }

    [HttpGet("difficulty/{level}")]
    public async Task<ActionResult<List<Recipe>>> GetByDifficulty(string level)
    {
        var results = await _service.GetByDifficultyAsync(level);
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult<Recipe>> Create([FromBody] CreateRecipeDto dto)
    {
        try

        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }

    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRecipeDto dto)
    {

        try
        {
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();

            return NoContent();
        }

        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {

        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound();

        return NoContent();
    }
}
