import NotFound from "../components/general/responses/notFound/notFound.tsx";
import InternalServerError from "../components/general/responses/internalServerError/internalServerError.tsx";
import BadRequest from "../components/general/responses/badRequest/badRequest.tsx";

/**
 * Компонента для обработки ошибок
 * @param statusCode - статус код
 * @param message - сообщение ошибки
 */
function HandlerErrors(
  {
    statusCode,
    message
  } : {
    statusCode: number,
    message?: string
  }) {

  if (statusCode == 500) return <InternalServerError></InternalServerError>

  if (statusCode == 404) return <NotFound></NotFound>;

  if (statusCode == 400) return <BadRequest></BadRequest>
}

export default HandlerErrors;