import SearchBlablacar from "../../components/general/search/searchBlablacar.tsx";
import classes from "./styles/searchPage.module.css"
import SearchFilter from "./components/searchFilter/searchFilter.tsx";
import CardsContainer from "./components/cardsContainer/cardsContainer.tsx";
import {Helmet} from "react-helmet"
import Button from "../../components/general/button/button.tsx";
import {useEffect, useState} from "react";
import {TripCardSearch} from "../../hooks/types/TripCardSearch.ts";
import {useLocation} from "react-router-dom";

/**
 * Страница поиска
 */
function SearchPage() {
  const [trips, setTrips] =
    useState<Array<TripCardSearch>>([]);

  const [isEarly, setIsEarly] = useState(true);
  const [isLate, setIsLate] = useState(false);
  const [isCheap, setIsCheap] = useState(false);

  const [isSmoking, setIsSmoking] = useState(false);
  const [isAnimal, setIsAnimal] = useState(false);
  const [isMusic, setIsMusic] = useState(false);

  const [isNotification, setIsNotification] = useState(false);

  const location = useLocation();

  useEffect(() => {
    if (location.state && location.state.trips) {
      setTrips(location.state.trips.sort((a: TripCardSearch, b: TripCardSearch) =>
        new Date(a.dateTimeTrip) < new Date(b.dateTimeTrip)
          ? -1
          : 1));
    }
  }, [location.state]);

  useEffect(() => {
    let sortedTrips = [...trips];
    if (isEarly){
      sortedTrips = sortedTrips.sort((a: TripCardSearch, b: TripCardSearch) =>
        new Date(a.dateTimeTrip) < new Date(b.dateTimeTrip)
          ? -1
          : 1);
    }
    else if (isLate){
      sortedTrips = sortedTrips.sort((a: TripCardSearch, b: TripCardSearch) =>
        new Date(a.dateTimeTrip) > new Date(b.dateTimeTrip)
          ? -1
          : 1);
    }
    else if (isCheap) {
      sortedTrips = sortedTrips.sort((a: TripCardSearch, b: TripCardSearch) =>
        a.price - b.price);
    }
    setTrips(sortedTrips);
  }, [isEarly, isLate, isCheap]);

  const handleTripsReceived = (receivedTrips: TripCardSearch[]) => {
    setIsEarly(true);
    setIsLate(false);
    setIsCheap(false);
    setTrips(receivedTrips.sort((a: TripCardSearch, b: TripCardSearch) =>
      new Date(a.dateTimeTrip) < new Date(b.dateTimeTrip)
        ? -1
        : 1));
  };

  return (
    <div className={classes.container}>
      <Helmet>
        <style>{'main { background-color: hsla(200, 20%, 97%, 1); }'}</style>
      </Helmet>
      <SearchBlablacar
        onTripsReceived={handleTripsReceived}
        isSmoking={isSmoking}
        isAnimal={isAnimal}
        isMusic={isMusic}
        isNotification={isNotification}
      ></SearchBlablacar>
      <div className={classes.containerCardsFilter}>
        <div className={classes.left}>
          <SearchFilter
            isEarly={isEarly}
            setIsEarly={setIsEarly}
            isCheap={isCheap}
            setIsCheap={setIsCheap}
            isLate={isLate}
            setIsLate={setIsLate}
            isSmoking={isSmoking}
            setIsSmoking={setIsSmoking}
            isAnimal={isAnimal}
            setIsAnimal={setIsAnimal}
            isMusic={isMusic}
            setIsMusic={setIsMusic}
          ></SearchFilter>
        </div>
        <div className={classes.right}>
          <CardsContainer
            trips={trips}
          ></CardsContainer>
          {trips.length > 0
            ?
            <div className={classes.buttonWrapper}>
              <Button
                disabled={isNotification}
                onClick={setIsNotification}
              >
                Создать уведомление о поездке
              </Button>
              {isNotification
                ?
                <span className={classes.notification}>Уведомление придёт вам на почту</span>
                : ""
              }
            </div>
            : ""
          }
      </div>
      </div>

    </div>
  );
}

export default SearchPage;