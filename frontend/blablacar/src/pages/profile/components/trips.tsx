import classes from "./styles/trips.module.css";
import {useEffect, useState} from "react";
import {TripCardSearch} from "../../../hooks/types/TripCardSearch.ts";
import {api} from "../../../config/axios.ts";
import Avatar from "react-avatar";
import Button from "../../../components/general/button/button.tsx";
import {useNavigate} from "react-router-dom";
import {format} from "date-fns";

/**
 * Компонент с поездками пользователя в профиле
 */
function Trips() {

  const [trips, setTrips] =
    useState<Array<TripCardSearch>>([]);

  const navigate = useNavigate();

  useEffect(() => {
    api
      .get("trips/getTripsUser")
      .then(response => {
        setTrips(response.data.trips);
        console.log(response.data)
      })
      .catch(error => {
        console.log(error);
      })
  }, []);

  return (
    <div className={classes.tripsWrapper}>
      <h1>Ваши активные поездки</h1>
      <ul>
        {trips.map((trip, index) => {
          return (
            <li key={index}>
              <div className={classes.cardTrip} key={trip.id} onClick={() => navigate(`/trips/${trip.id}`)}>
                <div className={classes.infoTripWrapperWrapper}>
                  <div className={classes.infoTripWrapper}>
                    <div className={classes.infoTripTime}>
                      {format(new Date(trip.dateTimeTrip), 'HH:mm')}
                    </div>
                    <div className={classes.infoTripWhereFromWrapper}>
                      <div className={classes.infoTripWhereFrom}></div>
                    </div>
                    <div className={classes.infoTripWhereFromName}>
                      {trip.whereFrom}
                    </div>
                    <div className={classes.infoTripWhereWrapper}>
                      <div className={classes.infoTripWhere}></div>
                    </div>
                    <div className={classes.infoTripWhereName}>
                      {trip.where}
                    </div>
                  </div>
                  <div>
                    {trip.price} ₽
                  </div>
                </div>
                <div className={classes.infoDriverBook}>
                  <div className={classes.infoDriver}>
                    <Avatar
                      name={trip.driverName}
                      size="50"
                      className={classes.profileAvatar}
                      src={trip.driverAvatar ? `data:image;base64,${trip.driverAvatar}` : undefined}
                    ></Avatar>
                    <p>
                      {trip.driverName}
                    </p>
                  </div>
                  <div className={classes.book}>
                    <Button>Перейти</Button>
                  </div>
                </div>
              </div>
            </li>
          )
        })}
      </ul>
    </div>
  );
}

export default Trips;