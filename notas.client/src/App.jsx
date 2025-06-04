import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import CadastrarEmpresa from './Pages/CadastrarEmpresa';
import VerEmpresas from './Pages/VerEmpresas';
import CadastrarNota from './Pages/CadastrarNota';
import VerNotas from './Pages/VerNotas';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/empresas/novo" element={<CadastrarEmpresa />} />
                <Route path="/empresas" element={<VerEmpresas />} />
                <Route path="/notas/novo" element={<CadastrarNota />} />
                <Route path="/notas" element={<VerNotas />} />
            </Routes>
        </Router>
    );
}

export default App;
