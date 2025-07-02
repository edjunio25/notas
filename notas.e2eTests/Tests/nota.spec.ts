import { test, expect } from '@playwright/test';

test('P�gina inicial carrega', async ({ page }) => {
    await page.goto('https://localhost:65124');
    await expect(page.locator('h1')).toHaveText(/Notas/);
});

test('� poss�vel criar uma nota', async ({ page }) => {
    await page.goto('https://localhost:65124');
    await page.fill('input[name="title"]', 'Nota de Teste');
    await page.fill('textarea[name="content"]', 'Conte�do da nota de teste');
    await page.click('button[type="submit"]');
    // Verifica se a nota foi criada
    const notas = page.locator('.nota');
    await expect(notas).toHaveCount(1);
    await expect(notas.first()).toContainText('Nota de Teste');
});