import apiClient from "./apiClient";

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

const authService: AuthService = {
    authenticate: authenticate,
    handleAuthorization: handleAuthorization,
    getAccessToken: getAccessToken,
    clearAccessToken: clearAccessToken,
}
export default authService;