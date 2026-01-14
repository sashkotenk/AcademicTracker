const request = require('supertest');
const baseUrl = 'https://localhost:7001'; // Порт, на якому працює твій сайт

// Дозволяємо працювати з самопідписаним SSL сертифікатом (localhost)
process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

describe('Academic Tracker API Tests', () => {
    
    // Тест для версії 1: має бути простий список
    test('GET /api/v1/students returns 200 and basic data', async () => {
        const response = await request(baseUrl).get('/api/v1/students');
        
        expect(response.status).toBe(200);
        expect(Array.isArray(response.body)).toBe(true);
        
        if (response.body.length > 0) {
            const student = response.body[0];
            // У версії 1 має бути ID, FullName, Group, але НЕМАЄ Email
            expect(student).toHaveProperty('id');
            expect(student).toHaveProperty('fullName');
            expect(student).not.toHaveProperty('email'); 
        }
    });

    // Тест для версії 2: мають бути розширені дані
    test('GET /api/v2/students returns 200 and extended data', async () => {
        const response = await request(baseUrl).get('/api/v2/students');
        
        expect(response.status).toBe(200);
        expect(Array.isArray(response.body)).toBe(true);
        
        if (response.body.length > 0) {
            const student = response.body[0];
            // У версії 2 МАЄ БУТИ Email та AverageScore
            expect(student).toHaveProperty('email');
            expect(student).toHaveProperty('averageScore');
        }
    });
});