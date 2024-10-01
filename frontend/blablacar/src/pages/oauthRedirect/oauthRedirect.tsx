import { useEffect, useState } from "react";
import { Navigate, useSearchParams } from "react-router-dom";
import { BeatLoader } from "react-spinners";
import LoginService from "../../services/loginService";
import ProfileService from "../../services/profileService";

/**
 * Компонент для входа или регистрации через сторонние сервисы
 */
function OauthRedirect() {
  const [loginResult, setLoginResult] = useState<Boolean | undefined>();
  const [searchParams, setSearchParams] = useSearchParams();
  const accessToken = searchParams.get("accessToken");
  const refreshToken = searchParams.get("refreshToken");

  useEffect(() => {
    if (accessToken === null || refreshToken === null) return;
    LoginService.storeTokens(
      accessToken,
      1,
      refreshToken
    );
    ProfileService.fetchProfile();
    setLoginResult(true);
  }, []);

  if (!loginResult) <Navigate to={"/login"}></Navigate>
  if (loginResult != undefined) return <Navigate to="/"></Navigate>;
  return (
    <>
      <BeatLoader color="var(--accent-color)"></BeatLoader>
    </>
  );
}

export default OauthRedirect;
