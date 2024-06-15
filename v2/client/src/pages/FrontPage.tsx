import AuthFooter from '../components/AuthFooter'
import Logo from '../components/Logo'

import '../styles/FrontPage.scss'

function FrontPage() {
    return (
        <div>
            <div className='centered-container'>
                <Logo/>
                <div className='text-container'>
                    <p className='text-bit'></p>
                    <p className='text-center'></p>
                </div>
            </div>
            
            <AuthFooter />
        </div>
    )
}

export default FrontPage