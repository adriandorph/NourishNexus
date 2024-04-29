import Logo from '../components/Logo'
import NNButton from '../components/NNButton'
import InputField from '../components/InputField';
import global from '../global';
import { useState } from 'react';
import '../styles/LoginPage.scss'
import authService from '../services/authService';
import { useNavigate } from 'react-router';

function LoginPage() {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const navigate = useNavigate()

    async function handleLogin() {
        console.log('email:', email)
        console.log('password:', password)
        if (await authService.authenticate(email, password)) {
            navigate('/search');
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
                    placeholder='Password' 
                    value={password} 
                    onChange={(e: any)=>setPassword(e.target.value)}
                    title='Password'
                    type='password' />
            </div>
            <NNButton 
                onClick={handleLogin} 
                text='log in' 
                color={global.primaryColor} 
                textColor='black' 
                sizePX={25} />
            <div className='sign-up'>
                Don't have an account yet? <a href='/signup'>Sign up here</a>
            </div>
        </div>
    );
}

export default LoginPage;