import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { listarEmpresas, cadastrarNota } from "../Api/Api";

export default function CadastrarNota() {
    const [nota, setNota] = useState({
        empresaOrigemIdEmpresa: "",
        empresaDestinoIdEmpresa: "",
        numeroNota: "",
        chaveAcesso: "",
        serie: "",
        tipoNota: 0,
        valorTotal: "",
        dataEmissao: "",
        dataPostagem: "",
        descricao: ""
    });
    const [empresas, setEmpresas] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        listarEmpresas().then(setEmpresas);
    }, []);

    function handleChange(e) {
        setNota({ ...nota, [e.target.name]: e.target.value });
    }

    async function handleSubmit(e) {
        e.preventDefault();
        await cadastrarNota(nota);
        navigate("/notas");
    }

    return (
        <form onSubmit={handleSubmit}>
            <h2>Cadastrar Nota Fiscal</h2>

            <select name="empresaOrigemIdEmpresa" onChange={handleChange} required>
                <option value="">Origem</option>
                {empresas.map((e) => (
                    <option key={e.idEmpresa} value={e.idEmpresa}>{e.razaoSocial}</option>
                ))}
            </select>

            <select name="empresaDestinoIdEmpresa" onChange={handleChange} required>
                <option value="">Destino</option>
                {empresas.map((e) => (
                    <option key={e.idEmpresa} value={e.idEmpresa}>{e.razaoSocial}</option>
                ))}
            </select>

            <input name="numeroNota" placeholder="Número" onChange={handleChange} required />
            <input name="chaveAcesso" placeholder="Chave de Acesso" onChange={handleChange} required />
            <input name="serie" placeholder="Série" onChange={handleChange} required />
            <select name="tipoNota" onChange={handleChange}>
                <option value="0">Entrada</option>
                <option value="1">Saída</option>
            </select>
            <input name="valorTotal" placeholder="Valor Total" onChange={handleChange} required />
            <input type="date" name="dataEmissao" onChange={handleChange} required />
            <input type="date" name="dataPostagem" onChange={handleChange} required />
            <textarea name="descricao" placeholder="Descrição" onChange={handleChange} />

            <button type="submit">Salvar</button>
            <button type="button" onClick={() => navigate("/notas")}>Voltar</button>
        </form>
    );
}