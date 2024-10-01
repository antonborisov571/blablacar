import classes from "./card.module.css";
import PropTypes from 'prop-types';

/**
 * Компонент карточки
 * @param props атрибуты
 */
function Card({...props}) {
    return (
        <>
          <div className={classes.card} {...props}></div>
        </>
    );
}

Card.propTypes = {
  className: PropTypes.string,
};

export default Card;
