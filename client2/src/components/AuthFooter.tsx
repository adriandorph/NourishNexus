import global from '../global.ts';
import NNButton from './NNButton.tsx';
import '../styles/AuthFooter.scss';

function AuthFooter() {
  return (
    <div className='button-bar'>
        <NNButton text='Log in' textColor='black' onClick={() => { }} color={global.primaryColor} sizePX={20}/>
        <NNButton text='Sign up' textColor='black' onClick={() => { }} color={global.accentColor} sizePX={20}/>
    </div>
  );
}

export default AuthFooter;