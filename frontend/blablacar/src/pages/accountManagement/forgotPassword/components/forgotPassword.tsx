import {useState} from "react";
import classes from "./styles/forgotPassword.module.css";
import ForgotPasswordImg from "../../../../assets/forgotPassword.svg";
import {auth} from "../../../../config/axios.ts";
import InputForm from "../../../../components/general/inputform/inputform.tsx";
import Button from "../../../../components/general/button/button.tsx";
import {ForgotPasswordFormData} from "./forgotPasswordFormData.tsx";

/**
 * Компонента для страницы забыли пароль
 */
function ForgotPasswordComponent() {
  const [email, setEmail] = useState("");
  const [sent, setSent] = useState<Boolean>();
  const [error, setError] = useState<string | undefined>();
  const [buttonDisabled, setButtonDisabled] = useState("");

  const handleSubmit = () => {
    // https://emailregex.com/index.html
    if (
      !/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(
        email
      )
    ) {
      setError("Вы ввели неправильный электронный адрес");
      return;
    }
    const formData: ForgotPasswordFormData = {
      email: email!,
    };

    console.log(formData);
    setError(undefined);
    auth
      .post("forgotPassword", formData)
      .then((_) => {
        setSent(true);
        setButtonDisabled("t");
      })
      .catch((error) => {
        setError("Произошла ошибка.");
        console.log(error);
      });
  };

  return (
    <div className={classes.container}>
      <img className={classes.image} src={ForgotPasswordImg} />
      <div className={classes.title}>Забыли пароль?</div>
      <div className={classes.description}>
        Введите электронную почту. Ссылка для замены пароля будет отправлена на
        указанную электронную почту
      </div>

      <form
        className={classes.form}
        action="post"
        onSubmit={(e: React.FormEvent<HTMLFormElement>) => {
          e.preventDefault();
          handleSubmit();
        }}
      >
        <InputForm
          placeholder="Эл. адрес"
          type="email"
          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            setEmail(e.target.value)
          }
          required="t"
        />
        <Button type="submit" disabled={buttonDisabled}>
          Отправить
        </Button>
      </form>
      {sent ? (
        <p className={classes.sent}>
          Письмо отправлено на электронный ящик пользователя с указанной почтой.
        </p>
      ) : (
        <></>
      )}
      {error != undefined ? <p className={classes.error}>{error}</p> : <></>}
    </div>
  );
}

export default ForgotPasswordComponent;
