import { useEffect, useState } from "react";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
  HttpTransportType,
} from "@microsoft/signalr";
import LoginService from "../../services/loginService.tsx";
import { useProfile } from "../profile/useProfile.ts";


/**
 * Компонент для работы с SignalR
 */
export default function useSignalR(url: string) {
  const [profile, _] = useProfile();
  const [connection, setConnection] = useState<HubConnection | undefined>(
    undefined
  );

  useEffect(() => {
    let canceled = false;
    const connection = new HubConnectionBuilder()
      .withUrl(url, {
        transport: HttpTransportType.LongPolling,
        accessTokenFactory: () => LoginService.getAccessToken(),
        headers: {
          Authorization: `Bearer ${LoginService.getAccessToken()}`
        },
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    connection
      .start()
      .then(() => {
        if (!canceled) {
          console.log(1);
          setConnection(connection);
        }
      })
      .catch((error) => {
        console.log("signal error", error);
      });

    connection.onclose((error) => {
      if (canceled) {
        return;
      }
      console.log("signal closed");
      setConnection(undefined);
    });

    connection.onreconnecting((error) => {
      if (canceled) {
        return;
      }
      console.log("signal reconnecting");
      setConnection(undefined);
    });

    connection.onreconnected((error) => {
      if (canceled) {
        return;
      }
      console.log("signal reconnected");
      setConnection(connection);
    });

    // Clean up the connection when the component unmounts
    return () => {
      canceled = true;
      connection.stop();
    };
  }, [profile]);

  return { connection };
}
