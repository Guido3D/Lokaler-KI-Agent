# AI Test

Eine Windows-Forms-App (.NET 8), die mit einem lokalen KI-Dienst kommuniziert
(Ollama-kompatibler `/v1`-API-Endpunkt, standardmäßig `http://127.0.0.1:8080`).

## Funktionen

- Auswählen lokaler Modelle über `/v1/models`
- Chat mit lokalem Modell über `/v1/chat/completions`
- Verlauf der Unterhaltung direkt in der App

## Voraussetzungen

- .NET SDK 8.0
- Ein lokaler KI-Dienst (z. B. Ollama), der auf `http://127.0.0.1:8080` erreichbar ist

## Bauen & Starten

```bash
dotnet build
dotnet run
```

## Lizenz

Dieses Projekt steht unter der [MIT-Lizenz](LICENSE) © 2026 Guido3D.
