import { useContext } from "react";
import { ProfileContext } from "./profileContext";
import { LoadingContext } from "./loadingContext";
import { Profile } from "./profile";

/**
 * Хук useProfile предоставляет доступ к профилю пользователя и состоянию загрузки
 * @returns Массив [Profile | null, boolean], где:
 * Profile | null: объект профиля пользователя или null, если профиль еще не загружен.
 * boolean: флаг состояния загрузки (true, если данные профиля загружаются, и false, если загрузка завершена).
 */
export const useProfile: () => [Profile | null, boolean] = () => [
  useContext(ProfileContext),
  useContext(LoadingContext),
];
