using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using QuantumQuasars.Data;
using UnityEngine;

public static class OrbitInfoAPI
{
    public static async Task<OrbitalInfoData> GetOrbitInfo()
    {
        OrbitalInfoData oiData = null;

        // PHP dosyasýnýn URL'si
        string url = "https://pace.oceansciences.org/getOrbitInfo.php";

        // HttpClient ile istek gönder
        using HttpClient client = new();
        try
        {
            // GET isteði gönder
            HttpResponseMessage response = await client.GetAsync(url);

            // Eðer baþarýlý ise
            if (response.IsSuccessStatusCode)
            {
                // JSON verisini al
                string jsonData = await response.Content.ReadAsStringAsync();

                // ["2024-10-05T12:51:00.000Z",3519,76.6556515,-141.3358068,688319.4,-2.7]

                // JSON verisini parse et
                JArray data = JArray.Parse(jsonData);
                //var data = JsonUtility.FromJson<object[]>(jsonData);
                oiData = new OrbitalInfoData((string)data[0], (float)data[2], (float)data[3]);
            }
            else
            {
                Debug.Log($"Hata: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Bir hata oluþtu: {ex.Message}");
        }

        return oiData;
    }
}