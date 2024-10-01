/**
 * Тип для регистрации пользователя
 */
export type RegisterForm = {
  /**
   * Имя пользователя
   */
  firstName?: string;

  /**
   * Фамилия пользователя
   */
  lastName?: string;

  /**
   * Почта пользователя
   */
  email?: string;

  /**
   * Дата рождения пользователя
   */
  birthday?: Date;

  /**
   * Пароль
   */
  password?: string;

  /**
   * Повторить пароль
   */
  passwordConfirm?: string;
};

/**
 * Тип ошибки для формы регистрации
 */
export type ErrorType = {
  /**
   * Ошибка с именем
   */
  firstName?: string,

  /**
   * Ошибка с фамилией
   */
  lastName?: string,

  /**
   * Ошибка с почтой
   */
  email?: string,

  /**
   * Ошибка с паролем
   */
  password?: string,

  /**
   * Ошибка с подтверждением пароля
   */
  passwordConfirm?: string,
}