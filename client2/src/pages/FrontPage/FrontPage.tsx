import AuthFooter from '../../components/AuthFooter.tsx'
import Logo from '../../components/Logo.tsx'

import '../../styles/FrontPage.scss'

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