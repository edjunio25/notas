import { defineConfig } from '@playwright/test';

export default defineConfig({
    testDir: './Tests',
    timeout: 30000,
    retries: 1,
    use: {
        baseURL: 'https://localhost:65124/',
        headless: false,
        screenshot: 'only-on-failure',
        video: 'retain-on-failure',
        ignoreHTTPSErrors: true, 
    },

    webServer: {
        command: 'npm run dev',
        port: 65124,
        cwd: '../notas.Client', 
        timeout: 30000,
        reuseExistingServer: !process.env.CI, 
    },
});
