import Logo from '../components/Logo.tsx'
import NNButton from '../components/NNButton.tsx'
import InputField from '../components/InputField.tsx';
import global from '../global.ts';
import { useState } from 'react';
import '../styles/LoginPage.scss'

function LoginPage() {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    function handleLogin() {
        console.log('email:', email)
        console.log('password:', password)
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