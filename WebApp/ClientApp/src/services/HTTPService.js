import axios from "axios";

export const axiosInstance = axios.create();

axiosInstance.interceptors.response.use(
    function (response) {
        return response;
    },
    function (error) {
        return Promise.reject(error);
    }
);

const HTTPService = {

    get: async (path) => {
        const response = await axiosInstance.get(path);
        return response.data;
    },

    post: async (path, data) => {
        const response = await axiosInstance.post(path, data);
        return response.data;
    },

    put: async (path, data) => {
        const response = await axiosInstance.put(path, data);
        return response.data;
    },

    del: async (path) => {
        const response = await axiosInstance.delete(path);
        return response.data;
    }
};

export default HTTPService;
