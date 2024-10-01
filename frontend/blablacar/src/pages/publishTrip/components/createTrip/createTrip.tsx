import classes from "./styles/createTrip.module.css"
import CreateTripMainIcon from "../../../../assets/createTrip/createTripMain.svg"
import CreateTripForm from "../../../../components/general/createTripForm/createTripForm.tsx";

/**
 * Компонент с формой для создания поездки
 */
function CreateTrip() {
  return (
    <div className={classes.createTripWrapperWrapper}>
      <div className={classes.createTripWrapper}>
        <h2 className={classes.createTripTitle}>
          Станьте водителем BlaBlaCar
          — берите попутчиков и экономьте на бензине
        </h2>
        <div className={classes.createTripFormImage}>
          <CreateTripForm></CreateTripForm>
          <img height={"285px"} width={"680px"} src={CreateTripMainIcon}/>
        </div>
      </div>
    </div>
  );
}

export default CreateTrip;