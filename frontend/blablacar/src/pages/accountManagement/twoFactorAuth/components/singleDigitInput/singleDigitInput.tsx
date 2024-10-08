import classes from "./styles/singleDigitInput.module.css";
import PropTypes from "prop-types";

/**
 * Компонента для ввода одной цифра кода двухфакторной авторизации
 * @param className - имя класса для чтобы переопределить стили
 * @param innerRef - ссылка на следующий ввод
 * @param props - атрибуты
 */
function SingleDigitInput({ className, innerRef, ...props }) {
  return (
    <>
      <input
        className={`${classes.singleDigitInput} ${className}`}
        ref={innerRef}
        maxLength={1}
        inputMode={"numeric"}
        {...props}
        required
      />
    </>
  );
}

SingleDigitInput.propTypes = {
  className: PropTypes.string,
  innerRef: PropTypes.oneOfType([
    PropTypes.func,
    PropTypes.shape({ current: PropTypes.instanceOf(Element) }),
  ]),
};

export default SingleDigitInput;
