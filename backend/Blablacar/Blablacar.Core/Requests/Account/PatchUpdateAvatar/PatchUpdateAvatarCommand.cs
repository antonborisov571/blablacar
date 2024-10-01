using MediatR;
using Microsoft.AspNetCore.Http;

namespace Blablacar.Core.Requests.Account.PatchUpdateAvatar;

/// <summary>
/// Команда для обновления аватара пользователя
/// </summary>
/// <param name="formFile"><see cref="IFormFile"/></param>
public class PatchUpdateAvatarCommand(IFormFile formFile) : IRequest
{
    /// <summary>
    /// Данные полученные из формы
    /// </summary>
    public IFormFile FormFile { get; set; } = formFile;
}