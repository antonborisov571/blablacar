import classes from "./styles/forgotPassword.module.css";
import ForgotPasswordComponent from "./components/forgotPassword.tsx";

/**
 * Страница забыли пароль
 */
function ForgotPassword() {
  return (
    <div className={classes.container}>
      <ForgotPasswordComponent></ForgotPasswordComponent>
    </div>
  );
}

export default ForgotPassword;
