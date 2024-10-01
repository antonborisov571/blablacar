import classes from "./vk.module.css";
import VkLogo from "../../../../assets/vk.svg";

/**
 * Компонент кнопки входа через ВК
 */
function Vk() {
  return (
    <a className={classes.authVK} href={"http://backend:8080/api/oauth/ExternalLogin?provider=Vkontakte"}>
      <img src={VkLogo} />
      <div>Войти через VK</div>
    </a>
  );
}

export default Vk;
