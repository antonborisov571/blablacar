/**
 * Информация о пользователе
 */
export type UserInfo = {
  id: string,
  firstName: string,
  avatar: string,
  birthday: Date,
  dateRegistration: Date,
  countTrips: number,
  preferencesSmoking: number,
  preferencesMusic: number,
  preferencesAnimal: number,
  preferencesTalk: number
}