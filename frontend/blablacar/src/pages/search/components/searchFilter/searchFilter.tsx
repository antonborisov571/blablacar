import classes from "./styles/searchFilter.module.css"
import Radio from "../../../../components/general/radio/radio.tsx";
import PriceIcon from "../../../../assets/filter/price.svg"
import TimeIcon from "../../../../assets/filter/time.svg"
import Checkbox from "../../../../components/general/checkbox/checkbox.tsx";
import SmokingIcon from "../../../../assets/profile/smoking.svg"
import AnimalIcon from "../../../../assets/profile/animal.svg"
import MusicIcon from "../../../../assets/profile/music.svg"

interface Props {
  isEarly: boolean;
  isLate: boolean;
  isCheap: boolean;
  setIsEarly: (isEarly: boolean) => void;
  setIsLate: (isLate: boolean) => void;
  setIsCheap: (isCheap: boolean) => void;
  isSmoking: boolean;
  isAnimal: boolean;
  isMusic: boolean;
  setIsSmoking: (isEarly: boolean) => void;
  setIsAnimal: (isLate: boolean) => void;
  setIsMusic: (isCheap: boolean) => void;
}

/**
 * Компонент с фильтрами
 * @param isEarly - самые ранние
 * @param isLate - самые поздние
 * @param isCheap - самые дешевые
 * @param setIsEarly - сеттер для isEarly
 * @param setIsLate - сеттер для isLate
 * @param setIsCheap - сеттер для isCheap
 * @param isSmoking - разрешено ли курение
 * @param isAnimal - разрешены ли животные
 * @param isMusic - разрешена ли музыка
 * @param setIsSmoking - сеттер для isSmoking
 * @param setIsAnimal - сеттер для isAnimal
 * @param setIsMusic - сеттер для isMusic
 */
function SearchFilter({
  isEarly, isLate, isCheap, setIsEarly, setIsLate, setIsCheap,
  isSmoking, isAnimal, isMusic, setIsSmoking, setIsAnimal, setIsMusic
} : Props){
  return (
    <div>
      <section>
        <h3 className={classes.title}>Сортировать</h3>
        <ul>
          <li>
            <div className={classes.sortItem}>
              <img src={TimeIcon}/>
              <p>Самые ранние поездки</p>
              <Radio
                checked={isEarly ? "t" : ""}
                onChange={() => {
                  setIsEarly(true)
                  setIsLate(false);
                  setIsCheap(false);
                }}
              ></Radio>
            </div>
          </li>
          <li>
            <div className={classes.sortItem}>
              <img src={TimeIcon}/>
              <p>Самые поздние поездки</p>
              <Radio
                checked={isLate ? "t" : ""}
                onChange={() => {
                  setIsEarly(false)
                  setIsLate(true);
                  setIsCheap(false);
                }}
              ></Radio>
            </div>
          </li>
          <li>
            <div className={classes.sortItem}>
              <img src={PriceIcon}/>
              <p>Самые дешевые поездки</p>
              <Radio
                checked={isCheap ? "t" : ""}
                onChange={() => {
                  setIsEarly(false)
                  setIsLate(false);
                  setIsCheap(true);
                }}
              ></Radio>
            </div>
          </li>
        </ul>
      </section>
      <hr className={classes.spliter}/>
      <section>
        <h3 className={classes.title}>Удобства</h3>
        <ul>
          <li>
            <div className={classes.sortItem}>
              <img height={24} src={SmokingIcon}/>
              <p>Можно курить</p>
              <Checkbox
                checked={isSmoking ? "t" : ""}
                onChange={() => setIsSmoking(!isSmoking)}
              ></Checkbox>
            </div>
          </li>
          <li>
            <div className={classes.sortItem}>
              <img height={24} src={AnimalIcon}/>
              <p>Можно с животными</p>
              <Checkbox
                checked={isAnimal ? "t" : ""}
                onChange={() => setIsAnimal(!isAnimal)}
              ></Checkbox>
            </div>
          </li>
          <li>
            <div className={classes.sortItem}>
              <img height={24} src={MusicIcon}/>
              <p>Можно с музыкой</p>
              <Checkbox
                checked={isMusic ? "t" : ""}
                onChange={() => setIsMusic(!isMusic)}
              ></Checkbox>
            </div>
          </li>
        </ul>
      </section>
    </div>
  );
}

export default SearchFilter;