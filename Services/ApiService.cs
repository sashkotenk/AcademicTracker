using Newtonsoft.Json;
using System.Net.Http.Json;
using Tracker.Mobile.Models;

namespace Tracker.Mobile.Services;

public class ApiService
{
    private HttpClient _httpClient;

    // URL для Android (10.0.2.2) і Windows (localhost)
    private string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android
        ? "https://10.0.2.2:7001/api/v1"
        : "https://localhost:7001/api/v1";

    public ApiService()
    {
        // Ігнорування SSL помилок для локальної розробки (Hack для Android Emulator)
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        _httpClient = new HttpClient(handler);
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/students");
            return JsonConvert.DeserializeObject<List<Student>>(response);
        }
        catch (Exception ex)
        {
            // Повертаємо пустий список, якщо помилка з'єднання
            System.Diagnostics.Debug.WriteLine($"API Error: {ex.Message}");
            return new List<Student>();
        }
    }

    public async Task<bool> AddStudentAsync(Student student)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/students", student);
        return response.IsSuccessStatusCode;
    }
}