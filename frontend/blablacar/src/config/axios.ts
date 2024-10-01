import axios from "axios";


const apiUrl = import.meta.env.VITE_API_URL;

/**
 * Axios для запросов на бэкенд сервис
 */
const api = axios.create({
  baseURL: `${apiUrl}/api/`,
});

/**
 * Axios для запросов связанных с аккаунтом
 */
const account = axios.create({
  baseURL: `${apiUrl}/api/account/`,
});

/**
 * Axios для запросов auth
 */
const auth = axios.create({
  baseURL:  `${apiUrl}/api/auth`,
});

export {api, auth, account};
