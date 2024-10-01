import classes from "./styles/infoCreateTrip.module.css";
import PriceIcon from "../../../../assets/createTrip/price.svg";
import TrustIcon from "../../../../assets/createTrip/trust.svg";
import EnergyIcon from "../../../../assets/createTrip/energy.svg";

/**
 * Информация о блаблакаре на странице создания поездки
 */
function InfoCreateTrip() {

  return (
    <div className={classes.infoWrapperWrapper}>
      <h2 className={classes.infoWrapperTitle}>Преимущества совместных поездок с BlaBlaCar</h2>
      <div className={classes.infoWrapper}>
        <div>
          <img className={classes.infoImage} src={PriceIcon}/>
          <h4 className={classes.infoTitle}>Экономия на бензине</h4>
          <p className={classes.infoText}>
            Берите в дорогу попутчиков,
            которым с вами по пути, и
            экономьте каждый раз, когда
            садитесь за руль. Зарегистрируйтесь
            как водитель, чтобы сократить
            дорожные расходы.
          </p>
        </div>
        <div>
          <img className={classes.infoImage} src={TrustIcon}/>
          <h4 className={classes.infoTitle}>Сообщество надежных попутчиков</h4>
          <p className={classes.infoText}>
            Мы знаем, кто наши водители и пассажиры.
            Мы проверяем отзывы, профили
            и документы, чтобы вы точно
            знали, с кем поедете.
          </p>
        </div>
        <div>
          <img className={classes.infoImage} style={{borderRadius:"50%"}} src={EnergyIcon}/>
          <h4 className={classes.infoTitle}>Простота</h4>
          <p className={classes.infoText}>
            BlaBlaCar устроен таким образом,
            что вы легко сможете найти пассажиров,
            договориться о месте встречи
            и забрать их по пути.
          </p>
        </div>
      </div>
    </div>
  );
}

export default InfoCreateTrip;