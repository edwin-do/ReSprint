using NationalInstruments.Restricted;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FinalSprint.src.Classes
{
    internal class UserInputValidation
    {
        public UserInputValidation()
        {

        }

        public UserInput checkUserInput(string userName, string userSampleName, double userSampleLength, double userSampleWidth, double userSampleThickness)
        {


            bool UserDataIsValid = validateUserData(userName, userSampleName, userSampleLength, userSampleWidth, userSampleThickness);

            if (UserDataIsValid)
            {
                return new UserInput
                {
                    UserName = userName,
                    UserSampleName = userSampleName,
                    UserSampleLength = userSampleLength,
                    UserSampleWidth = userSampleWidth,
                    UserSampleThickness = userSampleThickness
                };
            }
            return new UserInput { };


        }

        public bool checkInputBox(TextBox input)
        {

            if (string.IsNullOrEmpty(input.Text))
            {
                MessageBox.Show("Please enter " + input.Name + " in the textbox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public bool validateUserData(string userName, string userSampleName, double userSampleLength, double userSampleWidth, double userSampleThickness)
        {
            if (userName is not string && userSampleName is not string)
            {
                return false;
            }
            if (userSampleLength <= 0 && userSampleWidth <= 0 && userSampleThickness <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
