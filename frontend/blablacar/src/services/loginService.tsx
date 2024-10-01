import { api } from "../config/axios";

/**
 * Класс для работы с аккаунтом
 */
class LoginService {
  /**
   * Интервалы для обновления Refresh Token-а
   */
    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
    // @ts-expect-error
  static interval: NodeJS.Timeout | null = null;

  /**
   * Сеттер для задания Refresh Token-а
   */
  static setRefreshingTokens:
    | ((refreshingTokens: boolean) => void)
    | undefined = undefined;

  static noConnection = false;

  /**
   * Метод чтобы начать обновления Refresh Token-a
   */
  static registerAutomaticRefresh() {
    LoginService.unregisterAutomaticRefresh();
    LoginService.interval = setInterval(LoginService.refreshTokens, 600000);
    // refresh tokens every 10 minutes
  }

  /**
   * Статический инициализатор
   * @param setRefreshingTokens смотрите {@link LoginService#setRefreshingTokens}
   */
  static initialize(setRefreshingTokens: (refreshingTokens: boolean) => void) {
    LoginService.setRefreshingTokens = setRefreshingTokens;
    const accessToken = localStorage.getItem("access_token");
    if (accessToken)
      api.defaults.headers.common.Authorization = `Bearer ${accessToken}`;
  }

  /**
   * Обновляем интервал
   */
  static unregisterAutomaticRefresh() {
    if (LoginService.interval == null) return;
    clearInterval(LoginService.interval);
  }

  /**
   * Метод для добавления токенов в localStorage
   * @param accessToken JWT
   * @param expiresIn время жизни токена
   * @param refreshToken Refresh Token
   */
  static storeTokens(
    accessToken: string,
    expiresIn: number,
    refreshToken: string
  ) {
    localStorage.setItem("access_token", accessToken);
    localStorage.setItem("refresh_token", refreshToken);
    let date = new Date();
    date.setSeconds(date.getSeconds() + expiresIn);
    localStorage.setItem("expires", date.toString());
    api.defaults.headers.common.Authorization = `Bearer ${accessToken}`;
  }

  /**
   * Проверка авторизованности данного пользователя
   */
  static checkLoggedIn(): boolean {
    const accessToken = localStorage.getItem("access_token");
    const refreshToken = localStorage.getItem("refresh_token");
    const expiresString = localStorage.getItem("expires");
    if (
      accessToken === null ||
      refreshToken === null ||
      expiresString === null ||
      LoginService.noConnection
    )
      return false;
    const expires = new Date(localStorage.getItem("expires")!);
    if (expires <= new Date()) {
      LoginService.refreshTokens();
      return false;
    }
    return true;
  }

  /**
   * Отчистка токенов
   */
  static clearTokens() {
    localStorage.removeItem("access_token");
    localStorage.removeItem("refresh_token");
    localStorage.removeItem("expires");
    delete api.defaults.headers.common["Authorization"];
  }

  /**
   * Обновление Refresh Token-а
   */
  static refreshTokens() {
    const accessToken = localStorage.getItem("access_token");
    const refreshToken = localStorage.getItem("refresh_token");
    if (refreshToken === null || LoginService.setRefreshingTokens === undefined)
      return;
    LoginService.setRefreshingTokens(true);
    console.log("Token refresh requested");
    api
      .post("auth/RefreshToken", {
        accessToken: accessToken,
        refreshToken: refreshToken,
      })
      .then((response) => {
        LoginService.storeTokens(
          response.data.accessToken,
          response.data.expiresIn,
          response.data.refreshToken
        );
        console.log("Tokens updated");
      })
      .catch((error) => {
        if ("response" in error && error.response.status == 401) {
          LoginService.clearTokens();
          console.log("Tokens expired");
        } else {
          LoginService.noConnection = true;
          console.log(error);
        }
      })
      .finally(() => {
        LoginService.setRefreshingTokens!(false);
      });
  }

  /**
  * Получить токен
  */
  static getAccessToken() {
    if (!LoginService.checkLoggedIn()) return null;
    return localStorage.getItem("access_token");
  }
}

export default LoginService;
