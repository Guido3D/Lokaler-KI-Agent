namespace ai_test
{
    partial class Form1
    {
        private ComboBox modelSelector;
        private RichTextBox chatHistory;
        private TextBox messageInput;
        private Button sendButton;
        private Label modelLabel;
        private Label statusLabel;
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            modelSelector = new ComboBox();
            chatHistory = new RichTextBox();
            messageInput = new TextBox();
            sendButton = new Button();
            modelLabel = new Label();
            statusLabel = new Label();
            SuspendLayout();

            modelLabel.AutoSize = true;
            modelLabel.Location = new Point(12, 15);
            modelLabel.Text = "Modell:";

            modelSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            modelSelector.FormattingEnabled = true;
            modelSelector.Location = new Point(68, 11);
            modelSelector.Size = new Size(420, 23);

            chatHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chatHistory.BackColor = SystemColors.Window;
            chatHistory.Location = new Point(12, 48);
            chatHistory.ReadOnly = true;
            chatHistory.Size = new Size(776, 320);

            messageInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            messageInput.Location = new Point(12, 382);
            messageInput.Multiline = true;
            messageInput.ScrollBars = ScrollBars.Vertical;
            messageInput.Size = new Size(680, 56);

            sendButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            sendButton.Location = new Point(704, 382);
            sendButton.Size = new Size(84, 56);
            sendButton.Text = "Senden";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;

            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(12, 447);
            statusLabel.Text = "Verbinde mit lokalem Dienst ...";

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 480);
            Controls.Add(modelLabel);
            Controls.Add(modelSelector);
            Controls.Add(chatHistory);
            Controls.Add(messageInput);
            Controls.Add(sendButton);
            Controls.Add(statusLabel);
            MinimumSize = new Size(600, 400);
            Text = "Lokaler KI-Agent";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
