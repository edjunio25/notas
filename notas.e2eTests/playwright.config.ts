import { defineConfig } from '@playwright/test';

export default defineConfig({
    testDir: './Tests',
    timeout: 30000,
    retries: 1,
    use: {
        baseURL: 'https://localhost:4173/',
        headless: true,
        screenshot: 'only-on-failure',
        video: 'retain-on-failure',
        ignoreHTTPSErrors: true, 
    },

    webServer: {
        command: 'npm run preview',
        port: 4173,
        cwd: '../notas.client',
        timeout: 30000,
        reuseExistingServer: !process.env.CI,
    }
});
