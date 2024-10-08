import classes from "./checkbox.module.css";

/**
 * Компонент чекбокса
 * @param props атрибуты
 */
function Checkbox({...props}) {
  return (
    <input type="checkbox" className={classes.checkbox} {...props}></input>
  );
}

export default Checkbox;
