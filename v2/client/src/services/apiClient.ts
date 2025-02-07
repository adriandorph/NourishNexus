import axios, { AxiosResponse } from 'axios';
import authService from './authService.ts';

export interface ApiClient {
    getNoAuth: (url: string) => Promise<AxiosResponse<any> | undefined>;
    postNoAuth: (url: string, data: any) => Promise<AxiosResponse<any> | undefined>;
    get: (url: string) => Promise<AxiosResponse<any> | undefined>;
    put: (url: string, data: any) => Promise<AxiosResponse<any> | undefined>;
    delete: (url: string) => Promise<AxiosResponse<any> | undefined>;
    post: (url: string, data: any) => Promise<AxiosResponse<any> | undefined>;
}

async function apiGetNoAuth(url: string): Promise<AxiosResponse<any> | undefined> {
    try {
        return await axios.get(url, {
            validateStatus: (_) => true // Accept all status codes
        });
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            return error.response;
        }
        console.error('API Get No Auth Error:', error);
        return undefined;
    }
}

async function apiPostNoAuth(url: string, data: any): Promise<AxiosResponse<any> | undefined> {
    try {
        return await axios.post(url, data, {
            validateStatus: (_) => true // Accept all status codes
        });
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            return error.response;
        }
        console.error('API Post No Auth Error:', error);
        return undefined;
    }
}

async function apiGet(url: string): Promise<AxiosResponse<any> | undefined> {
    if (!authService.handleAuthorization()) return undefined;

    if (typeof window !== 'undefined') {
        try {
            return await axios.get(url, {
                headers: { Authorization: `Bearer ${authService.getAccessToken()}` },
                validateStatus: (_) => true // Accept all status codes
            });
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                return error.response;
            }
            console.error('API Get Error:', error);
            return undefined;
        }
    }

    return undefined;
}

async function apiPost(url: string, data: any): Promise<AxiosResponse<any> | undefined> {
    if (!authService.handleAuthorization()) return undefined;

    if (typeof window !== 'undefined') {
        try {
            return await axios.post(url, data, {
                headers: { Authorization: `Bearer ${authService.getAccessToken()}` },
                validateStatus: (_) => true // Accept all status codes
            });
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                return error.response;
            }
            console.error('API Post Error:', error);
            return undefined;
        }
    }

    return undefined;
}

async function apiPut(url: string, data: any): Promise<AxiosResponse<any> | undefined> {
    if (!authService.handleAuthorization()) return undefined;

    if (typeof window !== 'undefined') {
        try {
            return await axios.put(url, data, {
                headers: { Authorization: `Bearer ${authService.getAccessToken()}` },
                validateStatus: (_) => true // Accept all status codes
            });
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                return error.response;
            }
            console.error('API Put Error:', error);
            return undefined;
        }
    }

    return undefined;
}

async function apiDelete(url: string): Promise<AxiosResponse<any> | undefined> {
    if (!authService.handleAuthorization()) return undefined;

    if (typeof window !== 'undefined') {
        try {
            return await axios.delete(url, {
                headers: { Authorization: `Bearer ${authService.getAccessToken()}` },
                validateStatus: (_) => true // Accept all status codes
            });
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                return error.response;
            }
            console.error('API Delete Error:', error);
            return undefined;
        }
    }

    return undefined;
}

const apiClient: ApiClient = {
    getNoAuth: apiGetNoAuth,
    postNoAuth: apiPostNoAuth,
    get: apiGet,
    post: apiPost,
    put: apiPut,
    delete: apiDelete,
};

export default apiClient;