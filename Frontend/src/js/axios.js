import axios from "axios";
import { rootApi } from "./config";
const axiosInstance = axios.create({
    baseURL: rootApi
})
export default axiosInstance