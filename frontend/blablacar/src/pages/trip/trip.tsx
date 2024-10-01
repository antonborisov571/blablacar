import {useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {api} from "../../config/axios.ts";
import {TripBook} from "../../hooks/types/TripBook.ts";
import classes from "./styles/trip.module.css";
import {format} from "date-fns";
import ruLocale from "date-fns/locale/ru";
import Avatar from "react-avatar";
import ArrowIcon from "../../assets/trip/arrow.svg"
import ConnectIcon from "../../assets/trip/connect.svg"
import DateIcon from "../../assets/trip/date.svg"
import {MapperAnimal, MapperMusic, MapperSmoking} from "../profile/components/info/mapperPreferences.tsx";
import SmokingIcon from "../../assets/profile/smoking.svg";
import MusicIcon from "../../assets/profile/music.svg";
import AnimalIcon from "../../assets/profile/animal.svg";
import Button from "../../components/general/button/button.tsx";
import Loader from "../../components/general/loader/loader.tsx";
import NotFound from "../../components/general/responses/notFound/notFound.tsx";
import HandlerErrors from "../../config/handlerErrors.tsx";
import {useProfile} from "../../hooks/profile/useProfile.ts";
import TrashButton from "../../components/general/trashButton";

/**
 * Страница с поездкой
 */
function Trip(){
  const { tripId } = useParams();

  const [profile, _] = useProfile();

  const [isLoading, setIsLoading] = useState(true);

  const [trip, setTrip] = useState<TripBook>();

  const [statusCode, setStatusCode] = useState<number>();
  const [isSuccess, setIsSuccess] = useState<boolean>(false);

  const navigate = useNavigate();

  const id = Number(tripId);
  if (isNaN(id)) return <NotFound></NotFound>;

  useEffect(() => {
    api
      .get(`trips/${id}`)
      .then(response =>{
        setIsLoading(false);
        setTrip(response.data);
        setStatusCode(200);
      })
      .catch(error => {
        setStatusCode(error.response.status)
      });
  }, []);

  const formatDate = (date: Date) => {
    return format(date, 'd MMMM yyyy', {
      // eslint-disable-next-line @typescript-eslint/ban-ts-comment
      // @ts-expect-error
      locale: ruLocale 
    });
  };

  const booking = () => {
    api
      .post(`trips/booking`, {tripId})
      .then(response => {
        setIsSuccess(true);
      })
      .catch(error => {
        console.log(error);
      })
  };

  const deletePassenger = (id: string) => {
    api
      .delete(`trips/deletePassenger`, {params: {passengerId: id, tripId}})
      .then(response => {
        const newTrip = {passengers: trip?.passengers.filter(x => x.id === id), ...trip}
        setTrip(newTrip);
      })
      .catch(error => {
        console.log(error);
      })
  }

  const deleteTrip = (id: string) => {
    api
      .delete(`trips/deleteTrip`, {params: {tripId}})
      .then(response => {
        navigate("/");
      })
      .catch(error => {
        console.log(error);
      })
  }

  if (isLoading) return (<Loader></Loader>)

  if (statusCode && statusCode % 400 < 200)
    return <HandlerErrors statusCode={statusCode}></HandlerErrors>

  return (
    <div className={classes.tripWrapperWrapper}>
      <div className={classes.tripWrapper}>
        <h1 className={classes.title}>
          {formatDate(trip!.dateTimeTrip)}
        </h1>
        <div className={classes.infoTripWrapperWrapper}>
          <div className={classes.infoTripWrapper}>
            <div className={classes.infoTripTime}>
              {format(new Date(trip!.dateTimeTrip), 'HH:mm')}
            </div>
            <div className={classes.infoTripWhereFromWrapper}>
              <div className={classes.infoTripWhereFrom}></div>
            </div>
            <div className={classes.infoTripWhereFromName}>
              {trip?.whereFrom}
            </div>
            <div className={classes.infoTripWhereWrapper}>
              <div className={classes.infoTripWhere}></div>
            </div>
            <div className={classes.infoTripWhereName}>
              {trip?.where}
            </div>
          </div>
          <div>
            {trip?.price} ₽
          </div>
        </div>
        <hr className={classes.spliter}/>
        <div className={classes.infoPrice}>
          <div className={classes.countPassengers}>
            Итого за 1 пассажира
          </div>
          <div className={classes.price}>
            {trip?.price} ₽
          </div>
        </div>
        <hr className={classes.spliter}/>
        <div className={classes.infoDriver} onClick={() => navigate(`/users/${trip?.driver.id}`)}>
          <div className={classes.driverName}>
            {trip?.driver.driverName}
          </div>
          <div className={classes.avatarWithArrow}>
            <Avatar
              name={trip?.driver.driverName}
              size="50"
              className={classes.profileAvatar}
              src={trip?.driver.driverAvatar ? `data:image;base64,${trip?.driver.driverAvatar}` : undefined}
            ></Avatar>
            <img className={classes.arrowIcon} src={ArrowIcon}/>
          </div>
        </div>
        {!trip?.isDriver && (
          <div>
            <hr className={classes.spliterMini}/>
            <div className={classes.connectDriver} onClick={() => {
              if (profile != null)
                navigate(`/users/${trip?.driver.id}`);
              else navigate("/login");
            }}>
              <img height={"24px"} src={ConnectIcon}/>
              <p>Связаться с {trip?.driver.driverName}</p>
            </div>
            {/*<hr className={classes.spliterMini}/>
            <div className={classes.connectTime}>
              <img height={"24px"} src={DateIcon}/>
              <p>Ваше бронирование будет подтверждено только после одобрения водителя</p>
            </div>*/}
          </div>
        )}
        <hr className={classes.spliterMini}/>
        <div className={classes.preferencesWrapper}>
          <div className={classes.preferences}>
            <img height={"24px"} src={SmokingIcon}/>
            <p>{MapperSmoking[trip!.driver.preferencesSmoking]}</p>
          </div>
          <div className={classes.preferences}>
            <img height={"24px"} src={AnimalIcon}/>
            <p>{MapperAnimal[trip!.driver.preferencesAnimal]}</p>
          </div>
          <div className={classes.preferences}>
            <img height={"24px"} src={MusicIcon}/>
            <p>{MapperMusic[trip!.driver.preferencesMusic]}</p>
          </div>
          {/*<div className={classes.preferences}>
            <img src={"1"}/>
            <p>{MapperTalking[trip?.driver.talkingType]}</p>
          </div>*/
          }
        </div>
        <hr className={classes.spliter}/>
        {trip!.passengers.length > 0
          ?
          <h1 className={classes.titlePassengers}>Пассажиры</h1>
          :
          <h1 className={classes.titlePassengers}>Пассажиров нет</h1>
        }
        <ul>
          {trip?.passengers.map((passenger, index) => (
            <li key={index}>
              <div className={classes.infoDriver} onClick={() => {
                if (profile != null)
                  navigate(`/users/${passenger.id}`);
                else navigate("/login");
              }}>
                <div className={classes.driverName}>
                  {passenger.firstName}
                </div>
                <div className={classes.avatarWithArrow}>
                  <Avatar
                    name={passenger.firstName}
                    size="50"
                    className={classes.profileAvatar}
                    src={passenger.avatar ? `data:image;base64,${passenger.avatar}` : undefined}
                  ></Avatar>
                  {trip?.isDriver
                    ?
                    <TrashButton className={classes.arrowIcon} onClick={(e) => {
                      e.stopPropagation(); deletePassenger(passenger.id);
                    }}></TrashButton>
                    :
                    <img className={classes.arrowIcon} src={ArrowIcon}/>
                  }
                </div>
              </div>
            </li>
          ))}
        </ul>
      </div>
      {!trip?.isPassenger && profile && trip?.isActive && (
        <div className={classes.bookWrapper}>
          {!isSuccess && !trip?.isDriver
            ?
            <Button onClick={booking}>Забронировать</Button>
            :
            trip?.isDriver
              ?
              <Button onClick={deleteTrip}>Удалить поездку</Button>
              :
              <span className={classes.success}>Вы успешно забронировали поездку</span>
          }
        </div>
      )}
    </div>
  );
}

export default Trip;