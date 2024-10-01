import InputForm from "../../../../../components/general/inputform/inputform.tsx";
import Button from "../../../../../components/general/button/button.tsx";
import classes from "./styles/registrationForm.module.css";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { handleErrors, validateInput } from "./validation.ts";
import { auth } from "../../../../../config/axios.ts";
import {ErrorType, RegisterForm} from "./registerForm.ts";


/**
 * Форма регистрации
 */
function RegistrationForm() {
  const navigator = useNavigate();

  const [input, setInput] = useState<RegisterForm>();

  const [errorMessage, setErrorMessage] = useState<ErrorType>();

  const onInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setInput((prev) => ({
      ...prev,
      [name]: value,
    }));
    setErrorMessage((prev) => ({ ...prev, [e.target.name]: "" }));
  };

  useEffect(() => {
    const hasError = errorMessage ? Object.getOwnPropertyNames(errorMessage).some((key) => errorMessage[key as keyof ErrorType]) : false;
    setIsFormValid(!hasError);
  }, [errorMessage]);

  const [isFormValid, setIsFormValid] = useState(false);

  const onFinish = () => {
    Object.getOwnPropertyNames(input).forEach(key => validateInput(key, input, setErrorMessage))

    if (isFormValid) {
      auth
        .post("register", input)
        .then((response) => {
          console.log(response);
          navigator("/confirmEmail");
        })
        .catch((error) => {
          console.log(error);
          handleErrors(error.response.data.errors, setErrorMessage);
        });
    }
  };

  return (
    <div>
      <div className={classes.form}>
        <InputForm
          placeholder={"Имя"}
          name={"firstName"}
          onChange={onInputChange}
        ></InputForm>
        {errorMessage?.firstName && (
          <span className={classes.errorMessage}>{errorMessage.firstName}</span>
        )}
        <InputForm
          placeholder={"Фамилия"}
          name={"lastName"}
          onChange={onInputChange}
        ></InputForm>
        {errorMessage?.lastName && (
          <span className={classes.errorMessage}>{errorMessage.lastName}</span>
        )}
        <div className={classes.genders}>
          <InputForm
            name={"birthday"}
            onChange={onInputChange}
            type={"date"}
            style={{width:"100%"}}
            required
          ></InputForm>
          <div className={classes.blockGender}>
            <label className={classes.labelGenders}>Дата рождения</label>
          </div>
        </div>
        <InputForm
          placeholder={"Эл. адрес"}
          name={"email"}
          onChange={onInputChange}
        ></InputForm>
        {errorMessage?.email && (
          <span className={classes.errorMessage}>{errorMessage.email}</span>
        )}
        <InputForm
          placeholder={"Пароль"}
          name={"password"}
          type="password"
          onChange={onInputChange}
        ></InputForm>
        {errorMessage?.password && (
          <span className={classes.errorMessage}>{errorMessage.password}</span>
        )}
        <InputForm
          placeholder={"Повторите пароль"}
          name={"passwordConfirm"}
          type="password"
          onChange={onInputChange}
        ></InputForm>
        {errorMessage?.passwordConfirm && (
          <span className={classes.errorMessage}>
            {errorMessage.passwordConfirm}
          </span>
        )}
        <Button style={{ margin: "auto", width: "60%" }} onClick={onFinish}>Далее</Button>
      </div>
    </div>
  );
}

export default RegistrationForm;
