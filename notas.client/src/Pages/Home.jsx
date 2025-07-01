import { useNavigate } from 'react-router-dom';

export default function Home() {
    const navigate = useNavigate();

    return (
        <div style={{
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
            height: '100vh',
            padding: '2rem',
            fontFamily: 'Arial, sans-serif',
            textAlign: 'center',
        }}>
            <h1 style={{ fontSize: '2rem', marginBottom: '0.5rem' }}>Gestão de Notas Fiscais</h1>
            <p style={{ fontSize: '1.1rem', marginBottom: '2rem', color: '#aaa' }}>
                Sistema simples para cadastro e visualização de empresas e notas fiscais
            </p>

            <div style={{
                display: 'grid',
                gridTemplateColumns: 'repeat(2, 160px)',
                gap: '1.5rem',
                justifyContent: 'center'
            }}>
                <button style={buttonStyle} onClick={() => navigate('/notas')}>Ver Notas</button>
                <button style={buttonStyle} onClick={() => navigate('/notas/novo')}>Cadastrar Nota</button>
                <button style={buttonStyle} onClick={() => navigate('/empresas')}>Ver Empresas</button>
                <button style={buttonStyle} onClick={() => navigate('/empresas/novo')}>Cadastrar Empresa</button>
            </div>
        </div>
    );
}

const buttonStyle = {
    height: '100px',
    width: '160px',
    borderRadius: '12px',
    border: 'none',
    backgroundColor: '#007bff',
    color: '#fff',
    fontSize: '1rem',
    fontWeight: 'bold',
    cursor: 'pointer',
    boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
    transition: 'transform 0.2s ease',
};
