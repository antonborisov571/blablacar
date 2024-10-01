import classes from "./styles/confirmEmail.module.css";
import ConfirmEmailImg from "../../../../assets/confirmEmail.svg";
import Card from "../../../../components/general/card/card.tsx";
import { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { auth } from "../../../../config/axios.ts";
import { useProfile } from "../../../../hooks/profile/useProfile.ts";

/**
 * Компонента для страницы подтверждения почты
 */
function ConfirmEmailComponent() {
  const navigator = useNavigate();
  const [searchParams, _] = useSearchParams();

  const [isValidLink, setIsValidLink] = useState(true);

  const [profile, loading] = useProfile();

  useEffect(() => {
    const code = searchParams.get("code");
    const email = searchParams.get("email");
    if (loading) return;

    if (code == null || email == null) {
      navigator("/login");
      return;
    }

    auth
      .post("confirmEmail", { code: code, email: email })
      .then((response) => {
        console.log(response);
        setIsValidLink(true);
        setTimeout(() => navigator("/login"), 3000);
      })
      .catch((reason) => {
        console.log(reason);
        if (reason.response.status === 401) {
          setIsValidLink(false);
        }
      });
  }, [loading]);


  if (!isValidLink) {
    return (
      <>
        <div className={classes.invalidLink}>
          Cрок действия ссылки истек. Запросите новую ссылку для подтверждения
          почты.
        </div>
      </>
    );
  }

  return (
    <>
      <div>
        <Card className={classes.cardConfirmEmail}>
          <div className={classes.confirmEmailImg}>
            <img src={ConfirmEmailImg} />
          </div>
          <div className={classes.textInfo}>
            <div className={classes.title}>Почта была успешно подтверждена</div>
          </div>
        </Card>
      </div>
    </>
  );
}

export default ConfirmEmailComponent;
