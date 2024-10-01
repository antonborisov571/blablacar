/**
 * Тип профиля
 */
export type Profile = {
  /**
   * Имя пользователя
   */
  firstName: string;

  /**
   * Фамилия пользователя
   */
  lastName: string;

  /**
   * День рождения пользователя
   */
  birthday: Date;

  /**
   * Дата регистрации пользователя
   */
  dateRegistration: Date;

  /**
   * Ссылка на аватар пользователя
   */
  avatar: string | null;

  /**
   * Почта пользователя
   */
  email: string;

  /**
   * Предпочтения о музыке
   */
  preferencesMusic: Preferences;

  /**
   * Предпочтения о курении
   */
  preferencesSmoking: Preferences;

  /**
   * Разговорчивость
   */
  preferencesTalk: Preferences;

  /**
   * Предпочтения о животных
   */
  preferencesAnimal: Preferences;

  /**
   * Включена ли двухфакторка
   */
  twoFactorEnabled: boolean;
};

/**
 *  Тип предпочтения
 */
export enum Preferences {
  Low,
  Middle,
  High,
}

/**
 * Имя + фамилия
 */
export const getProfileName = (profile: Profile) =>
  profile.firstName + " " + profile.lastName;
