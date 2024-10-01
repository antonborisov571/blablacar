import {useNavigate} from "react-router-dom";
import classes from "../notFound/notFound.module.css";
import Button from "../../button/button.tsx";

/**
* Компонент для плохого запроса
* @returns {JSX.Element} - Возвращает JSX-элемент, представляющий компонент плохого запроса
*/
function BadRequest() {
  const navigate = useNavigate();

  return (
    <div className={classes.errorWrapperWrapper}>
      <div className={classes.errorWrapper}>
        <div>
          <h1>Ошибка 400</h1>
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
export default BadRequest;
