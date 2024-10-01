import {useEffect, useState} from "react";
import {Chat} from "../../../hooks/types/Chat.ts";
import {api} from "../../../config/axios.ts";
import classes from "./styles/chats.module.css"
import Avatar from "react-avatar";
import ArrowIcon from "../../../assets/trip/arrow.svg";
import {useNavigate} from "react-router-dom";

/**
 * Компонент с чатами пользователя в профиле
 */
function Chats(){

  const [chats, setChats] = useState<Array<Chat>>([])

  const navigate = useNavigate();

  useEffect(() => {
    api
      .get("chats/getChats")
      .then(response => {
        setChats(response.data.chats);
        console.log(response.data);
      })
      .catch(error => {
        console.log(error);
      })
  }, []);

  return (
    <div className={classes.chatsWrapper}>
      <h1>Ваши чаты</h1>
      <ul>
        {chats.map((chat, index) => (
          <li key={index}>
            <div className={classes.infoDriver} onClick={() => navigate(`/chats/${chat.buddyId}`)}>
              <div className={classes.avatarWithArrow}>
                <Avatar
                  name={chat.buddyName}
                  size="50"
                  className={classes.profileAvatar}
                  src={chat.buddyAvatar ? `data:image;base64,${chat.buddyAvatar}` : undefined}
                ></Avatar>
                {chat.buddyName}
              </div>
              <div className={classes.driverName}>
                <img className={classes.arrowIcon} src={ArrowIcon}/>
              </div>
            </div>
            <hr className={classes.spliter}/>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Chats;