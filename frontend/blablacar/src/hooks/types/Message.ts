/**
 * Тип сообщения
 */
export type Message = {
  /**
   * Id отправителя
   */
  senderId: string;

  /**
   * Имя отправителя
   */
  senderName: string;

  /**
   * Аватар отправителя
   */
  senderAvatar: string;

  /**
   * Текст сообщения
   */
  text: string;

  /**
   * Время отправки
   */
  dispatch: Date;
}