using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ai_test;

public sealed class LocalAgentClient
{
    private readonly HttpClient _httpClient;

    public LocalAgentClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress ??= new Uri("http://127.0.0.1:8080/v1/");
    }

    public async Task<IReadOnlyList<LocalModel>> GetModelsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<ModelsResponse>("models", cancellationToken)
            ?? throw new InvalidOperationException("Der lokale Dienst hat keine Modellliste zurückgegeben.");

        return response.Data ?? [];
    }

    public async Task<string> CreateChatCompletionAsync(
        string model,
        IReadOnlyList<ChatMessage> messages,
        CancellationToken cancellationToken = default)
    {
        var request = new ChatCompletionRequest(model, messages);
        using var response = await _httpClient.PostAsJsonAsync("chat/completions", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var completion = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>(cancellationToken)
            ?? throw new InvalidOperationException("Der lokale Dienst hat keine Chat-Antwort zurückgegeben.");

        var message = completion.Choices?.FirstOrDefault()?.Message;
        var content = message?.Content ?? message?.ReasoningContent;
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new InvalidOperationException("Die Chat-Antwort enthält keinen Text.");
        }

        return content.Trim();
    }
}

public sealed record LocalModel(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("object")] string? Object = null);

public sealed record ChatMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);

internal sealed record ModelsResponse(
    [property: JsonPropertyName("data")] List<LocalModel>? Data);

internal sealed record ChatCompletionRequest(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("messages")] IReadOnlyList<ChatMessage> Messages);

internal sealed record ChatCompletionResponse(
    [property: JsonPropertyName("choices")] List<ChatChoice>? Choices);

internal sealed record ChatChoice(
    [property: JsonPropertyName("message")] ChatResponseMessage? Message);

internal sealed record ChatResponseMessage(
    [property: JsonPropertyName("content")] string? Content,
    [property: JsonPropertyName("reasoning_content")] string? ReasoningContent);
