import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import 'react-toastify/dist/ReactToastify.css';
import Students from './pages/Students';
function App() {
    return (
        <Router>
            <Routes>
                <Route exact path='/' element={<Students />} />
            </Routes>
        </Router>
    )
}

export default App;
