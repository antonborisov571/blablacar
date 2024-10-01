import classes from "./radio.module.css";

/**
 * Компонент чекбокса
 * @param props атрибуты
 */
function Radio({...props}) {
  return <input type="radio" className={classes.radio} {...props}></input>;
}

export default Radio;
