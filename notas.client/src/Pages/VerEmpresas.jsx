import { useEffect, useState } from "react";
import { listarEmpresas } from "../Api/Api";
import { useNavigate } from "react-router-dom";

export default function VerEmpresas() {
    const [empresas, setEmpresas] = useState([]);
    const [empresaSelecionada, setEmpresaSelecionada] = useState(null);
    const [mostrarModal, setMostrarModal] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        carregarEmpresas();
    }, []);

    async function carregarEmpresas() {
        const lista = await listarEmpresas();
        setEmpresas(lista);
    }

    const abrirModalExclusao = (empresa) => {
        setEmpresaSelecionada(empresa);
        setMostrarModal(true);
    };

    const confirmarExclusao = async () => {
        await fetch(`https://localhost:7133/api/empresa/${empresaSelecionada.idEmpresa}/desativar`, {
            method: 'PUT'
        });
        await carregarEmpresas();
        setMostrarModal(false);
        setEmpresaSelecionada(null);
    };

    return (
        <div style={{ padding: "20px" }}>
            <h2>Empresas</h2>

            {empresas.filter((e) => e.isAtiva === 1).length === 0 ? (
                <p style={{ marginTop: "20px" }}>Nenhuma empresa disponível para visualização.</p>
            ) : (
                <table style={{ width: "100%", borderCollapse: "collapse" }}>
                    <thead>
                        <tr>
                            <th style={th}>Razão Social</th>
                            <th style={th}>CNPJ</th>
                            <th style={th}>Endereço</th>
                            <th style={th}>Cidade</th>
                            <th style={th}>UF</th>
                            <th style={th}>Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {empresas
                            .filter((empresa) => empresa.isAtiva === 1)
                            .map((empresa) => (
                                <tr key={empresa.idEmpresa}>
                                    <td style={td}>{empresa.razaoSocial}</td>
                                    <td style={td}>{empresa.cnpj}</td>
                                    <td style={td}>
                                        {empresa.enderecoEmpresa &&
                                            `${empresa.enderecoEmpresa.logradouro}, ${empresa.enderecoEmpresa.numero}, ${empresa.enderecoEmpresa.bairro}`}
                                    </td>
                                    <td style={td}>{empresa.enderecoEmpresa?.cidade}</td>
                                    <td style={td}>{empresa.enderecoEmpresa?.uf}</td>
                                    <td style={td}>
                                        <button
                                            onClick={() => abrirModalExclusao(empresa)}
                                            className="botao-padrao"
                                        >
                                            Excluir
                                        </button>
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
                <div style={modalOverlay}>
                    <div style={modalContent}>
                        <p>
                            Deseja realmente excluir a empresa{" "}
                            <strong>{empresaSelecionada?.razaoSocial}</strong>?
                        </p>
                        <div style={{ marginTop: "1rem", display: "flex", justifyContent: "center", gap: "1rem" }}>
                            <button
                                onClick={confirmarExclusao}
                                style={{
                                    padding: '0.5rem 1rem',
                                    background: '#dc2626',
                                    color: '#fff',
                                    borderRadius: '6px',
                                    border: 'none'
                                }}
                            >
                                Confirmar
                            </button>
                            <button
                                onClick={() => setMostrarModal(false)}
                                style={{
                                    padding: '0.5rem 1rem',
                                    background: '#e5e7eb',
                                    color: '#000',
                                    borderRadius: '6px',
                                    border: 'none'
                                }}
                            >
                                Cancelar
                            </button>
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
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: "rgba(0, 0, 0, 0.5)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    zIndex: 1000
};

const modalContent = {
    background: "#1f2937", 
    color: "#f9fafb",       
    padding: "2rem",
    borderRadius: "8px",
    minWidth: "300px",
    textAlign: "center",
    boxShadow: "0 4px 10px rgba(0, 0, 0, 0.3)"
};