using TalkMate.Application.DTOs;
using TalkMate.Application.Interfaces.Repositories;
using TalkMate.Application.Interfaces.Services;
using TalkMate.Domain.Entities;

namespace TalkMate.Application.Services;

public sealed class ChatService : IChatService
{
    private const string StressKeyword = "stress";
    private const string HappyKeyword = "happy";

    private readonly IUserRepository _userRepository;
    private readonly IChatRepository _chatRepository;

    public ChatService(IUserRepository userRepository, IChatRepository chatRepository)
    {
        _userRepository = userRepository;
        _chatRepository = chatRepository;
    }

    public async Task<ChatResponseDto> SendMessageAsync(SendMessageRequest request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty)
        {
            throw new ArgumentException("User id must be provided.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.MessageText))
        {
            throw new ArgumentException("Message text must be provided.", nameof(request));
        }

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new InvalidOperationException($"User with id '{request.UserId}' was not found.");
        }

        var createdAt = DateTime.UtcNow;
        var message = new Message
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Text = request.MessageText,
            CreatedAt = createdAt
        };

        await _chatRepository.AddMessageAsync(message, cancellationToken);

        var responseText = BuildResponse(request.MessageText);
        var response = new ChatResponse
        {
            Id = Guid.NewGuid(),
            MessageId = message.Id,
            ResponseText = responseText,
            CreatedAt = DateTime.UtcNow
        };

        await _chatRepository.AddResponseAsync(response, cancellationToken);

        return new ChatResponseDto
        {
            MessageId = message.Id,
            ResponseText = response.ResponseText,
            CreatedAt = response.CreatedAt
        };
    }

    public async Task<IReadOnlyList<ChatRecordDto>> GetChatHistoryAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User id must be provided.", nameof(userId));
        }

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new InvalidOperationException($"User with id '{userId}' was not found.");
        }

        var messages = await _chatRepository.GetChatHistoryAsync(userId, cancellationToken);

        return messages
            .OrderBy(m => m.CreatedAt)
            .Select(message => new ChatRecordDto
            {
                MessageId = message.Id,
                MessageText = message.Text,
                MessageCreatedAt = message.CreatedAt,
                ResponseText = message.Response?.ResponseText,
                ResponseCreatedAt = message.Response?.CreatedAt
            })
            .ToList();
    }

    private static string BuildResponse(string messageText)
    {
        if (messageText.Contains(StressKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return "It seems you're stressed. Take a deep breath 🌿";
        }

        if (messageText.Contains(HappyKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return "That's great! Keep positive energy 😊";
        }

        return "I'm here to listen. Tell me more about how you feel.";
    }
}

