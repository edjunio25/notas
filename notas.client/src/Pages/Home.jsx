import { useNavigate } from 'react-router-dom';


export default function Home() {
    const navigate = useNavigate();

    return (
        <div style={{ textAlign: 'center', padding: '2rem' }}>
            <h1>Gestão de Notas Fiscais</h1>
            <p>Sistema simples para cadastro e visualização de empresas e notas fiscais</p>
            <div style={{ marginTop: '2rem', display: 'flex', justifyContent: 'center', gap: '1rem', flexWrap: 'wrap' }}>
                <button onClick={() => navigate('/notas')}>Ver Notas</button>
                <button onClick={() => navigate('/notas/novo')}>Cadastrar Nota</button>
                <button onClick={() => navigate('/empresas')}>Ver Empresas</button>
                <button onClick={() => navigate('/empresas/novo')}>Cadastrar Empresa</button>
            </div>
        </div>
    );
}