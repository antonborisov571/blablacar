import Vk from "../../../../../components/general/oauth/vk/vk.tsx";
import classes from "./styles/siteHeader.module.css";
import SignInLogo from "../../../../../assets/signIn.svg";
import Yandex from "../../../../../components/general/oauth/yandex/yandex.tsx";

/**
 * Компонент со всякой информацией о платформе Blablacar
 */
function SiteHeader() {
  return (
    <div>
      <div className={classes.siteHeader}>
        <img className={classes.loginImg} style={{margin: "-100px", height: "300px"}} src={SignInLogo}/>
        <div className={classes.textInfo}>
          Зарегистрируйтесь и получите доступ <br/>к множеству поездкам
        </div>
        <Vk></Vk>
        <Yandex></Yandex>
      </div>
      <div className={classes.or}>
        <div className={classes.line}></div>
        <div style={{ color: "#494949" }}>или</div>
        <div className={classes.line}></div>
      </div>
    </div>
  );
}

export default SiteHeader;
