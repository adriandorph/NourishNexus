import './styles/app.scss'
import FrontPage from './pages/FrontPage'
import LoginPage from './pages/LoginPage'
import SignupPage from './pages/SignupPage'
import NotFound from './pages/NotFound'
import DiscoverPage from './pages/DiscoverPage'
import NiceTry from './pages/NiceTry'

import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import ProfilePage from './pages/ProfilePage'

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" Component={FrontPage} />
                <Route path="/authenticate" Component={LoginPage} />
                <Route path="/signup" Component={SignupPage} />
                <Route path="/discover" Component={DiscoverPage} />
                <Route path="/profile" Component={ProfilePage} />
                <Route path="/nicetry" Component={NiceTry} />

                <Route path="*" Component={NotFound} />
            </Routes>
        </Router>
    );
}

export default App;
