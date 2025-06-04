import { useEffect, useState } from "react";
import { listarNotas } from "../Api/Api";
import { useNavigate } from "react-router-dom";

export default function VerNotas() {
    const [notas, setNotas] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        listarNotas().then(setNotas);
    }, []);

    return (
        <div style={{ padding: "20px" }}>
            <h2>Notas Fiscais</h2>

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
                        </tr>
                    ))}
                </tbody>
            </table>

            <button onClick={() => navigate("/")} style={{ marginTop: "20px" }}>
                Voltar para Home
            </button>
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
