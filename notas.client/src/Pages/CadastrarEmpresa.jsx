import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { cadastrarEmpresa } from "../Api/Api";

export default function CadastrarEmpresa() {
    const [empresa, setEmpresa] = useState({
        razaoSocial: "",
        nomeFantasia: "",
        cnpj: "",
    });
    const navigate = useNavigate();

    async function handleSubmit(e) {
        e.preventDefault();
        await cadastrarEmpresa(empresa);
        navigate("/empresas");
    }

    return (
        <form onSubmit={handleSubmit}>
            <h2>Cadastrar Empresa</h2>
            <input
                type="text"
                placeholder="Razão Social"
                value={empresa.razaoSocial}
                onChange={(e) => setEmpresa({ ...empresa, razaoSocial: e.target.value })}
                required
            />
            <input
                type="text"
                placeholder="Nome Fantasia"
                value={empresa.nomeFantasia}
                onChange={(e) => setEmpresa({ ...empresa, nomeFantasia: e.target.value })}
            />
            <input
                type="text"
                placeholder="CNPJ"
                value={empresa.cnpj}
                onChange={(e) => setEmpresa({ ...empresa, cnpj: e.target.value })}
                required
            />
            <button type="submit">Salvar</button>
            <button type="button" onClick={() => navigate("/empresas")}>
                Voltar
            </button>
        </form>
    );
}
