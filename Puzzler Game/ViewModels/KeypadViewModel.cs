#nullable disable
using System.Windows.Input;

namespace Puzzler_Game;
class KeypadViewModel : ViewModel
{
    private string inputString = "";
    private string displayText = "";
    private readonly char[] specialChars = { '*', '#' };

    #region Add Character
    private ICommand addCharCommand;
    public ICommand AddCharCommand
    {
        get
        {
            if (addCharCommand == null)
            {
                addCharCommand = new Command<string>(DoAddChar);
            }
            return addCharCommand;
        }
    }
    void DoAddChar(string key)
    {
        InputString += key;
    }
    #endregion

    #region Delete Character
    private ICommand deleteCharCommand;
    public ICommand DeleteCharCommand
    {
        get
        {
            if(deleteCharCommand == null)
            {
                deleteCharCommand = new Command(DoDeleteCharChar,canExecute: CanDelete);
            }
            return deleteCharCommand;
        }
    }

    void DoDeleteCharChar()
    {
        InputString = InputString.Substring(0, InputString.Length - 1);
    }

    bool CanDelete()
    {
        return InputString.Length > 0;
    }
    #endregion


    public string InputString
    {
        get => inputString;
        private set
        {
            if (inputString != value)
            {
                inputString = value;
                OnPropertyChanged();
                DisplayText = FormatText(inputString);

                // Perhaps the delete button must be enabled/disabled.
                ((Command)DeleteCharCommand).ChangeCanExecute();
            }
        }
    }

    public string DisplayText
    {
        get => displayText;
        private set
        {
            if (displayText != value)
            {
                displayText = value;
                OnPropertyChanged();
            }
        }
    }

    string FormatText(string str)
    {
        bool hasNonNumbers = str.IndexOfAny(specialChars) != -1;
        string formatted = str;

        // Format the string based on the type of data and the length
        if (hasNonNumbers || str.Length < 4 || str.Length > 10)
        {
            // Special characters exist, or the string is too small or large for special formatting
            // Do nothing
        }

        else if (str.Length < 8)
            formatted = string.Format("{0}-{1}", str.Substring(0, 3), str.Substring(3));

        else
            formatted = string.Format("({0}) {1}-{2}", str.Substring(0, 3), str.Substring(3, 3), str.Substring(6));

        return formatted;
    }
}