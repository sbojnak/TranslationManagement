import toast from "react-hot-toast";

export const handleError = (error: unknown, errorLogMessage: string) => {
  toast.error("Error: " + errorLogMessage);
  if (error instanceof Error){
    console.error(errorLogMessage, error.name, error.message);
    console.error(error.name);
    console.error(error.message);
    throw new Error(error.message + " " + errorLogMessage);
  }
  else {
    console.error("Unknown error type", errorLogMessage, error);
    console.error(errorLogMessage);
    console.error(error);
    throw new Error(errorLogMessage);
  }
}