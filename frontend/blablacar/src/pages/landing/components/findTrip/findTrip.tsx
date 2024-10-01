import classes from "./styles/findTrip.module.css"
import SearchBlablacar from "../../../../components/general/search/searchBlablacar.tsx";
import InfoBlablacar from "../infoBlablacar/infoBlablacar.tsx";
import AutobusIcon from "../../../../assets/mainPage/autobus.svg";

/**
 * Компонент верхней части страницы
 */
function FindTrip() {

  return (
    <div>
      <div className={classes.imageSearchWrapper}>
        <div className={classes.shadow}></div>
        <div className={classes.imageWrapper}>
          <img className={classes.image}
               src={AutobusIcon}
          />
        </div>
        <div className={classes.titleSearchWrapper}>
          <h1 style={{fontSize:"56px", color:"white", fontWeight:"500", fontFamily: ""}}>
            Поездки на ваш выбор по самым низким ценам
          </h1>
          <SearchBlablacar
            navigateSearchPage={true}
          ></SearchBlablacar>
        </div>
      </div>
      <InfoBlablacar></InfoBlablacar>
    </div>
  );
}

export default FindTrip;