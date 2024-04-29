import apiClient from "./apiClient";
import { useNavigate } from "react-router-dom";
import Cookies from 'js-cookie';

export interface AuthService {
    authenticate: (email: string, password: string) => Promise<boolean>;
    handleAuthorization: () => boolean;
    getAccessToken: () => string | undefined;
    clearAccessToken: () => void;
}

async function authenticate(email: string, password: string): Promise<boolean> {
    // Call the login endpoint and store the token in local storage
    return await apiClient.postNoAuth(`${import.meta.env.VITE_API_URL}/auth`, { email: email, password: password }).then((res) => {
        if (res.status !== 200) {
            console.log('Login failed');
            return false;
        } else {
            console.log('Login successful');
            setAccessToken(res.data.access_token);
            return true;
        }
    });
}

function handleAuthorization(): boolean {
    const token = getAccessToken();
    if (token) {
        return true;
    } else {
        const navigate = useNavigate();
        navigate('/login');
        return false;
    }
}

function getAccessToken(): string | undefined {
    if (typeof window !== 'undefined') {
        const local = localStorage.getItem('access_token');
        const session = Cookies.get('access_token');
        if (local) return local;
        if (session) return session;
    }
    return undefined;
}

function clearAccessToken() {
    if (typeof window !== 'undefined') {
        localStorage.clear();
        Cookies.remove('access_token');
    }
}

function setAccessToken(token: string) {
    if (typeof window !== 'undefined') {
        localStorage.setItem('access_token', token);
        Cookies.set('access_token', token);
    }
}

const authService: AuthService = {
    authenticate: authenticate,
    handleAuthorization: handleAuthorization,
    getAccessToken: getAccessToken,
    clearAccessToken: clearAccessToken,
}
export default authService;