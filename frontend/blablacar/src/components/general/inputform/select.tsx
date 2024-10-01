import classes from "./select.module.css";

/**
 * Компонент select
 * @param props атрибуты
 */
function Select({...props}) {
  return (
    <div className={classes.customSelect}>
      <select {...props}></select>
    </div>
  );
}

export default Select;
