import classes from "./notFound.module.css"
import Button from "../../button/button.tsx";
import {useNavigate} from "react-router-dom";

/**
 * Компонент для ошибки 404
 * @returns {JSX.Element} - Возвращает JSX-элемент, представляющий компонент ошибки 404
 */
function NotFound() {
  const navigate = useNavigate();

  return (
    <div className={classes.errorWrapperWrapper}>
      <div className={classes.errorWrapper}>
        <div>
          <h1>Ошибка 404</h1>
        </div>
        <div>
          <p className={classes.errorText}>
            Кажется что-то пошло не так!
            Страница, которую вы запрашиваете,
            не существует. Возможно она устарела,
            была удалена, или был введен неверный
            адрес в адресной строке
          </p>
        </div>
        <Button onClick={() => navigate("/")}>Перейти на главную</Button>
      </div>
    </div>
  );
}

export default NotFound;
