import classes from "../inputform/inputform.module.css";

/**
 * Компонент для большого ввода
 * @param props атрибуты
 */
function InputFormArea({...props}) {
  return <textarea className={classes.inputform} {...props}></textarea>;
}

export default InputFormArea;
