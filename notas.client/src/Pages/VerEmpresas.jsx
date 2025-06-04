import { useEffect, useState } from "react";
import { listarEmpresas, deletarEmpresa } from "../Api/Api"

export default function VerEmpresas() {
    const [empresas, setEmpresas] = useState([]);

    useEffect(() => {
        listarEmpresas().then(setEmpresas);
    }, []);

    async function handleDelete(id) {
        await deletarEmpresa(id);
        setEmpresas(empresas.filter((e) => e.idEmpresa !== id));
    }

    return (
        <div>
            <h2>Empresas</h2>
            <ul>
                {empresas.map((empresa) => (
                    <li key={empresa.idEmpresa}>
                        {empresa.razaoSocial} ({empresa.cnpj})
                        <button onClick={() => handleDelete(empresa.idEmpresa)}>Excluir</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
