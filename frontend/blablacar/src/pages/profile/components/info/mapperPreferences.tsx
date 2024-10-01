import {Preferences} from "../../../../hooks/profile/profile.ts";

/**
 * Маппер для предпочтений музыки
 */
export const MapperMusic : {[index: string]:string} = {
  [Preferences.Low]: "Включайте, и погромче!",
  [Preferences.Middle]: "Все зависит от настроения — могу и спеть!",
  [Preferences.High]: "Предпочитаю тишину"
}

/**
 * Маппер для предпочтений о курении
 */
export const MapperSmoking : {[index: string]:string} = {
  [Preferences.Low]: "Я не против, если кто-то закурит",
  [Preferences.Middle]: "Можно курить, но не в машине",
  [Preferences.High]: "В моей машине не курят"
}

/**
 * Маппер для разговорчивости
 */
export const MapperTalk : {[index: string]:string} = {
  [Preferences.Low]: "Люблю поболтать!",
  [Preferences.Middle]: "Не прочь поболтать, когда мне комфортно",
  [Preferences.High]: "Я скорее тихоня"
}

/**
 * Маппер для предпочтений о животных
 */
export const MapperAnimal : {[index: string]:string} = {
  [Preferences.Low]: "Обожаю животных. Гав!",
  [Preferences.Middle]: "Зависит от животного",
  [Preferences.High]: "Предпочитаю поездки без питомцев"
}