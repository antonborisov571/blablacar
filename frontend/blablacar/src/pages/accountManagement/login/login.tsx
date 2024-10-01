import { ReactNode } from "react";
import classes from "./styles/login.module.css";
import SignInForm from "./components/loginForm/loginForm";
import RegistrationForm from "./components/registrationForm/registrationForm.tsx";

/**
 * Страница входа
 */
function Login(): ReactNode {
  return (
    <div className={classes.mainContainer}>
      <div className={classes.rightBlock}>
        <SignInForm />
        <RegistrationForm />
      </div>
    </div>
  );
}

export default Login;
