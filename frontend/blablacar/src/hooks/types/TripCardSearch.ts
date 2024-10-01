/**
 * Картачка поездки
 */
export type TripCardSearch = {
  /**
   * Id поездки
   */
  id: number,

  /**
   * Откуда
   */
  whereFrom: string,

  /**
   * Куда
   */
  where: string,

  /**
   * Время отъезда
   */
  dateTimeTrip: Date,

  /**
   * Цена
   */
  price: number,

  /**
   * Имя водителя
   */
  driverName: string,

  /**
   * Аватар водителя
   */
  driverAvatar: string | null
};