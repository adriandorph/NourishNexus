import global from '../global.ts';
import NNButton from './NNButton.tsx';
import { useNavigate } from 'react-router-dom';
import '../styles/AuthFooter.scss';



function AuthFooter() {
    const navigate = useNavigate();
  return (
    <div className='button-bar'>
        <NNButton text='Log in' textColor='black' onClick={() => {navigate('/authenticate')}} color={global.primaryColor} sizePX={20}/>
        <NNButton text='Sign up' textColor='black' onClick={() => {navigate('/signup');}} color={global.accentColor} sizePX={20}/>
    </div>
  );
}

export default AuthFooter;