import axios, { AxiosResponse } from 'axios';
import { handleError } from "../helperFunctions/errorHandlingFunctions";
import { Translator } from '../apiTypes/Translator';

export const getTranslators = async () : Promise<Translator[] | null> => {
  try {
    const response = await axios.get('/api/TranslatorsManagement/GetTranslators');
    console.log(response);
    return response.data;
  }
  catch(error) {
    handleError(error, "Error getting translators list from API");
    return null;
  }
}

export const addTranslator = async (translator: Translator) : Promise<AxiosResponse | null> => {
  try {
    const response = await axios.post('/api/TranslatorsManagement/AddTranslator', translator)
    return response.data;
  }
  catch(error) {
    handleError(error, "Error create a new translator through API");
    return null;
  }
}

export const updateTranslatorStatus = async (translatorId: string, newStatus: string) : Promise<AxiosResponse | null> => {
  try {
    const response = await axios.put('/api/TranslatorsManagement/UpdateTranslatorStatus', 
    {
      translatorId,
      newStatus
    });
    return response.data;
  }
  catch(error) {
    handleError(error, "Error updating translator status through API");
    return null;
  }
}