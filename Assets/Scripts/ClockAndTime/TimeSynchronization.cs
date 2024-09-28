using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class TimeSynchronization
{
    private const string _worldTimeAPI = "https://worldtimeapi.org/api/timezone/Etc/UTC";
    private const string _backupTimeAPI = "https://timeapi.io/api/Time/current/zone?timeZone=UTC";

    public async Task<DateTime> GetTimeFromAPI(int maxRetries = 10)
    {
        int retries = 0;
        while (retries < maxRetries)
        {
            try
            {
                DateTime time = await TryGetTimeFromService(_worldTimeAPI);
                if (time != DateTime.MinValue)
                {
                    return time;
                }
                Debug.LogWarning("Основной сервис недоступен. Попытка использовать резервный сервис.");
                time = await TryGetTimeFromService(_backupTimeAPI);
                if (time != DateTime.MinValue)
                {
                    return time;
                }
            }
            catch (Exception e)
            {
                retries++;
                Debug.LogWarning("Не удалось получить время. Попытка: " + retries + "\b Причина: " + e);
                await Task.Delay(1000);
            }
        }

        return DateTime.MinValue;
    }

    private async Task<DateTime> TryGetTimeFromService(string apiUrl)
    {
        try
        {
            return await GetTimeFromService(apiUrl);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Ошибка при обращении к сервису: " + apiUrl + " - " + e.Message);
        }
        return DateTime.MinValue;
    }

    private async Task<DateTime> GetTimeFromService(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                string dateTimeString = ExtractDateTimeFromJson(jsonResponse);
                if (!string.IsNullOrEmpty(dateTimeString))
                {
                    return DateTime.Parse(dateTimeString);
                }
            }
        }
        return DateTime.MinValue;
    }
    private string ExtractDateTimeFromJson(string json)
    {
        int dateTimeIndex = json.IndexOf("\"datetime\":\"");
        if (dateTimeIndex != -1)
        {
            dateTimeIndex += 12;
            int endIndex = json.IndexOf("\"", dateTimeIndex);
            if (endIndex != -1)
            {
                return json.Substring(dateTimeIndex, endIndex - dateTimeIndex);
            }
        }
        return null;
    }
}
