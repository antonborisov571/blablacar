import FindTrip from "./components/findTrip/findTrip.tsx";
import SafetyMain from "./components/safetyMain/safetyMain.tsx";
import PublishTrip from "./components/publishTrip/publishTrip.tsx";

/**
 * Компонент главной страницы
 */
function Landing() {
  return(
    <div>
      <FindTrip></FindTrip>
      <SafetyMain></SafetyMain>
      <PublishTrip></PublishTrip>
    </div>
  );
}

export default Landing;