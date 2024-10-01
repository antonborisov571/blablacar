import classes from "./inputform.module.css";

/**
 * Компонент формы
 * @param props атрибуты
 */
function InputForm({...props}) {
  return <input type="text" className={classes.inputform} {...props}></input>;
}

export default InputForm;
