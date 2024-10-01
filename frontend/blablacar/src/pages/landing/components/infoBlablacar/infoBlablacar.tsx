import classes from "./styles/infoBlablacar.module.css";
import PriceIcon from "../../../../assets/mainPage/price.svg";
import TrustIcon from "../../../../assets/mainPage/trust.svg";
import EnergyIcon from "../../../../assets/mainPage/energy.svg";

/**
 * Информация о блаблакаре на главной странице
 */
function InfoBlablacar() {

  return (
    <div className={classes.infoWrapperWrapper}>
      <div className={classes.infoWrapper}>
        <div>
          <img className={classes.infoImage} src={PriceIcon}/>
          <h4 className={classes.infoTitle}>Ваша поездка по низкой цене</h4>
          <p className={classes.infoText}>
            Куда бы вы ни собирались,
            на автобусе или с попутчиками,
            вы сможете найти свою идеальную
            поездку среди множества маршрутов
            и доехать по самой низкой цене.
          </p>
        </div>
        <div>
          <img className={classes.infoImage} src={TrustIcon}/>
          <h4 className={classes.infoTitle}>Доверяйте своим попутчикам</h4>
          <p className={classes.infoText}>
            Мы стараемся узнать ваших
            будущих попутчиков и автобусных
            перевозчиков как можно лучше.
            Мы проверяем отзывы, профили
            и паспортные данные попутчиков,
            чтобы вы знали, с кем поедете.
          </p>
        </div>
        <div>
          <img className={classes.infoImage} src={EnergyIcon}/>
          <h4 className={classes.infoTitle}>В дорогу за пару кликов!</h4>
          <p className={classes.infoText}>
            Забронировать поездку проще простого.
            В нашем приложении легко разобраться:
            мощный алгоритм за пару минут найдет
            водителя поблизости, и вам останется
            нажать пару кнопок для брони.
          </p>
        </div>
      </div>
    </div>
  );
}

export default InfoBlablacar;