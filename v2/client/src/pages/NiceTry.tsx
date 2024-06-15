import Logo from '../components/Logo';
import NNButton from '../components/NNButton';
import global from '../global';
import '../styles/NiceTry.scss'


function NiceTry() {

    return (
    <div className="centered-container">
        <Logo />
        <div className='nice-try-container'>
            <h1>Nice Try 😂</h1>
            <NNButton 
                onClick={() => window.history.back()} 
                text={'Okay, I\'ll go back'} 
                color={global.primaryColor} 
                textColor='black' 
                sizePX={20}/>
        </div>
    </div>);
}

export default NiceTry;