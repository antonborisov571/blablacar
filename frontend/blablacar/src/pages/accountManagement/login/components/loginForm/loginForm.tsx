import {useEffect, useState} from "react";
import {Link, useLocation, useNavigate, useSearchParams,} from "react-router-dom";

import Button from "../../../../../components/general/button/button.tsx";
import InputForm from "../../../../../components/general/inputform/inputform.tsx";

import classes from "./styles/loginForm.module.css";
import {auth} from "../../../../../config/axios.ts";
import Vk from "../../../../../components/general/oauth/vk/vk.tsx";
import LoginService from "../../../../../services/loginService.tsx";
import ProfileService from "../../../../../services/profileService.tsx";
import SignInLogo from "../../../../../assets/signIn.svg";
import Yandex from "../../../../../components/general/oauth/yandex/yandex.tsx";
import {LoginFormData} from "./loginFormData.tsx";

/**
 * Компонента для логина
 */
function LoginForm() {
  const [searchParams, _] = useSearchParams();

  const navigator = useNavigate();
  const location = useLocation();

  const [errorMessage, setErrorMessage] = useState<string>();
  const [email, setEmail] = useState<string>();
  const [password, setPassword] = useState<string>();
  const [twoFactor, setTwoFactor] = useState(false);

  useEffect(() => {
    if (location.state != null) {
      setErrorMessage(location.state.errorMessage);
    }
  }, []);

  const handleSubmit = () => {
    const formData: LoginFormData = {
      email: email!,
      password: password!,
    };

    console.log(formData);

    auth
      .post("login", formData)
      .then((response) => {
        if (response.data.twoFactorEnabled) {
          setTwoFactor(true);
          navigator(`/login/2fa?email=${email}`);
          return;
        } else {
          console.log(response);
          LoginService.storeTokens(
            response.data.accessToken,
            response.data.expiresIn,
            response.data.refreshToken
          );
          ProfileService.fetchProfile();
          const redirect = searchParams.get("redirect");
          if (redirect) navigator(decodeURIComponent(redirect));
          else navigator("/");
        }
      })
      .catch((_) => {
        setErrorMessage("Неверные имя пользователя или пароль");
      });
  };

  return (
    <div className={classes.container}>
      <img className={classes.loginImg} style={{margin:"-100px", height:"300px" }} src={SignInLogo} />
      <form
        className={classes.form}
        action="post"
        onSubmit={(e: React.FormEvent<HTMLFormElement>) => {
          e.preventDefault();
          handleSubmit();
        }}
      >
        {errorMessage && (
          <span className={classes.errorMessage}>{errorMessage}</span>
        )}
        <InputForm
          placeholder="Эл. адрес"
          type="email"
          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            setEmail(e.target.value)
          }
        />
        <InputForm
          placeholder="Пароль"
          type="password"
          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            setPassword(e.target.value)
          }
        />
        <Button style={{ width: "100%" }}>Войти</Button>
      </form>

      {twoFactor && (
        <p className={classes.response}>
          На вашу почту отправлено <br/>
          письмо, с помощью которого <br/>
          вы сможете зайти
        </p>
      )}


      <div className={classes.or}>
        <div className={classes.line}></div>
        <div style={{ color: "#494949" }}>или</div>
        <div className={classes.line}></div>
      </div>

      <Vk></Vk>
      <Yandex></Yandex>

      <div className={classes.forgotPassword}>
        <Link to="/forgotPassword">Забыли пароль?</Link>
      </div>
    </div>
  );
}

export default LoginForm;
