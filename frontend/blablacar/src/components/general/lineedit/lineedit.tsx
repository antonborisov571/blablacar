import classes from "./lineedit.module.css";

/**
 * Компонент ввода
 * @param props атрибуты
 */
function LineEdit({...props}) {
  return <input type="text" className={classes.lineedit} {...props}></input>;
}

export default LineEdit;
