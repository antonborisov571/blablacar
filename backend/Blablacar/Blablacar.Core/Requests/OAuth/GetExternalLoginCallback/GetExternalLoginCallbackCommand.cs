using Blablacar.Contracts.Requests.OAuth.GetExternalLoginCallback;
using MediatR;

namespace Blablacar.Core.Requests.OAuth.GetExternalLoginCallback;

/// <summary>
/// Команда для авторизации через сторонние сервисы
/// </summary>
public class GetExternalLoginCallbackCommand : IRequest<GetExternalLoginCallbackResponse>;