import axios from "axios";

const HTTPService = {

    get: async (path) => {
        const response = await axios.get(path);
        return response.data;
    },

    post: async (path, data) => {
        const response = await axios.post(path, data);
        return response.data;
    },

    put: async (path, data) => {
        const response = await axios.put(path, data);
        return response.data;
    },

    del: async (path) => {
        const response = await axios.delete(path);
        return response.data;
    }
};

export default HTTPService;
