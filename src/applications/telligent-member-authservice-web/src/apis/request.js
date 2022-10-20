import axios from "axios";
import { config } from "../config.js"
import { getAuthUserAsync, logout } from '../auth/authService';
const request = axios.create({
    baseURL: config.api
});

export default request;