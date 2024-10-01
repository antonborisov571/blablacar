import ProfileInfo from "./components/info";
import ProfilePageContainer from "./components/profilePageContainer";
import ProfileSidebar from "./components/sidebar/profileSidebar";
import { useProfile } from "../../hooks/profile/useProfile";
import { Navigate, Route, Routes } from "react-router-dom";
import Chats from "./components/chats.tsx";
import Trips from "./components/trips.tsx";

/**
 * Страницы профиля пользователя
 */
function ProfilePage() {
  const [profile, loading] = useProfile();
  if (profile == null && !loading) return <Navigate to="/login"></Navigate>;
  return (
    <>
      <ProfileSidebar></ProfileSidebar>
      <ProfilePageContainer>
        <Routes>
          <Route path="/" element={<ProfileInfo />} />
          <Route path="/chats" element={<Chats />} />
          <Route path="/trips" element={<Trips />} />
          <Route path="*" element={<Navigate to="/profile" />} />
        </Routes>
      </ProfilePageContainer>
    </>
  );
}

export default ProfilePage;
