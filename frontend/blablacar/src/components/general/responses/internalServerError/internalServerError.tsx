import classes from "./internalServerError.module.css"
import Button from "../../button/button.tsx";
import {useNavigate} from "react-router-dom";

/**
 * Компонент для ошибки на сервере
 * @returns {JSX.Element} - Возвращает JSX-элемент, представляющий компонент ошибки на сервере
 */
function InternalServerError() {
  const navigate = useNavigate();

  return (
    <div className={classes.errorWrapperWrapper}>
      <div className={classes.errorWrapper}>
        <div>
          <h1>Ошибка 500</h1>
        </div>
        <div>
          <p className={classes.errorText}>
            Кажется что-то пошло не так!
          </p>
        </div>
        <Button onClick={() => navigate("/")}>Перейти на главную</Button>
      </div>
    </div>
  );
}

export default InternalServerError;
