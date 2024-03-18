import axios, { AxiosResponse } from 'axios';
import { handleError } from "../helperFunctions/errorHandlingFunctions";
import { Translator } from '../apiTypes/Translator';

export const getTranslators = async () : Promise<Translator[] | null> => {
  try {
    const response = await axios.get('/api/v1/TranslatorsManagement/GetTranslators');
    return response.data;
  }
  catch(error) {
    handleError(error, "Error getting translators list from API");
    return null;
  }
}

export const addTranslator = async (translator: Translator) : Promise<AxiosResponse | null> => {
  try {
    const response = await axios.post('/api/v1/TranslatorsManagement', translator)
    return response.data;
  }
  catch(error) {
    handleError(error, "Error create a new translator through API");
    return null;
  }
}