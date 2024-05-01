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

    const emailPlaceholder = 'user@nourishnexus.yum'
    const passwordPlaceholder = 'N0t4S1mpleP@ssw0rd!'

    async function handleLogin() {
        if (email === emailPlaceholder && password === passwordPlaceholder) {
            navigate('/nicetry')
            return
        } else if (await authService.authenticate(email, password)) {
            navigate('/discover');
        }
    }

    return (
        <div className='centered-container'>
            <Logo/>
            <div className='input-fields'>
                <InputField 
                    placeholder='user@nourishnexus.yum' 
                    value={email} 
                    onChange={(e: any)=>setEmail(e.target.value)}
                    title='E-mail'
                    type='email' />
                <InputField 
                    placeholder='N0t4S1mpleP@ssw0rd!' 
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