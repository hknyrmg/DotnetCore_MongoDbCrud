using DotNetCore_MongoDb.Models;
using DotNetCore_MongoDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_MongoDb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PiecesController : ControllerBase
{

    private readonly ILogger<PiecesController> _logger;
    private readonly PiecesService _piecesService;
    public PiecesController(ILogger<PiecesController> logger, PiecesService piecesService) {
        _piecesService = piecesService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<List<ChessPiece>> Get() =>
        await _piecesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<ChessPiece>> Get(string id)
    {
        var chessPiece = await _piecesService.GetAsync(id);

        if (chessPiece is null)
        {
            return NotFound();
        }

        return chessPiece;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ChessPiece chessPiece)
    {
        await _piecesService.CreateAsync(chessPiece);

        return CreatedAtAction(nameof(Get), new { id = chessPiece.Id }, chessPiece);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, ChessPiece chessPiece)
    {
        var book = await _piecesService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        chessPiece.Id = book.Id;

        await _piecesService.UpdateAsync(id, chessPiece);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var chessPiece = await _piecesService.GetAsync(id);

        if (chessPiece is null)
        {
            return NotFound();
        }

        await _piecesService.RemoveAsync(id);

        return NoContent();
    }
}