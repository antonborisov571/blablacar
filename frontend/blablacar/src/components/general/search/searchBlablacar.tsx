import WhereIcon from "../../../assets/input/where.svg";
import DateIcon from "../../../assets/input/date.svg";
import PeopleIcon from "../../../assets/input/people.svg";
import classes from "./searchBlablacar.module.css";
import {useEffect, useState} from "react";
import {api} from "../../../config/axios.ts";
import InputWhere from "./components/inputWhere/inputWhere.tsx";
import {TripCardSearch} from "../../../hooks/types/TripCardSearch.ts";
import {useLocation, useNavigate} from "react-router-dom";
import queryString from "query-string";


/**
 * Интерфейс свойств для компоненты
 */
interface Props {
  /**
   * Полученные поездки
   */
  onTripsReceived?: ((trips: TripCardSearch[]) => void) | undefined;

  /**
   * Для перемещения на страницу поиска
   */
  navigateSearchPage?: boolean;

  /**
   * Фильтрации по предпочтениям о курении
   */
  isSmoking?: boolean;

  /**
   * Фильтрации по предпочтениям о животных
   */
  isAnimal?: boolean;

  /**
   * Фильтрации по предпочтениям о музыки
   */
  isMusic?: boolean;

  /**
   * Создание уведомление о поездки
   */
  isNotification?: boolean;
}

/**
 * Функция для поиска поездок Blablacar.
 * @param {Props} props - Свойства компоненты.
 */
function SearchBlablacar({
  onTripsReceived,
  navigateSearchPage,
  isSmoking,
  isAnimal,
  isMusic,
  isNotification
} : Props) {
  const location = useLocation();

  const [where, setWhere] = useState("");
  const [whereFrom, setWhereFrom] = useState("");
  const [date, setDate] = useState("Дата");
  const [countPassengers, setCountPassengers] = useState<number>();

  const navigate = useNavigate();

  useEffect(() => {
    const queryParams = queryString.parse(location.search);
    const whereValue = queryParams.where?.toString();
    const whereFromValue = queryParams.whereFrom?.toString();
    const dateValue = queryParams.date?.toString();
    const countPassengersValue =
      queryParams.countPassengers
        ? Number(queryParams.countPassengers?.toString())
        : undefined;
    if (whereValue) setWhere(whereValue);
    if (whereFromValue) setWhereFrom(whereFromValue);
    if (dateValue) setDate(dateValue);
    if (countPassengersValue && !isNaN(countPassengersValue))
      setCountPassengers(countPassengersValue);
  }, [location.search]);

  useEffect(() => {
    if (where != ""
      && whereFrom != ""
      && date != "Дата"
      && countPassengers != undefined) {
      api
        .get("trips/getTrips", { params: {
            where,
            whereFrom,
            dateTimeTrip: date,
            countPassengers,
            isSmoking,
            isAnimal,
            isMusic
          }
        })
        .then(res => {
          console.log(res.data);
          if (onTripsReceived) onTripsReceived(res.data.trips);
          else if (navigateSearchPage) {
            const query = {
              where: where,
              whereFrom: whereFrom,
              date: date,
              countPassengers: Number(countPassengers)
            };
            // @ts-ignore
            const searchParams = new URLSearchParams(query).toString();
            navigate(`/search?${searchParams}`, {state: {trips: res.data.trips}})
          }
        })
        .catch(error => {
          console.log(error);
        });
    }
  }, [isSmoking, isAnimal, isMusic]);

  useEffect(() => {
    if (where != ""
      && whereFrom != ""
      && date != "Дата") {
      api
        .post("trips/createTripNotification", {
            where,
            whereFrom,
            dateTimeTrip: date
        })
        .catch(error => console.log(error));
    }
  }, [isNotification]);

  const getCurrentDate = () => {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    let month: string | number = currentDate.getMonth() + 1;
    let day: string | number = currentDate.getDate();

    month = month < 10 ? '0' + month : month;
    day = day < 10 ? '0' + day : day;

    return `${year}-${month}-${day}`;
  }

  const updatePassengers = (ev) => {
    const value = Number(ev.target.value);
    if (value < 1)
      setCountPassengers(undefined);
    else if (value > 8)
      setCountPassengers(8);
    else setCountPassengers(value)
  }

  const fetchTrips = () => {
    if (where != ""
      && whereFrom != ""
      && date != "Дата"
      && countPassengers != undefined) {
      api
        .get("trips/getTrips", { params: {
            where,
            whereFrom,
            dateTimeTrip: date,
            countPassengers,
          }
        })
        .then(res => {
          console.log(res.data);
          if (onTripsReceived) onTripsReceived(res.data.trips);
          else if (navigateSearchPage) {
            const query = {
              where: where,
              whereFrom: whereFrom,
              date: date,
              countPassengers: Number(countPassengers)
            };
            // @ts-ignore
            const searchParams = new URLSearchParams(query).toString();
            navigate(`/search?${searchParams}`, {state: {trips: res.data.trips}})
          }
        })
        .catch(error => {
          console.log(error);
        });
    }
  };

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
            type="date"
            placeholder={date}
            className={classes.dateInput}
            onChange={(ev) => setDate(ev.target.value)}
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
        <div className={classes.buttonSearchWrapper}>
          <button
            className={`${classes.buttonSearch}`}
            onClick={fetchTrips}
          >Поиск
          </button>
        </div>
      </div>
    </div>
  )
}

export default SearchBlablacar;