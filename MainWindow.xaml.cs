using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfCybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private string userName = "User";
        private string lastResponse = "";

        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeChatbot();
        }

        private void InitializeChatbot()
        {
            AppendMessage("Chatbot",
                "Hello! Welcome to the Cybersecurity Awareness Chatbot. I'm here to help you stay safe online.",
                Colors.Magenta);

            AppendMessage("Chatbot",
                "Please tell me your name to get started or type 'quiz' to begin the cybersecurity quiz.",
                Colors.Cyan);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                ProcessUserInput();
            }
        }

        private void ProcessUserInput()
        {
            string input = InputBox.Text.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            AppendMessage(userName, input, Colors.Cyan);

            if (quizQuestions != null && currentQuestionIndex < quizQuestions.Count)
            {
                CheckQuizAnswer(input);
                InputBox.Clear();
                ChatScroll.ScrollToEnd();
                return;
            }

            if (userName == "User" && !input.ToLower().Contains("exit"))
            {
                userName = input;

                AppendMessage("Chatbot",
                    $"Hello {userName}. How may I help you today with cybersecurity?",
                    Colors.Red);

                InputBox.Clear();
                ChatScroll.ScrollToEnd();
                return;
            }

            if (input.ToLower().Contains("quiz"))
            {
                StartQuiz();
                InputBox.Clear();
                return;
            }

            string response = GetResponse(input);
            lastResponse = response;

            AppendMessage("Chatbot", response, GetColorForResponse(input));

            InputBox.Clear();
            ChatScroll.ScrollToEnd();
        }

        private string GetResponse(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("how are you"))
                return "I'm doing fine. Safe and secure, thank you!";

            else if (lowerInput.Contains("who are you") || lowerInput.Contains("purpose"))
                return "I am a cybersecurity assistant helping you stay safe online.";

            else if (lowerInput.Contains("phishing"))
                return "Phishing is a scam where attackers trick you into giving sensitive information.";

            else if (lowerInput.Contains("password"))
                return "Use strong passwords with letters, numbers, symbols & enable 2FA.";

            else if (lowerInput.Contains("privacy"))
                return "Protect privacy by limiting personal info and using secure networks.";

            else if (lowerInput.Contains("scam"))
                return "Always verify messages and never click suspicious links.";

            else if (lowerInput.Contains("exit") || lowerInput.Contains("bye"))
                return "Goodbye! Stay safe online.";

            return "Ask me about phishing, passwords, scams, privacy, or type 'quiz' to start the test.";
        }

        private Color GetColorForResponse(string input)
        {
            string lower = input.ToLower();

            if (lower.Contains("password"))
                return Colors.Yellow;

            if (lower.Contains("phishing"))
                return Colors.LightBlue;

            if (lower.Contains("scam"))
                return Colors.Orange;

            if (lower.Contains("privacy"))
                return Colors.LightGreen;

            return Colors.White;
        }

        private void AppendMessage(string sender, string message, Color color)
        {
            string timestamp = DateTime.Now.ToString("HH:mm");
            ChatHistory.Text += $"[{timestamp}] {sender}: {message}\n\n";
            ChatScroll.ScrollToEnd();
        }

        private void StartQuiz()
        {
            QuizQuestions();

            currentQuestionIndex = 0;
            score = 0;

            AppendMessage("Chatbot",
                "Starting Cybersecurity Quiz! Answer using numbers (1, 2, 3...).",
                Colors.Cyan);

            ShowNextQuestion();
        }

        private void QuizQuestions()
        {
            quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion(
                    "Phishing is a type of threat that abuses trust to steal information",
                    new[] { "True", "False" },
                    0),

                new QuizQuestion(
                    "What is social engineering?",
                    new[] { "Better communication", "Manipulating users into giving sensitive info", "Coding skill", "Hardware hacking" },
                    1),

                new QuizQuestion(
                    "When is a website secure?",
                    new[] { "Always", "When it says so", "When it uses HTTPS", "Incognito mode" },
                    2),

                new QuizQuestion(
                    "Is 2FA a good security practice?",
                    new[] { "Yes", "No", "Maybe" },
                    0),

                new QuizQuestion(
                    "What should you do with phishing emails?",
                    new[] { "Ignore and report", "Click links", "Reply", "Share them" },
                    0),
                new QuizQuestion(
                "What is the safest way to create a strong password?",
                  new[] { "Use your name and birthday", "Use short simple words", "Use a long mix of letters, numbers, and symbols", "Use 'password123'" },
                    2),

                new QuizQuestion(
                    "What is phishing primarily used for?",
                    new[] { "Improving network speed", "Stealing sensitive information through fake messages", "Fixing software bugs", "Encrypting files" },
                    1),
                
                new QuizQuestion(
                    "What should you do if you receive a suspicious email link?",
                    new[] { "Click it quickly", "Forward it to friends", "Verify the sender before clicking", "Ignore your antivirus" },
                    2),
                
                new QuizQuestion(
                    "What does 2FA stand for?",
                    new[] { "Two File Access", "Two Factor Authentication", "Fast Access System", "Firewall Authentication" },
                    1),
                
                new QuizQuestion(
                    "Which of the following is a safe practice on public Wi-Fi?",
                    new[] { "Log into banking accounts freely", "Use a VPN connection", "Disable antivirus", "Share files with strangers" },
                    1),
                
                new QuizQuestion(
                    "What is social engineering in cybersecurity?",
                    new[] { "Building social networks", "Manipulating people into revealing sensitive data", "Programming websites", "Fixing hardware issues" },
                    1),
                
                new QuizQuestion(
                    "Why should you regularly update software?",
                    new[] { "To change the interface only", "To add unnecessary apps", "To fix security vulnerabilities", "To slow down your device" },
                    2)
                             };
                        }

        private void ShowNextQuestion()
        {
            if (currentQuestionIndex >= quizQuestions.Count)
            {
                AppendMessage("Chatbot",
                    $"Quiz finished! Score: {score}/{quizQuestions.Count}",
                    Colors.LightGreen);
                return;
            }

            var q = quizQuestions[currentQuestionIndex];

            string options = "";
            for (int i = 0; i < q.Options.Length; i++)
            {
                options += $"{i + 1}. {q.Options[i]}\n";
            }

            AppendMessage("Quiz", q.Question + "\n" + options, Colors.Yellow);
        }

        private void CheckQuizAnswer(string input)
        {
            if (!int.TryParse(input, out int answer))
            {
                AppendMessage("Chatbot", "Please enter a number (1, 2, 3...)", Colors.Red);
                return;
            }

            var q = quizQuestions[currentQuestionIndex];

            if (answer - 1 == q.CorrectIndex)
            {
                score++;
                AppendMessage("Chatbot", "Correct!", Colors.LightGreen);
            }
            else
            {
                AppendMessage("Chatbot", "Incorrect.", Colors.Red);
            }

            currentQuestionIndex++;
            ShowNextQuestion();
        }

        private void ClearChat_Click(object sender, RoutedEventArgs e)
        {
            ChatHistory.Text = "";
            AppendMessage("Chatbot", "Chat cleared.", Colors.Magenta);
        }

        private void SpeakLastResponse_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(lastResponse, "Last Response");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }

        public QuizQuestion(string question, string[] options, int correctIndex)
        {
            Question = question;
            Options = options;
            CorrectIndex = correctIndex;
        }
    }
}