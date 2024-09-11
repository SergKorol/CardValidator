namespace CardValidator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnSubmitClicked(object sender, EventArgs e)
    {
        string cardNumber1 = CardNumberEntry1.Text;
        string cardNumber2 = CardNumberEntry2.Text;
        string cardNumber3 = CardNumberEntry3.Text;
        string cardNumber4 = CardNumberEntry4.Text;

        string fullCardNumber = $"{cardNumber1}{cardNumber2}{cardNumber3}{cardNumber4}";

        if (fullCardNumber.Length == 16)
        {
            if (IsValidCardNumber(fullCardNumber))
            {
                DisplayAlert("Success", $"Card Number: {fullCardNumber} submitted.", "OK");
            }
            else
            {
                DisplayAlert("Error", "The card number is invalid.", "OK");
            }
        }
        else
        {
            DisplayAlert("Error", "Please enter all 16 digits of the card number.", "OK");
        }
    }

    private bool IsValidCardNumber(string cardNumber)
    {
        int sum = 0;
        bool alternate = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int n = int.Parse(cardNumber[i].ToString());

            if (alternate)
            {
                n *= 2;
                if (n > 9)
                {
                    n -= 9;
                }
            }

            sum += n;
            alternate = !alternate;
        }

        return (sum % 10 == 0);
    }

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry)
        {
            entry.Text = string.Empty;
        }
    }

    private void OnCardNumberTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not Entry entry) return;

        if (entry.Text.Length != entry.MaxLength) return;

        if (entry == CardNumberEntry1)
        {
            CardNumberEntry2.Focus();
        }
        else if (entry == CardNumberEntry2)
        {
            CardNumberEntry3.Focus();
        }
        else if (entry == CardNumberEntry3)
        {
            CardNumberEntry4.Focus();
        }
        else if (entry == CardNumberEntry4)
        {
            CardNumberEntry4.Unfocus();
        }
    }
}