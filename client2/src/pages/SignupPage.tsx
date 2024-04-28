import { useState } from "react";
import InputField from "../components/InputField.tsx";
import Logo from "../components/Logo.tsx";
import NNButton from "../components/NNButton.tsx";
import global from "../global.ts";
import '../styles/LoginPage.scss'

function SignupPage() {
    const [email, setEmail] = useState('')
    const [nickname, setNickname] = useState('')
    const [password, setPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')

    function handleSignup() {
        console.log('email:', email)
        console.log('nickname:', nickname)
        console.log('password:', password)
        console.log('confirm password:', confirmPassword)
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

export default SignupPage;