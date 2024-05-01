import { useState } from "react";
import { useNavigate } from "react-router-dom";
import InputField from "../components/InputField";
import Logo from "../components/Logo";
import NNButton from "../components/NNButton";
import global from "../global";
import '../styles/LoginPage.scss'
import apiClient from "../services/apiClient";
import authService from "../services/authService";


function SignupPage() {
    const navigate = useNavigate()
    const [email, setEmail] = useState('')
    const [nickname, setNickname] = useState('')
    const [password, setPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')

    async function handleSignup() {
        console.log('email:', email);
        console.log('nickname:', nickname);
        console.log('password:', password);
        console.log('confirm password:', confirmPassword);
    
        const success = await postUser(email, nickname, password, confirmPassword);
        if (success) {
            const isAuthenticated = await authService.authenticate(email, password);
            if (isAuthenticated) {
                navigate('/profile');
            } else {
                console.log('Login failed');
            }
        }
    }

    return (
        <div className='centered-container'>
            <Logo/>
            <div className='input-fields'>
                <InputField 
                    placeholder='E-mail' 
                    value={email} 
                    onChange={(e: any)=>setEmail(e.target.value)}
                    title='E-mail'
                    type='email' />
                <InputField 
                    placeholder='Nickname' 
                    value={nickname} 
                    onChange={(e: any)=>setNickname(e.target.value)}
                    title='Nickname'
                    type='text' />
                <InputField 
                    placeholder='Password' 
                    value={password} 
                    onChange={(e: any)=>setPassword(e.target.value)}
                    title='Password'
                    type='password' />
                <InputField 
                    placeholder='Confirm Password' 
                    value={confirmPassword} 
                    onChange={(e: any)=>setConfirmPassword(e.target.value)}
                    title='Confirm Password'
                    type='password' />
            </div>
            <NNButton 
                onClick={handleSignup} 
                text='sign up' 
                color={global.primaryColor} 
                textColor='black' 
                sizePX={25} />
            <div className='sign-up'>
                Already have an account? <a href='/authenticate'>Log in here</a>
            </div>
        </div>
    );
}

async function postUser(email: string, nickname: string, password: string, confirmPassword: string): Promise<boolean> { 
    const res = await apiClient.postNoAuth(
        `${import.meta.env.VITE_API_URL}/user`, 
        { 
            email: email, 
            nickname: nickname, 
            password: password, 
            confirmPassword: confirmPassword
        }
    );
    return res.status === 200;
}



export default SignupPage;