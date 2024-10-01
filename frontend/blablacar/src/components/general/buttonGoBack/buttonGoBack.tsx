import classes from "./buttonGoBack.module.css";

/**
 * Компонент кнопки возращения назад
 * @param props атрибуты
 */
function ButtonGoBack({ ...props }) {
  return (
    <>
      <button className={classes.buttonGoBack} {...props}></button>
    </>
  );
}

export default ButtonGoBack;
