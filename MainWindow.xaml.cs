using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityChatbot
{
    //Implements responses to user input such as bot purpose, info on passwords and phishing
    Dictionary<string, List<string>> Trivia = new()
        {
            {"purpose", new() {
               "My purpose as a chatbot is to provide you information on security on the internet",
               "I am here to assist you with password creation, management, safety online and security",
               "My sole purpose is to ensure you leave this chat better informed on how to stay safe from scams and online threats"
            }
        },
            {"password", new() {
                "Passwords are safety locks that keep your account and sensitive information safe from unwanted users.",
                "Passworeds shouldn't contain any personal information such as names or places of significance.",
                "A password should contain a minimum of 8 characters and must consist of letters, numbers, capital letters and lowercase letters."
            }
        },
            {"phishing", new() {
                "Phishing is a type of cyberattack that uses trickery to scam users out of their personal information and assets.",
                "Properly analyzing the types of messages and impersonations can help with realizing the phishing attempt.",
                "Make sure not to interact with suspicious mail or entertain any phone calls that sound shady",
            }
        }
         
        
        };
    public partial class MainWindow : Window
    {
        private string userName;
        private bool nameInput = false;


        public object Log { get; }
        public object UserInputBox { get; private set; }
        public object Message { get; private set; }

        public MainWindow(object Log)
        {
            InitializeComponent();
            ShowWelcomeMessage();
            Log = Log;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        //method to display the statup message and request the user name
        private void ShowWelcomeMessage()
        {
            AddMessageToChat("┌[0-0]┐", "Chatbot", true);
            AddMessageToChat("Hello! Welcome to the CyberSecurity Awareness Chatbot. I'm here to help.", "Chatbot", true);
            AddMessageToChat("Please enter your name:", "Chatbot", true);
        }

        //Displays previous and current messages in the chat log
        private void AddMessageToChat(string message, string sender, bool isBotMessage)
        {
            var chatMessage = new ChatMessage
            {
                Message = message,
                Sender = sender,
                IsBotMessage = isBotMessage
            };

            Message.Add(chatMessage);


        }

        //Read user input for name and allow the chat to progress
        private void ProcessUserInput(string input)
        {
            if (!nameInput)
            {
                userName = input;
                nameInput = true;
                AddMessageToChat($"Hello {userName}. How may I help you?", "Chatbot", true);
                return;
            }
            //List of set responses depending on the user's questions
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("how are you"))
            {
                AddMessageToChat("I'm doing fine. Safe and secure, thank you.", "Chatbot", true);
            }
            else if (lowerInput.Contains("what is your purpose"))
            {
                AddMessageToChat("I am a bot that was created with the purpose of properly educating you on the importance of cybersecurity as well as answering your queries to the best of my abilities.", "Chatbot", true);
            }
            else if (lowerInput.Contains("what can i ask you about"))
            {
                AddMessageToChat("You may ask me about what cybersecurity is, the importance of it in everyday life, as well as common threats to cybersecurity.", "Chatbot", true);
            }
            else if (lowerInput.Contains("what is cybersecurity"))
            {
                AddMessageToChat("Cybersecurity refers to various methods, tools and strategies that are set in place to protect a computer from various digital threats like cyber attacks and unauthorized access from known and unknown parties and ensures that users can practice safe browsing. Examples of such threats are phishing, unethical hacking as well as password theft.", "Chatbot", true);
            }
            else if (lowerInput.Contains("what is the importance of cybersecurity"))
            {
                AddMessageToChat("Cybersecurity assists in maintaining user security and safety by ensuring user data is protected and inaccessible to unwanted, unauthorized parties. This is done by reinforcing the strength of user passwords, frequent security checks, 2 factor verification as well as a quick response to security breaches.", "Chatbot", true);
            }
            else if (lowerInput.Contains("what are some threats to cybersecurity"))
            {
                AddMessageToChat("Examples of threats to cybersecurity include phishing, which is when attackers deceive users into giving away sensitive information through emails or messages.", "Chatbot", true);
            }
            else if (lowerInput.Contains("exit"))
            {
                AddMessageToChat("Thanks for stopping by. Goodbye and stay safe.", "Chatbot", true);
                Application.Current.Shutdown();
            }
            else
            {
                AddMessageToChat("[・╭╮・]┘", "Chatbot", true);
                AddMessageToChat("I didn't understand that question. Please try again.", "Chatbot", true);
            }
        }

        //Allows the send button to function for the sake of progressing the chat
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string input = UserInputBox.ToString();
            if (!string.IsNullOrEmpty(input))
            {
                AddMessageToChat(input, userName ?? "You", false);
                ProcessUserInput(input);
                UserInputBox.ToString();
            }
        }
        //Searches for keywords in the reminder function
        private string NLPTask(string userInput)
        {
            string task = userInput;
            string taskReminder = "";
            if (userInput.Contains("reminder"))
                taskReminder = userInput;
        }
        private void QuizQuestions()
        {
            quizQuestions = new()
            ("Phising is a type of threat that abuses your trust in close parties by impersonating them to acquire data and personal information", new[] { "True", "False" }, 0)
                ("What is the purpose of social engineering? ", new[] { "To create better social skills", "To manipulate users into giving sensitive info", "To create better conversation", "To exploit users' ears and thoughts" }, 1)
                ("When can you be certain your site is secured?", new[] { "When it says so", "you can never be certain", "When the link starts with 'https' ", "when the browser is in incognito mode" }, 2)
                ("Security is a redundant medium as some people can access your data anyways", new[] {"True", "False"  }, 1)
                ("Phishing may be harmful but can come with some hidden benefits to victims", new[] {"True", "False" }, 1)
                ("What countermeasures should you take against scams like phishing?", new[] { "Panic and fall for it", "Relax and ignore it", "Keep a cool head and report the attempt", "Do nothing" }, 2)
                ("Is 2FA a wise security strategy to apply?", new[] { "Yes", "No", "I don't know" }, 0)
                ("Under what circumstances should you use public wifi?", new[] { "When you need internet for Youtube", "When you want to send a WhatsApp", "Only if you have a VPN active", "when searching for phishing preventative measures" }, 2)
                ("End-to-End encryption aims to keep your privacy as the highest priority?", new[] { "True", "False"}, 0)
                }
        public class ChatMessage
        {
            public string Message { get; set; }
            public string Sender { get; set; }
            public bool IsBotMessage { get; set; }
        }
    }
}
