﻿using FitnessApp.SignUpPages;
using FitnessApp.SQLserver;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace FitnessApp.Windows
{
    /// <summary>
    /// Interaction logic for SigningWindow.xaml
    /// </summary>
    public partial class SigningWindow : Window
    {
        public static SigningWindow    SigningWindowObject;
        public static SignUpFirstPage  SignUpFirstPageObject;
        public static SignUpSecondPage SignUpSecondPageObject;
        public static SetUpProfilePage SetUpProfilePageObject;
        

        public SigningWindow()
        {
            InitializeComponent();
            SigningWindowObject = this;

            // Initialize UserWindowPages Objects
            SignUpFirstPageObject  = new SignUpFirstPage();
            SignUpSecondPageObject = new SignUpSecondPage();
            SetUpProfilePageObject = new SetUpProfilePage();

            // Intialize ErrorMessagesQueue and Assign it to ErrorsSnackbar's MessageQueue
            var ErrorMessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
            ErrorsSnackbar.MessageQueue = ErrorMessagesQueue;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {

            bool isAccountFound = Database.IsUserFound(EmailSignInTextBox.Text, PasswordSignInTextBox.Password);

            if (isAccountFound == true)
            {
                if (Database.accountType == "User")
                {
                    // Open User Main Window
                    UserWindow UserWindowTemp = new UserWindow(Database.accountID);
                    UserWindowTemp.Show();
                }
                else
                {
                    // Open Admin Main Window
                    AdminWindow AdminWindowTemp = new AdminWindow(Database.accountID);
                    AdminWindowTemp.Show();
                }

                // Close Signing Window
                Close();
            }
            else
            {
                // Display error when the user is not found
                ErrorsSnackbar.MessageQueue.Enqueue("Incorrect Email Or Password");
            }

        }


        private void CreateAnAccountButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpPagesFrame.NavigationService.Navigate(SigningWindow.SignUpFirstPageObject);
        }
    }
}
