namespace Wpm.Clinic.Api.Services;

public sealed class ManagementService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<PetInfo?> GetPetInfoById(int petId)
    {
        var petInfo = await _httpClient.GetFromJsonAsync<PetInfo>($"/api/pets/{petId}");

        return petInfo is null ? null : petInfo;
    }
}
