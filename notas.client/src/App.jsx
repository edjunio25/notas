import { useEffect, useState } from 'react';

function App() {
    const [notas, setNotas] = useState([]);
    const [form, setForm] = useState({
        numeroNota: '',
        chaveAcesso: '',
        serie: '',
        tipoNota: 0,
        valorTotal: 0,
        dataEmissao: '',
        dataPostagem: '',
        descricao: '',
        cnpjOrigem: '',
        cnpjDestino: ''
    });

    useEffect(() => {
        fetch('/api/NotaFiscal')
            .then(res => res.json())
            .then(data => setNotas(data));
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const res = await fetch('/api/NotaFiscal', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(form)
        });
        if (res.ok) {
            const nova = await res.json();
            setNotas([...notas, nova]);
        } else {
            alert("Erro ao criar nota.");
        }
    };

    return (
        <div style={{ padding: '2rem' }}>
            <h2>Notas Fiscais</h2>

            <form onSubmit={handleSubmit} style={{ marginBottom: '2rem' }}>
                <input placeholder="Número" onChange={e => setForm({ ...form, numeroNota: e.target.value })} />
                <input placeholder="Chave de Acesso" onChange={e => setForm({ ...form, chaveAcesso: e.target.value })} />
                <input placeholder="Série" onChange={e => setForm({ ...form, serie: e.target.value })} />
                <input type="number" placeholder="TipoNota (ex: 0)" onChange={e => setForm({ ...form, tipoNota: parseInt(e.target.value) })} />
                <input type="number" placeholder="Valor Total" onChange={e => setForm({ ...form, valorTotal: parseFloat(e.target.value) })} />
                <input type="date" placeholder="Data Emissão" onChange={e => setForm({ ...form, dataEmissao: e.target.value })} />
                <input type="date" placeholder="Data Postagem" onChange={e => setForm({ ...form, dataPostagem: e.target.value })} />
                <input placeholder="Descrição" onChange={e => setForm({ ...form, descricao: e.target.value })} />
                <input placeholder="CNPJ Origem" onChange={e => setForm({ ...form, cnpjOrigem: e.target.value })} />
                <input placeholder="CNPJ Destino" onChange={e => setForm({ ...form, cnpjDestino: e.target.value })} />
                <button type="submit">Criar Nota</button>
            </form>

            <ul>
                {notas.map(n => (
                    <li key={n.idNota}>
                        {n.numeroNota} - {n.empresaOrigem?.razaoSocial} → {n.empresaDestino?.razaoSocial}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
