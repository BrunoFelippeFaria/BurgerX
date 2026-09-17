import axios from "axios";

const api = axios.create({
    baseURL: 'http://localhost:5064/',
    withCredentials: true
})

export { api } 