import logo from '../assets/logo.png'

import '../styles/Logo.scss'

export interface LogoProps {
    size?: string
}

function Logo(props: LogoProps) {

    const size = props.size || 'default'

    return (
        <img className={size} src={logo} alt='logo' />
    )
}

export default Logo