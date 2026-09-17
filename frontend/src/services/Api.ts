import axios from "axios";

const api = axios.create({
    baseURL: 'http://localhost:5064/api',
    withCredentials: true
})

export { api } 