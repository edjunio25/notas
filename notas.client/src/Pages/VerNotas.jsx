import { useEffect, useState } from "react";
import { listarNotas } from "../Api/Api";
import { useNavigate } from "react-router-dom";

export default function VerNotas() {
    const [notas, setNotas] = useState([]);
    const [notaSelecionada, setNotaSelecionada] = useState(null);
    const [mostrarModal, setMostrarModal] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        listarNotas().then(setNotas);
    }, []);

    async function deletarNota(id) {
        await fetch(`https://localhost:7133/api/notafiscal/${id}`, {
            method: 'DELETE'
        });
    }

    const excluirNota = (nota) => {
        setNotaSelecionada(nota);
        setMostrarModal(true);
    };

    const confirmarExclusao = async () => {
        await deletarNota(notaSelecionada.idNota); 
        const atualizadas = await listarNotas();
        setNotas(atualizadas);
        setMostrarModal(false);
        setNotaSelecionada(null);
    };

    return (
        <div style={{ padding: "20px" }}>
            <h2>Notas Fiscais</h2>

            {notas.length === 0 ? (
                <p style={{ marginTop: "20px" }}>Nenhuma nota disponível para visualização.</p>
            ) : (
                <table style={{ width: "100%", borderCollapse: "collapse" }}>
                    <thead>
                        <tr>
                            <th style={th}>Número</th>
                            <th style={th}>Valor</th>
                            <th style={th}>Tipo</th>
                            <th style={th}>Data de Emissão</th>
                            <th style={th}>Data de Postagem</th>
                            <th style={th}>Descrição</th>
                            <th style={th}>Origem</th>
                            <th style={th}>Destino</th>
                            <th style={th}>Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {notas.map((n, i) => (
                            <tr key={i}>
                                <td style={td}>{n.numeroNota}</td>
                                <td style={td}>R$ {n.valorTotal}</td>
                                <td style={td}>{n.tipoNota}</td>
                                <td style={td}>{new Date(n.dataEmissao).toLocaleDateString()}</td>
                                <td style={td}>{new Date(n.dataPostagem).toLocaleDateString()}</td>
                                <td style={td}>{n.descricao}</td>
                                <td style={td}>{n.empresaOrigem?.razaoSocial || "-"}</td>
                                <td style={td}>{n.empresaDestino?.razaoSocial || "-"}</td>
                                <td style={td}>
                                    <button onClick={() => excluirNota(n)}>Excluir</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}

            <button onClick={() => navigate("/")} style={{ marginTop: "20px" }}>
                Voltar para Home
            </button>

            {mostrarModal && (
                <div style={{
                    position: 'fixed',
                    top: 0, left: 0, right: 0, bottom: 0,
                    backgroundColor: 'rgba(0,0,0,0.5)',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    zIndex: 1000
                }}>
                    <div style={{
                        backgroundColor: '#fff',
                        color: '#000',
                        padding: '1.5rem',
                        borderRadius: '8px',
                        boxShadow: '0 2px 10px rgba(0, 0, 0, 0.2)',
                        width: '300px',
                        textAlign: 'center'
                    }}>
                        <p>Deseja realmente excluir a nota <strong>{notaSelecionada?.numeroNota}</strong>?</p>
                        <div style={{ marginTop: "1rem", display: "flex", justifyContent: "center", gap: "1rem" }}>
                            <button onClick={confirmarExclusao} style={{ padding: '0.5rem 1rem' }}>Confirmar</button>
                            <button onClick={() => setMostrarModal(false)} style={{ padding: '0.5rem 1rem' }}>Cancelar</button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}

const th = {
    border: "1px solid #ccc",
    padding: "8px",
    background: "#333",
    color: "white",
    textAlign: "left"
};

const td = {
    border: "1px solid #ccc",
    padding: "8px"
};

const modalOverlay = {
    position: "fixed",
    top: 0, left: 0, right: 0, bottom: 0,
    backgroundColor: "rgba(0, 0, 0, 0.5)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    zIndex: 1000
};

const modalContent = {
    background: "#fff",
    padding: "2rem",
    borderRadius: "8px",
    minWidth: "300px",
    textAlign: "center"
};
