import classes from "./styles/twoFactor.module.css";
import CardTwoFactor from "./components/cardTwoFactor/cardTwoFactor.tsx";

/**
 * Страница двухфакторной аутенификации
 */
function TwoFactor() {
  return (
    <>
      <div className={classes.container}>
        <div>
          <CardTwoFactor />
        </div>
      </div>
    </>
  );
}

export default TwoFactor;
