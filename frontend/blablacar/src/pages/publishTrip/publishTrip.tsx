import CreateTrip from "./components/createTrip/createTrip.tsx";
import InfoCreateTrip from "./components/infoBlablacar/infoCreateTrip.tsx";

/**
 * Страницы для создания новых поездок
 */
function PublishTrip() {
  return (
    <div>
      <CreateTrip></CreateTrip>
      <InfoCreateTrip></InfoCreateTrip>
    </div>
  );
}

export default PublishTrip;