import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../App.css';
import { cadastrarEmpresa } from "../Api/Api";

export default function CadastrarEmpresa() {
    const [empresa, setEmpresa] = useState({
        nomeFantasia: '',
        razaoSocial: '',
        cnpj: '',
        endereco: {
            cep: '',
            logradouro: '',
            numero: '',
            complemento: '',
            bairro: '',
            cidade: '',
            uf: '',
        },
    });

    const navigate = useNavigate();

    const atualizarEndereco = (campo, valor) => {
        setEmpresa(prev => ({
            ...prev,
            endereco: {
                ...prev.endereco,
                [campo]: valor,
            },
        }));
    };
    const estadosEnum = {
        AC: 0,AL: 1,AM: 2,AP: 3,BA: 4,CE: 5,DF: 6,ES: 7,GO: 8,
        MA: 9,MG: 10,MS: 11,MT: 12,PA: 13,PB: 14,PE: 15,PI: 16,PR: 17,RJ: 18,
        RN: 19,RO: 20,RR: 21,RS: 22,SC: 23,SE: 24,SP: 25,TO: 26
    };

    const buscarCep = async () => {
        const response = await fetch(`https://viacep.com.br/ws/${empresa.endereco.cep}/json/`);
        const data = await response.json();
        atualizarEndereco('logradouro', data.logradouro);
        atualizarEndereco('bairro', data.bairro);
        atualizarEndereco('cidade', data.localidade);
        atualizarEndereco('uf', data.uf);
    };


    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            ...empresa,
            endereco: {
                ...empresa.endereco,
                uf: estadosEnum[empresa.endereco.uf] ?? 0
            }
        };

        try {
            const response = await cadastrarEmpresa(payload);
            console.log('Empresa cadastrada com sucesso:', response);
            navigate('/empresas');
        } catch (error) {
            console.error('Erro ao cadastrar empresa:', error);
            alert('Falha ao cadastrar a empresa. Verifique os dados e tente novamente.');
        }
    };



    return (
        <div className="formulario-container">
            <h2 className="titulo">Cadastrar Empresa</h2>
            <form onSubmit={handleSubmit}>
                <div className="row">
                    <div className="column">
                        <label>Nome Fantasia</label>
                        <input className="input-padrao" value={empresa.nomeFantasia} onChange={(e) => setEmpresa({ ...empresa, nomeFantasia: e.target.value })} />
                    </div>
                    <div className="column">
                        <label>Razão Social</label>
                        <input className="input-padrao" value={empresa.razaoSocial} onChange={(e) => setEmpresa({ ...empresa, razaoSocial: e.target.value })} />
                    </div>
                    <div className="column">
                        <label>CNPJ</label>
                        <input className="input-padrao" value={empresa.cnpj} onChange={(e) => setEmpresa({ ...empresa, cnpj: e.target.value })} />
                    </div>
                </div>

                <div className="row">
                    <div className="column">
                        <label>CEP</label>
                        <div className="cep-group">
                            <input className="input-padrao" value={empresa.endereco.cep} onChange={(e) => atualizarEndereco('cep', e.target.value)} />
                            <button type="button" className="botao-padrao" onClick={buscarCep}>Buscar</button>
                        </div>
                    </div>
                    <div className="column">
                        <label>Logradouro</label>
                        <input className="input-padrao" value={empresa.endereco.logradouro} onChange={(e) => atualizarEndereco('logradouro', e.target.value)} />
                    </div>
                    <div className="column">
                        <label>Número</label>
                        <input className="input-padrao" value={empresa.endereco.numero} onChange={(e) => atualizarEndereco('numero', e.target.value)} />
                    </div>
                </div>

                <div className="row">
                    <div className="column">
                        <label>Complemento</label>
                        <input className="input-padrao" value={empresa.endereco.complemento} onChange={(e) => atualizarEndereco('complemento', e.target.value)} />
                    </div>
                    <div className="column">
                        <label>Bairro</label>
                        <input className="input-padrao" value={empresa.endereco.bairro} onChange={(e) => atualizarEndereco('bairro', e.target.value)} />
                    </div>
                    <div className="column">
                        <label>Cidade</label>
                        <input className="input-padrao" value={empresa.endereco.cidade} onChange={(e) => atualizarEndereco('cidade', e.target.value)} />
                    </div>
                    <div className="column">
                        <label>UF</label>
                        <input className="input-padrao" value={empresa.endereco.uf} onChange={(e) => atualizarEndereco('uf', e.target.value)} />
                    </div>
                </div>

                <div className="row botoes">
                    <button className="botao-padrao" type="submit">Salvar</button>
                    <button className="botao-padrao" type="button" onClick={() => navigate('/')}>Voltar</button>
                </div>
            </form>
        </div>
    );
}