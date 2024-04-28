import './styles/app.scss'
import FrontPage from './pages/FrontPage.tsx'
import LoginPage from './pages/LoginPage.tsx'
import SignupPage from './pages/SignupPage.tsx'
import NotFound from './pages/NotFound.tsx'

import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" Component={FrontPage} />
                <Route path="/authenticate" Component={LoginPage} />
                <Route path="/signup" Component={SignupPage} />
                <Route path="*" Component={NotFound} />
            </Routes>
        </Router>
    );
}

export default App;
