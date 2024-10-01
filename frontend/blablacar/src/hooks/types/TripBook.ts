/**
 * Тип поездки для бронирования
 */
export type TripBook = {
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
   * Дата выезда
   */
  dateTimeTrip: Date,

  /**
   * Цена за поездку
   */
  price: number,

  /**
   * Водитель
   */
  driver: Driver,

  /**
   * Пассажиры
   */
  passengers: Array<Passenger>,

  /**
   * Является ли пользователь водителем
   */
  isDriver: boolean,

  /**
   * Является ли пользователь пассажиром
   */
  isPassenger: boolean,

  /**
   * Активная ли поездка
   */
  isActive: boolean
};

/**
 * Водитель
 */
export type Driver = {
  /**
   * Id водителя
   */
  id: string;

  /**
   * Имя водителя
   */
  driverName: string;

  /**
   * Аватар водителя
   */
  driverAvatar: string;

  /**
   * Предопчтения о музыке
   */
  preferencesMusic: number;

  /**
   * Предопчтения о животных
   */
  preferencesAnimal: number;

  /**
   * Предопчтения о курении
   */
  preferencesSmoking: number;
};

/**
 * Пассажир
 */
export type Passenger = {
  /**
   * Id пассажира
   */
  id: string;

  /**
   * Имя пассажира
   */
  firstName: string;

  /**
   * Аватар пассажира
   */
  avatar: string;
}