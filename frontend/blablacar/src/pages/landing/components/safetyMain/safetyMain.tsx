import classes from "./styles/safetyMain.module.css"

/**
 * Компонент с информацией о безопасности блаблакара
 */
function SafetyMain() {
  return (
    <div className={classes.safetyWrapperWrapper}>
      <div className={classes.safetyWrapper}>
        <div>
          <img height="377px" width="496px"
               src={"https://cdn.blablacar.com/kairos/assets/images/phishing-b200bc23cc51c0950d45.svg"}/>
        </div>
        <div className={classes.safetyInfoWrapper}>
          <h2 className={classes.safetyTitle}>Ваша безопасность для нас самое главное</h2>
          <p>
            Мы в BlaBlaCar прилагаем много усилий
            для безопасности нашей платформы.
            Но иногда махинации случаются, и мы
            хотим, чтобы вы знали о способах
            защиты и о том, куда о таких случаях сообщать.
            Чтобы обезопасить себя, следуйте нашим советам.
          </p>
        </div>
      </div>
    </div>
  )
}

export default SafetyMain;