/**
 * Тип формы для нового пароля
 */
export type ResetPasswordFormData = {
  /**
   * Почта пользователя
   */
  email: string;

  /**
  * Новый пароль
  */
  newPassword: string;

  /**
   * Код для верификации, того что действительно пользователь меняет пароль, а не мошенник
   */
  verificationCodeFromUser: string;
};