import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import classes from "../styles/profile.module.css";
import Dialog from "@mui/material/Dialog";
import {api} from "../../../../config/axios.ts";
import Radio from "../../../../components/general/radio/radio";
import Button from "../../../../components/general/button/button";
import {TypePreference} from "../info.tsx";
import {Slide} from "@mui/material";
import {TransitionProps} from "@mui/material/transitions";
import React from "react";
import CommunicationIcon from "../../../../assets/profile/communication.svg";
import NoCommunicationIcon from "../../../../assets/profile/noCommunication.svg";
import SmokingIcon from "../../../../assets/profile/smoking.svg";
import NoSmokingIcon from "../../../../assets/profile/noSmoking.svg";
import MusicIcon from "../../../../assets/profile/music.svg";
import NoMusicIcon from "../../../../assets/profile/noMusic.svg";
import AnimalIcon from "../../../../assets/profile/animal.svg";
import NoAnimalIcon from "../../../../assets/profile/noAnimal.svg";

const Transition = React.memo(React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement;
  },
  ref: React.Ref<unknown>,
) {
  return <Slide direction="up" ref={ref} {...props} />;
}));

/**
 * Компонент окна для выбора предпочтений
 * @param props - свойства окна
 */
function WindowPreference(props : {
  modalOpen: boolean,
  setModalOpen: (modalOpen1: boolean) => void,
  title: string,
  preference: string,
  setPreference: (pref: string) => void
  mapper: any,
  allPreferences: {[index: string]:any},
}) {
  const save = () => {

    api
      .patch("account/UpdateUserInfo",{
        preferencesTalk:
          props.title === "Разговорчивость"
            ? Number(props.preference) : props.allPreferences[TypePreference.Talk].preference,
        preferencesSmoking:
          props.title === "Курение"
            ? Number(props.preference) : props.allPreferences[TypePreference.Smoking].preference,
        preferencesMusic:
          props.title === "Музыка"
            ? Number(props.preference) : props.allPreferences[TypePreference.Music].preference,
        preferencesAnimal:
          props.title === "Животные"
            ? Number(props.preference) : props.allPreferences[TypePreference.Animal].preference,
      });
  }

  const updatedPreference = (str: string) => {
    props.setPreference(str);
  }

  return (
    <Dialog
      fullScreen
      open={props.modalOpen}
      onClose={() => props.setModalOpen(false)}
      PaperProps={{
        component: "form",
        onSubmit: save,
      }}
      TransitionComponent={Transition}
    >
      <DialogTitle
        style={{margin:"10% auto 5% auto", font: "inherit", fontWeight: "500", fontSize: "40px"}}
      >{props.title}</DialogTitle>
      <DialogContent>
        <div className={classes.dialogContent}>
          <ul>
            {["0", "1", "2"].map(str => (
              <li key={str}>
                <button type="button" className={classes.dialogButton} onClick={() => updatedPreference(str)}>
                  {props.title === "Разговорчивость"
                    ? (str === "2"
                      ? <img height={"30px"} src={NoCommunicationIcon}></img>
                      : <img height={"30px"} src={CommunicationIcon}></img>)
                  : (props.title === "Курение"
                    ? (str === "2"
                      ? <img height={"30px"} src={NoSmokingIcon}></img>
                      : <img height={"30px"} src={SmokingIcon}></img>)
                  : (props.title === "Музыка"
                    ? (str === "2"
                      ? <img height={"30px"} src={NoMusicIcon}></img>
                      : <img height={"30px"} src={MusicIcon}></img>)
                  : (props.title === "Животные"
                      ? (str === "2"
                        ? <img height={"30px"} src={NoAnimalIcon}></img>
                        : <img height={"30px"} src={AnimalIcon}></img>)
                      : "")
                      )
                    )
                  }
                  <p className={classes.dialogPreference}>{props.mapper[str]}</p>
                  <Radio
                    checked={props.preference == str ? "t" : ""}
                    onChange={() => updatedPreference(str)}
                  ></Radio>
                </button>
              </li>
            ))}
          </ul>
        </div>
        <div style={{display: "flex", justifyContent: "center"}}>
          <Button type="submit">Сохранить</Button>
        </div>
      </DialogContent>

    </Dialog>
  )
}

export default WindowPreference;