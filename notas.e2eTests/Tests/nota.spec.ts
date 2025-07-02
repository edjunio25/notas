import { test, expect } from '@playwright/test';

test('Página inicial carrega', async ({ page }) => {
    await page.goto('https://localhost:4173');
    await expect(page.locator('h1')).toHaveText(/Notas/);
});

