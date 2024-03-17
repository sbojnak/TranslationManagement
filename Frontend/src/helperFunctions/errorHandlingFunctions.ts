export const handleError = (error: unknown, errorLogMessage: string) => {
  if (error instanceof Error){
    console.error(errorLogMessage, error.name, error.message);
    throw new Error(error.message + " " + errorLogMessage);
  }
  else {
    console.error("Unknown error type", errorLogMessage, error);
    throw new Error(errorLogMessage);
  }
}