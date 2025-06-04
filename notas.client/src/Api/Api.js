const API_URL = "http://localhost:5297/api";

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
    const response = await fetch("http://localhost:5297/api/NotaFiscal", {
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

