import { Link } from "react-router-dom";
import Logo from "../../assets/blablacar.svg";
import classes from "./header.module.css";
import { useProfile } from "../../hooks/profile/useProfile";
import { getProfileName } from "../../hooks/profile/profile";
import Avatar from "react-avatar";
import SearchIcon from "../../assets/header/search.svg"
import PublishTripIcon from "../../assets/header/publishTrip.svg"

/**
 * Компонент header-а
 */
function Header() {
  const [profile, _] = useProfile();

  return (
    <header className={classes.header}>
      <div className={classes.leftWrapper}>
        <Link to="/" className={classes.logoContainer}>
          <img src={Logo}></img>
          {/*<h1 className={classes.logoTitle}>Blablacar</h1>*/}
        </Link>
        <div className={classes.links}>

          <Link to="/search" className={classes.link}>
            <img style={{height:"25px", marginRight:"10px"}} src={SearchIcon}></img>
            Искать
          </Link>
          <Link to="/publishTrip" className={classes.link}>
            <img style={{height: "25px", marginRight: "10px"}} src={PublishTripIcon}></img>
            Опубликовать поездку
          </Link>
        </div>
      </div>
      <div className={classes.rightWrapper}>
        {profile == null ? (
          <Link to="/login" className={classes.profileContainer}>
            Войти
          </Link>
        ) : (
          <Link to="/profile" className={classes.profileContainer}>
            <p className={classes.profileName}>{getProfileName(profile)}</p>
            <Avatar
              name={getProfileName(profile)}
              size="50"
              className={classes.profileAvatar}
              src={profile.avatar ? `data:image;base64,${profile.avatar}` : undefined}
            ></Avatar>
          </Link>
        )}
      </div>
    </header>
  );
}

export default Header;
