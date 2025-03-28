﻿using System.Net.Http.Json;
using ScreenSound.Web.Response;

namespace ScreenSound.Web.Services;

public class MusicaAPI
{
    private readonly HttpClient _httpClient;

    public MusicaAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<MusicaResponse>?> GetMusicaAsync()
    {
        return await 
            _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("musicas");
    }
}