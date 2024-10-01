import { api } from "../config/axios";
import { Profile } from "../hooks/profile/profile";

/**Сервис для задания профиля
 * @class
 */
class ProfileService {
  /**
   * Сеттер для профиля
   * @param profile - профиль
   */
  static setProfile: ((profile: Profile | null) => void) | undefined =
    undefined;
    /**
     * Сеттер для загрузки
     * @param loading - загрузка
     */
  static setLoading: ((loading: boolean) => void) | undefined = undefined;

  /**
   * Статический инициализатор
   * @param setProfile смотрите {@link ProfileService#setProfile}
   * @param setLoading смотрите {@link ProfileService#setLoading}
   */
  static initialize(
    setProfile: (profile: Profile | null) => void,
    setLoading: (loading: boolean) => void
  ) {
    ProfileService.setProfile = setProfile;
    ProfileService.setLoading = setLoading;
  }

  /**
   * Метод для задания профиля
   */
  static fetchProfile() {
    if (
      ProfileService.setProfile === undefined ||
      ProfileService.setLoading === undefined
    )
      return;
    ProfileService.setLoading(true);
    api
      .get("account/userinfo")
      .then((response) => {
        ProfileService.setProfile!(response.data)
        console.log(response.data)
      })
      .catch()
      .finally(() => ProfileService.setLoading!(false));
  }
}

export default ProfileService;
