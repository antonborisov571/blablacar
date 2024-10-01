import {useEffect, useState} from 'react';
import Header from "./components/header/header";
import { Route, Routes } from "react-router-dom";
import './App.css'
import Login from "./pages/accountManagement/login/login.tsx";
import Register from "./pages/accountManagement/register/register.tsx";
import TwoFactor from "./pages/accountManagement/twoFactorAuth/twoFactor.tsx";
import ForgotPassword from "./pages/accountManagement/forgotPassword/forgotPassword.tsx";
import ResetPassword from "./pages/accountManagement/resetPassword/resetPassword.tsx";
import ConfirmEmail from "./pages/accountManagement/confirmEmail/confirmEmail.tsx";
import { ProfileContext } from "./hooks/profile/profileContext.ts";
import { LoadingContext } from "./hooks/profile/loadingContext.ts";
import {Profile} from "./hooks/profile/profile.ts";
import LoginService from "./services/loginService.tsx";
import ProfileService from "./services/profileService.tsx";
import OauthRedirect from "./pages/oauthRedirect/oauthRedirect.tsx";
import ProfilePage from "./pages/profile/profile.tsx";
import Landing from "./pages/landing/landing.tsx";
import PublishTrip from "./pages/publishTrip/publishTrip.tsx";
import SearchPage from "./pages/search/searchPage.tsx";
import Trip from "./pages/trip/trip.tsx";
import UserPage from "./pages/user/userPage.tsx";
import Chat from "./pages/chat/chat.tsx";

/**
 * Страница в браузере
 */
function App() {
  const [profile, setProfile] = useState<Profile | null>(null);
  const [loading, setLoading] = useState(true);
  const [refreshingTokens, setRefreshingTokens] = useState(false);
  LoginService.initialize(setRefreshingTokens);
  LoginService.registerAutomaticRefresh();
  ProfileService.initialize(setProfile, setLoading);
  useEffect(() => {
    if (refreshingTokens) setLoading(true);
    else if (LoginService.checkLoggedIn()) ProfileService.fetchProfile();
    else setLoading(false);
  }, [refreshingTokens]);

  return (
    <>
      <ProfileContext.Provider value={profile}>
        <LoadingContext.Provider value={loading}>
          <Header></Header>
          <main>
            <Routes>
              <Route path="/" element={<Landing />}></Route>
              <Route path="/search" element={<SearchPage />}></Route>
              <Route path="/trips/:tripId" element={<Trip />}></Route>
              <Route path="/users/:userId" element={<UserPage />}></Route>
              <Route path="/chats/:buddyId" element={<Chat />}></Route>
              <Route path="/publishTrip" element={<PublishTrip />}></Route>
              <Route path="/register" element={<Register />}></Route>
              <Route path="/login" element={<Login />}></Route>
              <Route path="/login/2fa" element={<TwoFactor />}></Route>
              <Route
                path="/forgotPassword"
                element={<ForgotPassword />}
              ></Route>
              <Route path="/profile/*" element={<ProfilePage />}></Route>
              <Route path="/oauth-redirect" element={<OauthRedirect />}></Route>
              <Route path="/resetPassword" element={<ResetPassword />}></Route>
              <Route path="/confirmEmail" element={<ConfirmEmail />}></Route>
            </Routes>
          </main>
        </LoadingContext.Provider>
      </ProfileContext.Provider>
    </>
  )
}

export default App
