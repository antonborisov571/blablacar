import classes from "./styles/cardTwoFactor.module.css";
import Card from "../../../../../components/general/card/card.tsx";
import TwoFactorImg from "../../../../../assets/twoFactor.svg";
import Button from "../../../../../components/general/button/button.tsx";
import { useState, useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import SixDigitsInput from "../sixDigitsInput/sixDigitsInput.tsx";
import { auth } from "../../../../../config/axios.ts";
import LoginService from "../../../../../services/loginService.tsx";

/**
 * Компонента карточки с двухфакторной аутентификации
 */
function CardTwoFactor() {
  const navigator = useNavigate();
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);

  const [digits, setDigits] = useState("");
  const [email, setEmail] = useState();
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    if (
      searchParams.get("email") == null
    ) {
      navigator("/login");
    } else {
      // eslint-disable-next-line @typescript-eslint/ban-ts-comment
      // @ts-expect-error
      setEmail(searchParams.get("email"));
    }
  }, []);

  const sendCode = () => {
    if (digits !== "") {
      auth
        .post("twofactor", {
          email: email,
          code: digits
        })
        .then((response) => {
          console.log(response);
          LoginService.storeTokens(
            response.data.accessToken,
            response.data.expiresIn,
            response.data.refreshToken
          );
          navigator("/");
        })
        .catch((reason) => {
          const message =
            reason.response == null || reason.response.status >= 500
              ? "Ошибка сервера"
              : reason.response.data.detail == "LockedOut"
                ? "Слишком много неверных попыток входа, подождите несколько минут и попробуйте еще раз."
                : "Неверные данные для входа";

          navigator("/login", {
            state: {
              errorMessage: message,
            },
          });
        });
    } else {
      setErrorMessage("Поле не заполнено");
    }
  };

  return (
    <>
      <Card className={classes.cardTwoFactor}>
        <div className={classes.twoFactorImg}>
          <img src={TwoFactorImg} />
        </div>
        <div className={classes.textInfo}>
          <div className={classes.title}>
            Аутентифицируйте свою учетную <br />
            запись
          </div>
          <div className={classes.pleaseEnterCode}>
            Введите код, который вам пришёл на почту <br />
            для 2-факторной аутентификации
          </div>
          <SixDigitsInput onChange={setDigits}></SixDigitsInput>
          {errorMessage && (
            <span className={classes.errorMessage}>{errorMessage}</span>
          )}
          <Button onClick={sendCode}>
            подтвердить
          </Button>
        </div>
      </Card>
    </>
  );
}

export default CardTwoFactor;
