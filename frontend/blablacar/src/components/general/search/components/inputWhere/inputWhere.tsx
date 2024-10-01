import classes from "./styles/inputWhere.module.css";
import {useEffect, useState} from "react";
import {api} from "../../../../../config/axios.ts";

/**
 * Строка поиска
 * @param placeholder - placeholder
 * @param value - значение
 * @param setValue - сеттер для значение в поиске
 */
function InputWhere({
    placeholder,
    value,
    setValue
  } : {
    placeholder: string,
    value: string,
    setValue: (val: string) => void
  }) {
  const [suggestions, setSuggestions] = useState<Array<string>>([]);
  const [focused, setFocused] = useState(false);

  useEffect(() => {
    updateCities("");
  }, []);

  const updateCities = (value) => {
    api.get("searchcity/getcities?cityname=" + (value === "" ? "Москва" : value))
      .then(res => {
        if (value) {
          setSuggestions(res.data.citiesName);
          console.log(suggestions);
        } else {
          setSuggestions([]);
        }
      })
      .catch(error => {
        console.error("Error fetching cities:", error);
      });
  };

  const handleInputValueChange = (event) => {
    const value = event.target.value;
    setValue(value);
    if (value === "") {
      setSuggestions([]);
    }
    updateCities(value);
  };

  const handleSuggestionClick = (city) => {
    setValue(city);
    setSuggestions([]);
  };

  return (
    <>
      <input
        className={`${classes.whereInput}`}
        type="text"
        placeholder={placeholder}
        value={value}
        onChange={handleInputValueChange}
        onFocus={() => setFocused(true)}
        onBlur={() => setTimeout(() => {
          setFocused(false);
        }, 100)}
      />
      {suggestions.length > 0 && focused && (
        <div className={classes.suggestions}>
          {suggestions.map((city: any, index) => (
            <div
              key={index}
              className={classes.suggestion}
              onClick={() => handleSuggestionClick(city)}
            >
              {city}
            </div>
          ))}
        </div>
      )}
    </>
  )
}

export default InputWhere;