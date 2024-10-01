import Dropzone, {DropEvent, FileRejection, useDropzone} from "react-dropzone";
import classes from "./dropzoneBlablacar.module.css"
import DropzoneIcon from "../../../assets/input/dropzone.svg"

/**
 * DropzoneBlablacar компонент - это настраиваемый dropzone для загрузки файлов.
 * Он использует библиотеку react-dropzone для обработки загрузки файлов с помощью драг-н-броса.
 *
 * @param {Function} onDrop - Функция-обработчик, которая вызывается, когда файлы загружаются в dropzone.
 * @returns {JSX.Element} - Возвращает JSX-элемент, представляющий компонент dropzone.
 */
function DropzoneBlablacar({onDrop} : {onDrop: <T extends File>(acceptedFiles: T[], fileRejections: FileRejection[], event: DropEvent) => void}) {
  return (
    <Dropzone
      onDrop={onDrop}
      maxSize={3072000}
    >
      {({getRootProps, getInputProps, isDragAccept, isDragActive, acceptedFiles}) => (
        <div
          {...getRootProps({
            className: `dropzone ${classes.active}`,
          })}
        >
          <input {...getInputProps()} />
          <p>{isDragActive
            ? "Отпустите, чтобы загрузить файлы"
            : acceptedFiles.length == 0
              ? "Перетащите файлы сюда или выберите их"
              : "Файлы загружены"}</p>
          <img height={"100px"} src={DropzoneIcon}/>
          <aside>
            <ul>{acceptedFiles.map((file) => file.name)}</ul>
          </aside>
        </div>
      )
      }
    </Dropzone>
  );
}

export default DropzoneBlablacar;