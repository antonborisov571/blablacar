import classes from "./styles/confirmEmail.module.css";
import ConfirmEmailComponent from "./components/confirmEmail.tsx";

/**
 * Страница подтверждения почты
 */
function ConfirmEmail() {
  return (
    <div className={classes.container}>
      <ConfirmEmailComponent></ConfirmEmailComponent>
    </div>
  );
}

export default ConfirmEmail;
