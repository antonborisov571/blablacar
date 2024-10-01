import {api} from "../../config/axios.ts";
import {useEffect, useState} from "react";
import useSignalR from "../../hooks/chat/signalr.ts";
import {Message} from "../../hooks/types/Message.ts";
import classes from "./styles/chat.module.css"
import {format} from "date-fns";
import Avatar from "react-avatar";
import AirplaneIcon from "../../assets/chat/airplane.svg"
import {useParams} from "react-router-dom";
import Loader from "../../components/general/loader/loader.tsx";
import HandlerErrors from "../../config/handlerErrors.tsx";
import ruLocale from "date-fns/locale/ru";

/**
 * Компонент чата
 */
function Chat() {
  const { connection } = useSignalR("https://unbiased-splendid-terrapin.ngrok-free.app/chat");

  const { buddyId } = useParams();

  const [isLoading, setIsLoading] = useState(true);

  const [messages, setMessages] = useState<Array<Message>>([]);
  const [input, setInput] = useState("");

  const [ourId, setOurId] = useState<string>("");

  const [buddyName, setBuddyName] = useState<string>("");
  const [buddyAvatar, setBuddyAvatar] = useState<string>("");

  const [statusCode, setStatusCode] = useState<number>();

  let currentDate = null;

  useEffect(() => {

    api
      .get(`chats/${buddyId}`)
      .then(response => {
        console.log(response.data);
        setMessages(response.data.messages.sort((a: Message, b: Message) =>
          new Date(a.dispatch) > new Date(b.dispatch)
            ? 1
            : -1));
        setOurId(response.data.ourId);
        setBuddyName(response.data.buddyName);
        setBuddyAvatar(response.data.buddyAvatar);
        setStatusCode(200);
        setIsLoading(false);
      })
      .catch(error => {
        setStatusCode(error.data.status);
      });
  }, []);

  useEffect(() => {
    if (!connection) return;

    connection.on("ReceiveMessage", (message: Message) => {
      setMessages([...messages, message]);
      console.log("message from the server", message);
    });

    return () => connection.off("ReceiveMessage");
  }, [connection, messages]);

  const sendMessage = () => {
    if (!connection) return;

    setInput("");

    connection.send("SendMessage", {receiverId: buddyId, text: input});
  };

  if (isLoading) return (<Loader></Loader>)

  if (statusCode && statusCode % 400 < 200)
    return <HandlerErrors statusCode={statusCode}></HandlerErrors>

  return (
    <div className={classes.chatWrapperWrapper}>
      <div className={classes.buddyInfo}>
        <Avatar
          name={buddyName}
          size="70"
          className={`${classes.profileAvatar}`}
          src={buddyAvatar ? `data:image;base64,${buddyAvatar}` : undefined}
        ></Avatar>
        <h1 className={classes.title}>Чат с {buddyName}</h1>
      </div>
      <div className={classes.chatWrapper}>

        {messages.map((message, index) => {

          const messageDate = format(new Date(message.dispatch), 'dd/MM/yyyy');
          const showDate = messageDate !== currentDate;
          currentDate = messageDate;

          return (
            <>
              {showDate && (
                <div className={classes.dateSeparator}>
                  
                  {format(new Date(message.dispatch), 'd MMMM', { 
                    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
                    // @ts-expect-error
                    locale: ruLocale })}
                </div>
              )}
              {ourId == message.senderId ? (
                <div className={classes.ourMessageImage}>
                  <div className={classes.ourMessageWrapper}>
                  <span className={classes.ourMessageText}>
                    {message.text}
                  </span>
                    <div className={classes.ourMessageTime}>
                      {format(new Date(message.dispatch), 'HH:mm')}
                    </div>
                  </div>
                  <Avatar
                    name={message.senderName}
                    size="50"
                    className={`${classes.profileAvatar} ${classes.ourImage}`}
                    src={message.senderAvatar ? `data:image;base64,${message.senderAvatar}` : undefined}
                  ></Avatar>
                </div>
              ) : (
                <div className={classes.buddyMessageImage}>
                  <Avatar
                    name={message.senderName}
                    size="50"
                    className={`${classes.profileAvatar} ${classes.buddyImage}`}
                    src={message.senderAvatar ? `data:image;base64,${message.senderAvatar}` : undefined}
                  ></Avatar>
                  <div className={classes.buddyMessageWrapper}>
                  <span className={classes.buddyMessageText}>
                    {message.text}
                  </span>
                    <div className={classes.buddyMessageTime}>
                      {format(new Date(message.dispatch), 'HH:mm')}
                    </div>
                  </div>
                </div>
              )}
            </>
          );
        })}
      </div>
      <div className={classes.inputWrapperWrapper}>
        <div className={classes.inputWrapper}>
          <textarea
            placeholder={"Ваше сообщение"}
            className={classes.input}
            value={input}
            onChange={(e) => setInput(e.target.value)}
          ></textarea>
          <button
            className={input != "" ? classes.buttonSend : classes.buttonNone}
            onClick={sendMessage}
          >
            <img height={"32px"} src={AirplaneIcon}/>
          </button>
        </div>
      </div>
    </div>
  );
}

export default Chat;