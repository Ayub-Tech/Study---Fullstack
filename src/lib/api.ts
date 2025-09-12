import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5192", // skriv direkt istället för env
});

export default api;
