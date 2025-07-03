import { test, expect } from '@playwright/test';
import { Console } from 'console';

//Geral Tests
test('Página inicial carrega', async ({ page }) => {
    await page.goto('https://localhost:4173');
    await expect(page.locator('h1')).toHaveText(/Notas/);
});

test('É possível navegar entre todas as páginas usando os botões', async ({ page }) => {
    await page.goto('https://localhost:4173');

    //Cadastrar Empresa
    await page.getByRole('button', { name: 'Cadastrar Empresa' }).click();
    await expect(page).toHaveURL('https://localhost:4173/empresas/novo');

    //Menu inicial
    await page.getByRole('button', { name: 'Voltar' }).click();
    await expect(page).toHaveURL('https://localhost:4173/');

    //Cadastrar Nota
    await page.getByRole('button', { name: 'Cadastrar Nota' }).click();
    await expect(page).toHaveURL('https://localhost:4173/notas/novo');

    //Ver Notas
    await page.getByRole('button', { name: 'Voltar' }).click();
    await expect(page).toHaveURL('https://localhost:4173/notas');

    //Menu inicial
    await page.getByRole('button', { name: 'Voltar para Home' }).click();
    await expect(page).toHaveURL('https://localhost:4173/');

    //Ver Notas
    await page.getByRole('button', { name: 'Ver Notas' }).click();
    await expect(page).toHaveURL('https://localhost:4173/notas');

    //Menu inicial
    await page.getByRole('button', { name: 'Voltar para Home' }).click();
    await expect(page).toHaveURL('https://localhost:4173/');

    //Ver Empresas
    await page.getByRole('button', { name: 'Ver Empresas' }).click();
    await expect(page).toHaveURL('https://localhost:4173/empresas');

    //Menu inicial
    await page.getByRole('button', { name: 'Voltar para Home' }).click();
    await expect(page).toHaveURL('https://localhost:4173/');


});



// Cadastrar Empresa Tests
test('Página "Cadastrar Empresa" carrega com todos os campos', async ({ page }) => {
    await page.goto('https://localhost:4173/empresas/novo');


    await expect(page.getByText('Razão Social')).toBeVisible();
    
    await expect(page.getByText('CNPJ')).toBeVisible();
    await expect(page.getByText('CEP')).toBeVisible();
    await expect(page.getByText('Logradouro')).toBeVisible();
    await expect(page.getByText('Número')).toBeVisible();
    await expect(page.getByText('Bairro')).toBeVisible();
    await expect(page.getByText('Cidade')).toBeVisible();
    await expect(page.getByText('UF')).toBeVisible();

    await expect(page.getByRole('button', { name: /Salvar/i })).toBeVisible();
    await expect(page.getByRole('button', { name: /Voltar/i })).toBeVisible();
});

test('Cadastro de Empresa - preenchimento automático de endereço pelo CEP', async ({ page }) => {
    await page.goto('https://localhost:4173/empresas/novo');

    await page.locator('div').filter({ hasText: /^Buscar$/ }).getByRole('textbox').fill('32341020');

    await page.getByRole('button', { name: /buscar/i }).click();

    await expect(page.locator('div').filter({ hasText: /^Logradouro$/ }).getByRole('textbox')).toHaveValue('Avenida Potiguara');
    await expect(page.locator('div').filter({ hasText: /^Bairro$/ }).getByRole('textbox')).toHaveValue('Novo Eldorado');
    await expect(page.locator('div').filter({ hasText: /^Cidade$/ }).getByRole('textbox')).toHaveValue('Contagem');
    await expect(page.locator('div').filter({ hasText: /^UF$/ }).getByRole('textbox')).toHaveValue('MG');
});


// Cadastrar nota tests
test('Cadastro de Nota - alerta ao tentar salvar sem preencher', async ({ page }) => {
    await page.goto('https://localhost:4173/notas/novo');

    page.once('dialog', async dialog => {
        expect(dialog.message()).toContain('Verifique os dados e tente novamente');
        await dialog.dismiss();
    });

    await page.getByRole('button', { name: /salvar/i }).click();
});


