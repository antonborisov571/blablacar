import WhereIcon from "../../../assets/input/where.svg";
import DateIcon from "../../../assets/input/date.svg";
import PeopleIcon from "../../../assets/input/people.svg";
import PriceIcon from "../../../assets/input/price.svg"
import classes from "./createTripForm.module.css";
import {useEffect, useState} from "react";
import {api} from "../../../config/axios.ts";
import InputWhere from "./components/inputWhere/inputWhere.tsx";
import {useProfile} from "../../../hooks/profile/useProfile.ts";
import {useNavigate} from "react-router-dom";
/**
 * CreateTripForm компонент для создания новой поездки.
 * @returns {JSX.Element} Возвращает JSX-элемент для компоненты CreateTripForm.
 */
function CreateTripForm(): JSX.Element {
  const [profile, isProfile] = useProfile();
  const navigation = useNavigate();

  const [where, setWhere] = useState("");
  const [whereFrom, setWhereFrom] = useState("");
  const [dateTimeTrip, setDateTimeTrip] = useState("Дата");
  const [countPassengers, setCountPassengers] = useState<number>();
  const [price, setPrice] = useState<number>()

  const [response, setResponse] = useState<string>();
  const [error, setError] = useState<string>()

  /**
   * Отправляет данные о путешествии на сервер для создания.
   */
  const sendTrip = () => {
    if (!profile) {
      navigation("/login");
    }

    if (where != ""
        && whereFrom != ""
        && countPassengers != undefined
        && price != undefined
        && dateTimeTrip != "Дата") {
      api
        .post("trips/createTrip",
          {
            where,
            whereFrom,
            dateTimeTrip: new Date(dateTimeTrip),
            countPassengers,
            price
          })
        .then(() => {
          setResponse("Поездка успешно опубликована, " +
            "вы можете её посмотреть в личном кабинете");
          setError(undefined);
        })
        .catch(error => {
          const messages = error.response.data;
          setError(messages[0].errorMessage);
          setResponse(undefined);
        });
    }
  }

  /**
   * Возвращает текущую дату и время в формате "yyyy-mm-ddT00:00".
   * @returns {string} Текущая дата и время в формате "yyyy-mm-ddT00:00".
   */
  const getCurrentDate = () => {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    let month: string | number = currentDate.getMonth() + 1;
    let day: string | number = currentDate.getDate();

    month = month < 10 ? '0' + month : month;
    day = day < 10 ? '0' + day : day;

    return `${year}-${month}-${day}T00:00`;
  }

  /**
   * Обновляет количество пассажиров в зависимости от введенного значения.
   * @param {React.ChangeEvent<HTMLInputElement>} ev Событие ввода значения.
   */
  const updatePassengers = (ev) => {
    const value = Number(ev.target.value);
    if (value < 1)
      setCountPassengers(undefined);
    else if (value > 8)
      setCountPassengers(8);
    else setCountPassengers(value)
  }

  /**
   * Обновляет стоимость путешествия в зависимости от введенного значения.
   * @param {React.ChangeEvent<HTMLInputElement>} ev Событие ввода значения.
   */
  const updatePrice = (ev) => {
    const value = Number(ev.target.value);
    if (value < 1)
      setPrice(undefined);
    else if (value > 10000)
      setPrice(10000);
    else setPrice(value)
  }

  return (
    <div className={classes.searchWrapper}>
      <div className={classes.search}>
        <div className={classes.whereFromWhere}>
          <div className={classes.whereInputWrapper}>
            <div className={`${classes.whereIconWrapper}`}>
              <img height="24px" src={WhereIcon}></img>
            </div>
            <InputWhere
              placeholder={"Откуда"}
              value={whereFrom}
              setValue={setWhereFrom}
            ></InputWhere>
          </div>
          <hr className={classes.spliter}/>
          <div className={classes.whereInputWrapper}>
            <div className={classes.whereIconWrapper}>
              <img height="24px" src={WhereIcon}></img>
            </div>
            <InputWhere
              placeholder={"Куда"}
              value={where}
              setValue={setWhere}
            ></InputWhere>
          </div>
          <hr className={classes.spliter}/>
        </div>
        <div className={classes.whereInputWrapper}>
          <div className={classes.whereIconWrapper}>
            <img height="24px" src={DateIcon}></img>
          </div>
          <input
            type="datetime-local"
            placeholder={dateTimeTrip}
            className={classes.dateInput}
            onChange={(ev) => setDateTimeTrip(ev.target.value)}
            min={getCurrentDate()}
          />
        </div>
        <hr className={classes.spliter}/>
        <div className={classes.whereInputWrapper}>
          <div className={classes.whereIconWrapper}>
            <img height="24px" src={PeopleIcon}></img>
          </div>
          <input
            className={`${classes.whereInput}`}
            type="number"
            placeholder={"Сколько пассажиров"}
            value={countPassengers}
            onChange={(ev) => updatePassengers(ev)}
          ></input>
        </div>
        <hr className={classes.spliter}/>
        <div className={classes.whereInputWrapper}>
          <div className={classes.whereIconWrapper}>
            <img height="24px" src={PriceIcon}></img>
          </div>
          <input
            className={`${classes.whereInput}`}
            type="number"
            placeholder={"Цена"}
            value={price}
            onChange={(ev) => updatePrice(ev)}
          ></input>
        </div>
        <div className={classes.buttonSearchWrapper}>
          <button
            className={`${classes.buttonSearch}`}
            onClick={sendTrip}
          >
            Опубликовать поездку
          </button>
        </div>
      </div>
      <div className={classes.responseWrapper}>
        {response
          ? <p className={classes.response}>{response}</p>
          : error
            ? <p className={classes.error}>{error}</p>
            : ""}
      </div>
    </div>
  )
}

export default CreateTripForm;