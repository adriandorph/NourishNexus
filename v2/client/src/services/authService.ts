import { AuthUser } from "../types/authUser";
import apiClient from "./apiClient";

export interface AuthService {
    authenticate: (email: string, password: string) => Promise<boolean>;
    handleAuthorization: () => boolean;
    getAccessToken: () => string | undefined;
    clearAccessToken: () => void;
    getAuthenticatedUser: () => AuthUser | undefined;
}

async function authenticate(email: string, password: string): Promise<boolean> {
    // Call the login endpoint and store the token in local storage
    return await apiClient.postNoAuth(`${import.meta.env.VITE_API_URL}/auth`, { email: email, password: password }).then((res) => {
        if (!res || res.status !== 200) {
            return false;
        } else {
            setAccessToken(res.data);
            return true;
        }
    });
}

function handleAuthorization(): boolean {
    const token = getAccessToken();
    if (token) {
        return true;
    } else {
        return false;
    }
}

function getAccessToken(): string | undefined {
    if (typeof window !== 'undefined') {
        const local = localStorage.getItem('access_token');
        if (local) return local;
    }
    return undefined;
}

function clearAccessToken() {
    if (typeof window !== 'undefined') {
        localStorage.clear();
    }
}

function setAccessToken(token: string) {
    if (typeof window !== 'undefined') {
        localStorage.setItem('access_token', token);
    }
}

function getAuthenticatedUser() {
    const accessToken = getAccessToken();
    //Decode the JWT
    if (accessToken) {
        const payload = accessToken.split('.')[1];
        const decodedPayload = atob(payload);
        const parsedPayload = JSON.parse(decodedPayload);

        return { 
            emailaddress: parsedPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
            name:  parsedPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
            nameidentifier:  parsedPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
        }
    }
    return undefined;
}

const authService: AuthService = {
    authenticate: authenticate,
    handleAuthorization: handleAuthorization,
    getAccessToken: getAccessToken,
    clearAccessToken: clearAccessToken,
    getAuthenticatedUser: getAuthenticatedUser
}
export default authService;