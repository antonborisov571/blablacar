import classes from "./styles/resetPassword.module.css";
import ResetPasswordComponent from "./components/resetPassword.tsx";

/**
 * Страница новый пароль
 */
function ResetPassword() {
  return (
    <div className={classes.container}>
      <ResetPasswordComponent></ResetPasswordComponent>
    </div>
  );
}

export default ResetPassword;
