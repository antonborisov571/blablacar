import {Watch} from 'react-loader-spinner';
import classes from "./loader.module.css";

/*
* Компонент который представляет собой загрузку
* @returns {JSX.Element} - Возвращает JSX-элемент, представляющий компонент загрузки.
*/
function Loader() {
  return (
    <div className={classes.loaderWrapper}>
      <Watch
        visible={true}
        height="160"
        width="160"
        radius="48"
        color="hsla(189, 89%, 17%, 1)"
        ariaLabel="watch-loading"
        wrapperStyle={{}}
        wrapperClass=""
      />
    </div>
  );
}

export default Loader;