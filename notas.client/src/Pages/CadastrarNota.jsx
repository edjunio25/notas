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
        const { name, value } = e.target;

        const conversoes = {
            tipoNota: parseInt(value),
            empresaOrigemIdEmpresa: parseInt(value),
            empresaDestinoIdEmpresa: parseInt(value),
            valorTotal: parseFloat(value),
        };

        setNota(prev => ({
            ...prev,
            [name]: conversoes[name] ?? value
        }));
    }


    async function handleSubmit(e) {
        e.preventDefault();
        nota.dataPostagem = new Date().toISOString(); 
        await cadastrarNota(nota);
        navigate("/notas");
    }

    return (
        <form onSubmit={handleSubmit} className="formulario-container text-left">
            <h2 className="text-2xl font-bold mb-4">Cadastrar Nota Fiscal</h2>

            <div className="row">
                <div className="column">
                    <label>Empresa de Origem</label>
                    <select
                        name="empresaOrigemIdEmpresa"
                        onChange={handleChange}
                        required
                        className="input-padrao w-full"
                    >
                        <option value="">Selecione</option>
                        {empresas.map((e) => (
                            <option key={e.idEmpresa} value={e.idEmpresa}>{e.razaoSocial}</option>
                        ))}
                    </select>
                </div>

                <div className="column">
                    <label>Empresa de Destino</label>
                    <select
                        name="empresaDestinoIdEmpresa"
                        onChange={handleChange}
                        required
                        className="input-padrao w-full"
                    >
                        <option value="">Selecione</option>
                        {empresas.map((e) => (
                            <option key={e.idEmpresa} value={e.idEmpresa}>{e.razaoSocial}</option>
                        ))}
                    </select>
                </div>
            </div>

            <div className="row">
                <div className="column">
                    <label>Número da Nota</label>
                    <input
                        name="numeroNota"
                        placeholder="Número"
                        onChange={handleChange}
                        required
                        className="input-padrao w-full"
                    />
                </div>

                <div className="column">
                    <label>Chave de Acesso</label>
                    <input
                        name="chaveAcesso"
                        placeholder="Chave de Acesso"
                        onChange={handleChange}
                        required
                        className="input-padrao w-full"
                    />
                </div>

                <div className="column">
                    <label>Série</label>
                    <input
                        name="serie"
                        placeholder="Série"
                        onChange={handleChange}
                        required
                        className="input-padrao w-full"
                    />
                </div>
            </div>

            <div className="row">
                <div className="column">
                    <label>Tipo da Nota</label>
                    <select
                        name="tipoNota"
                        onChange={handleChange}
                        className="input-padrao w-full"
                    >
                        <option value="0">Entrada</option>
                        <option value="1">Saída</option>
                    </select>
                </div>

                <div className="column">
                    <label>Valor Total</label>
                    <input
                        name="valorTotal"
                        placeholder="Valor Total"
                        onChange={handleChange}
                        required
                        className="input-padrao w-full"
                    />
                </div>
            </div>

            <div className="row">
                <div className="column">
                    <label>Data de Emissão</label>
                    <input
                        type="date"
                        name="dataEmissao"
                        onChange={handleChange}
                        required
                        className="input-padrao w-full"
                    />
                </div>
            </div>

            <div className="row">
                <div className="column">
                    <label>Descrição</label>
                    <textarea
                        name="descricao"
                        placeholder="Descrição"
                        onChange={handleChange}
                        className="input-padrao w-full"
                        rows={4}
                    />
                </div>
            </div>


            <div className="mt-6 flex gap-4">
                <button type="submit" className="botao-padrao">Salvar</button>
                <button type="button" onClick={() => navigate("/notas")} className="botao-padrao">Voltar</button>
            </div>
        </form>
    );
}
