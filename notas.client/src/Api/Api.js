const API_URL = "https://localhost:7133/api";

export async function listarEmpresas() {
    const response = await fetch(`${API_URL}/Empresa`);
    return await response.json();
}

export async function cadastrarEmpresa(empresa) {
    const response = await fetch(`${API_URL}/Empresa`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(empresa),
    });
    return await response.json();
}

export async function deletarEmpresa(id) {
    return await fetch(`${API_URL}/Empresa/${id}`, { method: "DELETE" });
}

export async function listarNotas() {
    const response = await fetch(`${API_URL}/NotaFiscal`);
    return await response.json();
}

export async function cadastrarNota(nota) {
    const response = await fetch("https://localhost:7133/api/NotaFiscal", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(nota),
    });

    if (!response.ok) {
        const textoErro = await response.text();
        throw new Error(`Erro ao cadastrar nota: ${textoErro}`);
    }

    return await response.json();
}




export async function buscarEnderecoPorCep(cep) {
    const response = await fetch(`${API_URL}/cep/${cep}`);
    if (!response.ok) {
        throw new Error("CEP não encontrado.");
    }
    return await response.json();
}


