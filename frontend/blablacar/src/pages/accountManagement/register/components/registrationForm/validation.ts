import {ErrorType, RegisterForm} from "./registerForm.ts";

/**
 * Валидатор полей
 */
export const validateInput = (
  field: string,
  input : RegisterForm | undefined, 
  setErrorMessage:  React.Dispatch<React.SetStateAction<ErrorType | undefined>>) => {
  const { name, value } = {name: field, value: input[field]};
  setErrorMessage(({...prev}) => {
    const stateObj = { ...prev, [name]: "" };

    switch (name) {
      case "firstName":
        if (!value) {
          stateObj[name] = "Введите имя";
        }
        break;

      case "lastName":
        if (!value) {
          stateObj[name] = "Введите фамилию";
        }
        break;

      case "email":
        if (!value) {
          stateObj[name] = "Введите электронный адрес";
        }
        break;

      case "password":
        if (!/(?=.*\d)/.test(value)) {
          stateObj[name] = "Пароль должен содержать \n хотя бы одну цифру";
        }

        if (!/(?=.*[a-z])/.test(value)) {
          stateObj[name] = "Пароль должен содержать \n хотя бы одну строчную букву";
        }

        if (!/(?=.*[A-Z])/.test(value)) {
          stateObj[name] = "Пароль должен содержать \n хотя бы одну заглавную букву";
        }

        if (!/(?=.*[!@#$%^&*.,?])/.test(value)) {
          stateObj[name] = "Пароль должен содержать \n хотя бы один специальный символ";
        }

        if (!/[0-9a-zA-Z!@#$%^&*]{8,}/.test(value)) {
          stateObj[name] = "Пароль должен состоять \n из 8 или более латинских символов ";
        } else if (input?.passwordConfirm && value !== input.passwordConfirm) {
          stateObj["passwordConfirm"] = "Введенные пароли не совпадают";
        } else {
          stateObj["passwordConfirm"] = input?.passwordConfirm
            ? ""
            : "Введенные пароли не совпадают";
        }
        break;

      case "passwordConfirm":
        if (input?.password && value !== input.password) {
          stateObj[name] = "Введенные пароли не совпадают";
        }
        break;

      default:
        break;
    }

    return stateObj;
  });
};

export const handleErrors = (
  errors: any,
  setErrorMessage: React.Dispatch<React.SetStateAction<ErrorType | undefined>>) => {
  const keys = Object.keys(errors);
  if (keys.includes("DuplicateUserName")) {
    setErrorMessage((prev) => ({
      ...prev,
      email: "Введенная электронная почта уже используется",
    }));
  } else if (keys.some((key) => key.startsWith("Password"))) {
    setErrorMessage((prev) => ({
      ...prev,
      password: "Введенный пароль слишком простой",
    }));
  }
};
