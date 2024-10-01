import {useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {UserInfo} from "../../hooks/types/UserInfo.ts";
import {api} from "../../config/axios.ts";
import Loader from "../../components/general/loader/loader.tsx";
import HandlerErrors from "../../config/handlerErrors.tsx";
import classes from "./styles/userPage.module.css";
import Avatar from "react-avatar";
import ConnectIcon from "../../assets/trip/connect.svg";
import SmokingIcon from "../../assets/profile/smoking.svg";
import {MapperAnimal, MapperMusic, MapperSmoking, MapperTalk} from "../profile/components/info/mapperPreferences.tsx";
import AnimalIcon from "../../assets/profile/animal.svg";
import MusicIcon from "../../assets/profile/music.svg";
import TalkIcon from "../../assets/profile/communication.svg";
import {differenceInYears, format} from "date-fns";
import ruLocale from "date-fns/locale/ru";

/**
 * Страница пользователя
 */
function UserPage() {
  const { userId } = useParams();

  const navigate = useNavigate();

  const [isLoading, setIsLoading] = useState(true);

  const [user, setUser] = useState<UserInfo>();

  const [statusCode, setStatusCode] = useState<number>();

  useEffect(() => {
    api
      .get(`users/${userId}`)
      .then(response => {
        setIsLoading(false);
        setUser(response.data);
        setStatusCode(200);
      })
      .catch(error => {
        setStatusCode(error.response.status)
      })
  }, []);

  const getYears = () => {
    if (!user) return ""
    const birthday = new Date(user?.birthday);

    const age = differenceInYears(new Date(), birthday);
    let strAge = "";
    if (age % 100 > 10 && age % 100 < 20) {
      strAge = 'лет';
    } else if (age % 10 === 1) {
      strAge = 'год';
    } else if (age % 10 > 1 && age % 10 < 5) {
      strAge = 'года';
    } else {
      strAge = 'лет';
    }

    return `${age} ${strAge}`;
  }

  const getRegistration = () => {
    if (!user) return ""
    const dateRegistration = new Date(user?.dateRegistration);

    return format(dateRegistration, 'LLLL yyyy', {
      // eslint-disable-next-line @typescript-eslint/ban-ts-comment
      // @ts-expect-error
      locale: ruLocale });
  }

  if (isLoading) return (<Loader></Loader>)

  if (statusCode && statusCode % 400 < 200)
    return <HandlerErrors statusCode={statusCode}></HandlerErrors>

  return (
    <div className={classes.infoUserWrapperWrapper}>
      <div className={classes.infoUserWrapper}>
        <div className={classes.infoUser}>
          <div className={classes.userNameWrapper}>
            <div className={classes.userName}>
              {user?.firstName}
            </div>
            <div className={classes.age}>
              {getYears()}
            </div>
          </div>
          <div className={classes.avatarWithArrow}>
            <Avatar
              name={user?.firstName}
              size="96"
              className={classes.profileAvatar}
              src={user?.avatar ? `data:image;base64,${user?.avatar}` : undefined}
            ></Avatar>
          </div>
        </div>
        <hr className={classes.spliter}/>
        <div className={classes.connectUser} onClick={() => navigate(`/chats/${user?.id}`)}>
          <img height={"24px"} src={ConnectIcon}/>
          <p>Связаться с {user?.firstName}</p>
        </div>
        <hr className={classes.spliter}/>
        <div className={classes.preferencesWrapper}>
          <div className={classes.titlePreferences}>
            {user?.firstName} о себе
          </div>
          <div className={classes.preferences}>
            <img height={"24px"} src={SmokingIcon}/>
            <p>{MapperSmoking[user!.preferencesSmoking]}</p>
          </div>
          <div className={classes.preferences}>
            <img height={"24px"} src={AnimalIcon}/>
            <p>{MapperAnimal[user!.preferencesAnimal]}</p>
          </div>
          <div className={classes.preferences}>
            <img height={"24px"} src={MusicIcon}/>
            <p>{MapperMusic[user!.preferencesMusic]}</p>
          </div>
          <div className={classes.preferences}>
            <img height={"24px"} src={TalkIcon}/>
            <p>{MapperTalk[user!.preferencesTalk]}</p>
          </div>
        </div>
        <hr className={classes.spliter}/>
        <div className={classes.countTripsDateRegistrationWrapper}>
          <div className={classes.preferences}>
            <p>{user?.countTrips} опубликованных поездок</p>
          </div>
          <div className={classes.preferences}>
            <p>Дата регистрации: {getRegistration()}</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default UserPage;