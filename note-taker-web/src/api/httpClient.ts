import axios from 'axios';

const httpClient = axios.create({
    baseURL: import.meta.env.NOTE_TAKER_API_BASE_URL,
    headers: {
        "Content-Type": "application/json",
    }
});

export default httpClient;