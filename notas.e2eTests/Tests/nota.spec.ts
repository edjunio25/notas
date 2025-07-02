import { test, expect } from '@playwright/test';

test('P�gina inicial carrega', async ({ page }) => {
    await page.goto('https://localhost:65124');
    await expect(page.locator('h1')).toHaveText(/Notas/);
});

