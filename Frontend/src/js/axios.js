import axios from "axios";
import { rootApi } from "../config/configApi";
const axiosInstance = axios.create({
    baseURL: rootApi
})
export default axiosInstance