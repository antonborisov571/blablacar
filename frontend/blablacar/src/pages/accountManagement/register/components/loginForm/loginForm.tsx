import { useNavigate } from "react-router-dom";
import classes from "./styles/loginForm.module.css";
import Button from "../../../../../components/general/button/button.tsx";

/**
 * Компонент в регистрации, для входа
 */
function LoginForm() {
  const navigator = useNavigate();

  return (
    <div className={classes.login}>
      <div style={{ whiteSpace: "nowrap", fontSize: "12pt" }}>
        Есть аккаунт?
      </div>
      <Button style={{ width: "100%" }} onClick={() => navigator("/login")}>
        Войти
      </Button>
    </div>
  );
}

export default LoginForm;
