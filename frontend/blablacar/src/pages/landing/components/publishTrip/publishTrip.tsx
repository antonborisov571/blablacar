import classes from "./styles/publishTrip.module.css"
import Button from "../../../../components/general/button/button.tsx";

/**
 * Компонент с формой для создания новой поездки
 */
function PublishTrip() {
  return (
    <div className={classes.publishWrapperWrapper}>
      <div className={classes.publishWrapper}>
        <div className={classes.publishInfoWrapper}>
          <h2 className={classes.publishTitle}>Экономьте, когда вы за рулем</h2>
          <p>
            Зарегистрируйте профиль водителя,
            берите попутчиков и экономьте на бензине.
            Чтобы опубликовать первую поездку,
            нужно всего пару минут. Готовы ехать?
          </p>
          <Button>Опубликовать поездку</Button>
        </div>
        <img height={"344px"} width={"496px"} src="https://cdn.blablacar.com/kairos/assets/images/driver-c3bdd70e6a29c6af9ef1.svg"/>
      </div>
    </div>
  );
}

export default PublishTrip;
