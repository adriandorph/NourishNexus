import axios from 'axios';
import authService from './authService.ts';

export interface ApiClient {
    getNoAuth: (url: string) => Promise<any>;
    postNoAuth: (url: string, data: any) => Promise<any>;
    get: (url: string) => Promise<any>;
    put: (url: string, data: any) => Promise<any>;
    delete: (url: string) => Promise<any>;
    post: (url: string, data: any) => Promise<any>;
}


async function apiGetNoAuth(url: string) {
    return await axios.get(url);
}

async function apiPostNoAuth(url: string, data: any) {
    return await axios.post(url, data);
}

async function apiGet(url: string) {
    if (!authService.handleAuthorization()) return;

    if (typeof window !== 'undefined') {
        return await axios.get(url, { headers: { Authorization: `Bearer ${authService.getAccessToken()}` } });
    }
};

async function apiPost(url: string, data: any) {
    if (!authService.handleAuthorization()) return;

    if (typeof window !== 'undefined') {
        return await axios.post(url, data, {
            headers: { Authorization: `Bearer ${authService.getAccessToken()}` },
        });
    }
}

async function apiPut(url: string, data: any) {
    if (!authService.handleAuthorization()) return;

    if (typeof window !== 'undefined') {
        return await axios.put(url, data, {
            headers: { Authorization: `Bearer ${authService.getAccessToken()}` },
        });
    }
};

async function apiDelete(url: string) {
    if (!authService.handleAuthorization()) return;

    if (typeof window !== 'undefined') {
        return await axios.delete(url, {
            headers: { Authorization: `Bearer ${authService.getAccessToken()}` },
        });
    }
};
  
const apiClient: ApiClient = {
    getNoAuth: apiGetNoAuth,
    postNoAuth: apiPostNoAuth,
    get: apiGet,
    post: apiPost,
    put: apiPut,
    delete: apiDelete,
};
export default apiClient;

