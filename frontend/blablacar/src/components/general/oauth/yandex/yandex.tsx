import classes from "./yandex.module.css";
import YandexLogo from "../../../../assets/yandex.svg";

/**
 * Компонент кнопки для входа через Яндекс
 */
function Yandex() {
  return (
    <a className={classes.authYandex} href={"http://backend:8080/api/oauth/ExternalLogin?provider=Yandex"}>
      <img style={{width:"30px"}} src={YandexLogo} />
      <div>Войти через Yandex</div>
    </a>
);
}

export default Yandex;
