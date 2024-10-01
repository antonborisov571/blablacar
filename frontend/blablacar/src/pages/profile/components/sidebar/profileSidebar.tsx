import { useEffect, useState } from "react";
import classes from "./styles/sidebar.module.css";
import BarLoader from "react-spinners/BarLoader";
import { Link, useLocation } from "react-router-dom";
import { useProfile } from "../../../../hooks/profile/useProfile";

/**
 * Вкладки
 */
type Tab = {
  /**
   * Заголовок вкладки
   */
  title: string;

  /**
   * Url вкладки
   */
  url: string;
};

/**
 * Компонент сайдбара профиля
 */
function ProfileSidebar() {
  const location = useLocation();
  const [currentTabUrl, setCurrentTabUrl] = useState("/");
  const [profile, _] = useProfile();
  const tabs: Array<Tab> = [
    { title: "Мои данные", url: "" },
    { title: "Мои чаты", url: "chats" },
    { title: "Активные поездки", url: "trips"}
  ];
  const renderBlock = (tab: Tab, index: number) => {
    if (currentTabUrl == tab.url)
      return (
        <div className={classes.module} key={index}>
          {tab.title}
        </div>
      );
    return (
      <Link className={classes.block} key={index} to={`/profile/${tab.url}`}>
        {tab.title}
      </Link>
    );
  };

  useEffect(() => {
    setCurrentTabUrl(
      location.pathname
        .replace("/profile", "")
        .replace(/^\//, "")
        .replace(/\/$/, "")
    );
  }, [location.pathname]);

  return (
    <div className={classes.sidebar}>
      <div>
        {profile != null ? (
          tabs.map(renderBlock)
        ) : (
          <div className="d-flex justify-content-center m-5">
            <BarLoader color="var(--accent-color)" width="100%"></BarLoader>
          </div>
        )}
      </div>
    </div>
  );
}

export default ProfileSidebar;
