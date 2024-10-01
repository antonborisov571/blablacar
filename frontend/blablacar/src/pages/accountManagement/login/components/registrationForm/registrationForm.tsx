import { useNavigate } from "react-router-dom";
import classes from "./styles/registrationForm.module.css";
import Button from "../../../../../components/general/button/button.tsx";

/**
 * Компонента регистрации на странице входа
 */
function RegistrationForm() {
  const navigator = useNavigate();

  return (
    <div className={classes.container}>
      <div style={{ whiteSpace: "nowrap", fontSize: "12pt" }}>
        Нет аккаунта?
      </div>
      <Button style={{ width: "100%" }} onClick={() => navigator("/register")}>
        Зарегистрироваться
      </Button>
    </div>
  );
}

export default RegistrationForm;
