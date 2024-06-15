import Logo from "../components/Logo";
import '../styles/NotFound.scss';

function NotFound() {
    return (
        <div className="centered-container">
            <Logo/>
            <h1>404</h1>
            <p>Could not find the page you are looking for :/</p>
        </div>
    );
}

export default NotFound;