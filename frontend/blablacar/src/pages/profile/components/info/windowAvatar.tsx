import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import {useState} from "react";
import classes from "../styles/profile.module.css";
import DialogActions from "@mui/material/DialogActions";
import Dialog from "@mui/material/Dialog";
import {api} from "../../../../config/axios.ts";
import ProfileService from "../../../../services/profileService.tsx";
import {Errors} from "../types/errors.tsx";
import DropzoneBlablacar from "../../../../components/general/dropzone/dropzoneBlablacar.tsx";

function WindowAvatar({
    errors,
    setErrors,
    avatarChangeModalOpen,
    setAvatarChangeModalOpen
  } : {
    errors: Errors,
    setErrors: (error: Errors) => void,
    avatarChangeModalOpen: boolean,
    setAvatarChangeModalOpen: (avatarChangeModalOpen1: boolean) => void
}) {

  const [newAvatar, setNewAvatar] = useState<string | Blob>("");

  const saveAvatar = (e: any) => {
    e.preventDefault();
    setErrors({
      firstName: null,
      lastName: null,
      avatar: null,
    });
    const formData = new FormData();
    formData.append('request', newAvatar);
    api
      .patch("account/UpdateAvatar", formData,
        {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        })
      .then((_) => {
        setAvatarChangeModalOpen(false);
        ProfileService.fetchProfile();
      })
      .catch((error) => {
        if (error.response.status == 400) {
          if (error.response.data.errors.AvatarHref) {
            setErrors({ ...errors, avatar: "Введите корректный URL." });
          }
        }
      });
  };

  return (
    <Dialog
      open={avatarChangeModalOpen}
      onClose={() => setAvatarChangeModalOpen(false)}
      PaperProps={{
        component: "form",
        style: {minWidth: "800px"},
        onSubmit: saveAvatar,
      }}
    >
      <DialogTitle>Загрузить аватар</DialogTitle>
      <DialogContent>
        <DialogContentText>Введите ссылку на новый аватар</DialogContentText>
        <DropzoneBlablacar
          onDrop={(file) => setNewAvatar(file[0])}
        >
        </DropzoneBlablacar>
        {errors.avatar ? (
          <div className={classes.profileInfoError}>{errors.avatar}</div>
        ) : (
          <></>
        )}
      </DialogContent>
      <DialogActions>
        <button type="submit" className={classes.avatarChangeDialogButton}>
          сохранить
        </button>
      </DialogActions>
    </Dialog>
  )
}

export default WindowAvatar;