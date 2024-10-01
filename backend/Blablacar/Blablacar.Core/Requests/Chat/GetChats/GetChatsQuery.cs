using Blablacar.Contracts.Requests.Chat.GetChats;
using MediatR;

namespace Blablacar.Core.Requests.Chat.GetChats;

/// <summary>
/// Команда на получение чатов
/// </summary>
public class GetChatsQuery : IRequest<GetChatsResponse>;