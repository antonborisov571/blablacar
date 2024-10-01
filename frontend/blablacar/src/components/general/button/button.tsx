import classes from "./button.module.css";

/**
 * Компонент кнопки
 * @param props атрибуты
 */
function Button({...props}) {
  return (
    <>
      <button className={classes.button} {...props}></button>
    </>
  );
}

export default Button;
