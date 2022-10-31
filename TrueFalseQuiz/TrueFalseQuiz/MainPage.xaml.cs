using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TrueFalseQuiz
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        List<Question> questions = new List<Question>()
        {
            new Question("Cats make the best pets.", "cat.png"),
            new Question("On cold days I like to sit on the couch with a warm blanket.", "blanket.png"),
            new Question("Schrodinger's cat is dead.", "schrodingers_cat.jpg"),
            new Question("Schrodinger's cat is alive", "schrodingers_cat.jpg"),
            new Question("I prefer tea over coffee.", "tea_or_coffee.jpg"),
            new Question("Dogs are great.", "dog.jpg")
        };

        List<string> trueTraits = new List<string>()
        {
            "Couragous", "Dependable", "Insightful", "Smart", "Orderly", "Innovative", "Sensitive", "Fair", "Incorruptable", "Tolerant"
        };
        List<string> falseTraits = new List<string>()
        {
            "Clingy", "Insensitive", "Sloppy", "Morbid", "Spineless", "Peevish", "Vulnerable", "Aimless", "Hostile", "Demanding"
        };

        int falseCount = 0;
        int trueCount = 0;
        List<int> questionList = new List<int>();

        void StartClicked(object sender, EventArgs args)
        {
            trueButton.IsVisible = true;
            falseButton.IsVisible = true;
            questionArea.IsVisible = true;
            imageSpot.IsVisible = true;
            startButton.IsVisible = false;

            GetNextQuestion();
        }

        void RestartClicked(object sender, EventArgs args)
        {
            trueCount = 0;
            falseCount = 0;
            questionList = new List<int>();
            trueButton.IsVisible = false;
            falseButton.IsVisible = false;
            questionArea.IsVisible = false;
            imageSpot.IsVisible = false;
            startButton.IsVisible = true;
            restartButton.IsVisible = false;
        }

        void FalseClicked(object sender, EventArgs args)
        {
            falseCount++;
            CheckForFiveQuestions();
        }
        void TrueClicked(object sender, EventArgs args)
        {
            trueCount++;
            CheckForFiveQuestions();
        }

        void GetNextQuestion()
        {
            int questionNumber = GenerateNonRepeatingRandomNumber(6, questionList);
            questionArea.Text = questions[questionNumber].Text;
            imageSpot.Source = questions[questionNumber].Image;
        }

        void CheckForFiveQuestions()
        {
            int questionCount = trueCount + falseCount;
            if (questionCount == 5)
            {
                trueButton.IsVisible = false;
                falseButton.IsVisible = false;

                GeneratePersonality();
            }
            else
            {
                GetNextQuestion();
            }
        }

        void GeneratePersonality()
        {
            string personality = "";
            for (int i = 0; i < trueCount; i++)
            {
                int number = GenerateNonRepeatingRandomNumber(10, new List<int>());
                personality += (trueTraits[number] + ", ");
            }
            for (int j = 0; j < falseCount; j++)
            {
                int number = GenerateNonRepeatingRandomNumber(10, new List<int>());
                personality += (falseTraits[number] + ", ");
            }

            questionArea.Text = "You are a " + personality + " person.";
            imageSpot.Source = "angel_demon.png";
            restartButton.IsVisible = true;
        }

        int GenerateNonRepeatingRandomNumber(int range, List<int> listNumbers)
        {
            Random rng = new Random();
            int number;
            do
            {
                number = rng.Next(range);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
            return number;
        }

    }
}
