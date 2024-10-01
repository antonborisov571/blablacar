import React from "react";
import { Profile } from "./profile";

/**
 * Контекст для профиля
 */
export const ProfileContext = React.createContext<Profile | null>(null);
