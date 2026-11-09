namespace ai_test
{
    public partial class Form1 : Form
    {
        private readonly LocalAgentClient _agentClient = new(new HttpClient());
        private readonly List<ChatMessage> _conversation =
        [
            new("system", "Du bist ein hilfreicher lokaler Agent. Antworte präzise und auf Deutsch, sofern der Benutzer keine andere Sprache verlangt.")
        ];

        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private async void Form1_Shown(object? sender, EventArgs e)
        {
            await LoadModelsAsync();
        }

        private async Task LoadModelsAsync()
        {
            SetBusy(true, "Lade lokale Modelle ...");

            try
            {
                var models = await _agentClient.GetModelsAsync();
                modelSelector.Items.Clear();
                modelSelector.Items.AddRange(models
                    .Where(model => !string.IsNullOrWhiteSpace(model.Id))
                    .Select(model => model.Id)
                    .Cast<object>()
                    .ToArray());

                if (modelSelector.Items.Count == 0)
                {
                    statusLabel.Text = "Keine Modelle unter /v1/models gefunden.";
                    return;
                }

                modelSelector.SelectedIndex = 0;
                statusLabel.Text = $"{modelSelector.Items.Count} Modell(e) verfügbar.";
            }
            catch (Exception exception)
            {
                statusLabel.Text = $"Lokaler Dienst nicht erreichbar: {exception.Message}";
            }
            finally
            {
                SetBusy(false, statusLabel.Text);
            }
        }

        private async void sendButton_Click(object? sender, EventArgs e)
        {
            var message = messageInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(message) || modelSelector.SelectedItem is not string model)
            {
                return;
            }

            messageInput.Clear();
            AppendMessage("Du", message);
            _conversation.Add(new ChatMessage("user", message));
            SetBusy(true, "Agent antwortet ...");

            try
            {
                var response = await _agentClient.CreateChatCompletionAsync(model, _conversation);
                _conversation.Add(new ChatMessage("assistant", response));
                AppendMessage("Agent", response);
                statusLabel.Text = $"Bereit · {model}";
            }
            catch (Exception exception)
            {
                statusLabel.Text = $"Fehler: {exception.Message}";
            }
            finally
            {
                SetBusy(false, statusLabel.Text);
                messageInput.Focus();
            }
        }

        private void AppendMessage(string speaker, string message)
        {
            chatHistory.AppendText($"{speaker}:\r\n{message}\r\n\r\n");
            chatHistory.SelectionStart = chatHistory.TextLength;
            chatHistory.ScrollToCaret();
        }

        private void SetBusy(bool busy, string status)
        {
            modelSelector.Enabled = !busy;
            messageInput.Enabled = !busy;
            sendButton.Enabled = !busy;
            statusLabel.Text = status;
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}
