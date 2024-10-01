import RegistrationForm from "./components/registrationForm/registrationForm.tsx";
import LoginForm from "./components/loginForm/loginForm.tsx";
import classes from "./styles/register.module.css";
import SiteHeader from "./components/siteHeader/siteHeader.tsx";
import Card from "../../../components/general/card/card.tsx";

/**
 * Страница регистрации
 */
function Register() {
  return (
    <div className={classes.container}>
      <div>
        <Card>
          <SiteHeader />
          <RegistrationForm />
        </Card>
        <LoginForm />
      </div>
    </div>
  );
}

export default Register;
