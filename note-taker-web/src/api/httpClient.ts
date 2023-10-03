import axios from 'axios';

const httpClient = axios.create({
    baseURL: import.meta.env.VITE_NOTE_TAKER_API_BASE_URL,
    headers: {
        "Content-Type": "application/json",
    }
});

export default httpClient;