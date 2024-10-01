namespace Blablacar.Contracts.Requests.Account.PatchUpdateTwoFactor;

/// <summary>
/// Запрос на обновления двухфакторной авторизации
/// </summary>
public class PatchUpdateTwoFactorRequest
{

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PatchUpdateTwoFactorRequest(PatchUpdateTwoFactorRequest request)
    {
        TwoFactorEnabled = request.TwoFactorEnabled;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PatchUpdateTwoFactorRequest()
    {
    }
    
    /// <summary>
    /// Двухфакторная авторизация включена или нет
    /// </summary>
    public bool TwoFactorEnabled { get; set; }
}