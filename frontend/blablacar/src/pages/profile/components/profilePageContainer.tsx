import React from "react";
import classes from "./styles/profile.module.css";
import { useProfile } from "../../../hooks/profile/useProfile";
import Avatar from "react-avatar";
import { getProfileName } from "../../../hooks/profile/profile";

/**
 * Компонент для вывода чего-то связанного с пользователем: данные, чаты и т.д.
 * @param children компоненты с данными, чатами и т.д.
 */
function ProfilePageContainer({ children }: { children: React.ReactNode }) {
  const [profile, _] = useProfile();
  if (profile == null) return <></>;
  return (
    <div className={classes.profilePageContainer}>
      <div className={classes.profileHeader}>
        <Avatar
          name={getProfileName(profile)}
          size="150"
          className={classes.profileAvatar}
          src={profile.avatar ? `data:image;base64,${profile.avatar}` : undefined}
        ></Avatar>
        <div className={classes.profileInfoWrapper}>
          <h2 className={classes.profileName}>{getProfileName(profile)}</h2>
          <h4 className={classes.profileEmail}>{profile.email}</h4>
        </div>
      </div>
      {children}
    </div>
  );
}

export default ProfilePageContainer;
