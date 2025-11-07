using Microsoft.AspNetCore.Mvc;
using TalkMate.Application.DTOs;
using TalkMate.Application.Interfaces.Services;

namespace TalkMate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("send")]
    public async Task<ActionResult<ChatResponseDto>> SendMessage([FromBody] SendMessageRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _chatService.SendMessageAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpGet("history/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ChatRecordDto>>> GetHistory(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var history = await _chatService.GetChatHistoryAsync(userId, cancellationToken);
            return Ok(history);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}

