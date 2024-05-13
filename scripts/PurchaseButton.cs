using System;
using Godot;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class PurchaseButton : Button
{
	public Node thankYouScene;

	private Label validationStatus;

	public override void _Ready()
	{
		validationStatus = (Label) GetParent().GetChild(-1);
		thankYouScene = ResourceLoader.Load<PackedScene>("res://scenes/thank_you.tscn").Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private async void ShowThankYou() 
	{
		await Task.Delay(TimeSpan.FromMilliseconds(3000)); // delay before switching to thank you scene.

		GetTree().Root.AddChild(thankYouScene); // Switch to thank you scene.
		GetTree().Root.RemoveChild(GetTree().Root.GetChild(0));
	}

	private Tuple<bool, string> AreDetailsValid(
		string name, 
		string email, 
		string phoneNumber, 
		string address, 
		string cardNumber, 
		string cardCSV
	)
	{
		Regex emptyStringRegex = new Regex(@"^\s*$");

		Regex nameRegex = new Regex(@"^[a-zA-Z]+$");
		Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
		Regex phoneRegex = new Regex(@"^\+?[0-9\s-]{7,}$");
		Regex cardNumberRegex = new Regex(@"^(\d{4}\s){3}\d{4}$");

		if (!nameRegex.IsMatch(name)) {
			return Tuple.Create(
				false, "Name is invalid! \nIt should only contain lower case and upper case letters. No numbers or special characters."
			);
		}

		if (!emailRegex.IsMatch(email)) {
			return Tuple.Create(
				false, "Email is invalid!"
			);
		}

		if (!phoneRegex.IsMatch(phoneNumber)) {
			return Tuple.Create(
				false, "Phone number is invalid!"
			);
		}

		if (emptyStringRegex.IsMatch(address)) { // return false if address field is empty.
			return Tuple.Create(
				false, "Address field is empty!"
			);
		}

		if (!cardNumberRegex.IsMatch(cardNumber)) {
			return Tuple.Create(
				false, "Card number is invalid!"
			);
		}

		if (emptyStringRegex.IsMatch(cardCSV)) {
			return Tuple.Create(
				false, "Card CSV is not present!"
			);
		}

		return Tuple.Create(true, "All good 👍");
	}

	private void _on_pressed()
	{
		Node parentNode = GetParent();
		Node form = parentNode.GetChild(5);

		LineEdit nameInput = (LineEdit) form.GetChild(0).GetChild(1);
		LineEdit emailInput = (LineEdit) form.GetChild(1).GetChild(1);
		LineEdit phoneInput = (LineEdit) form.GetChild(2).GetChild(1);
		LineEdit addressInput = (LineEdit) form.GetChild(3).GetChild(1);
		LineEdit cardNumberInput = (LineEdit) form.GetChild(4).GetChild(1);
		// SpinBox cardExpiringDateMonthInput = (SpinBox) form.GetChild(5).GetChild(1);
		// SpinBox cardExpiringDateYearInput = (SpinBox) form.GetChild(5).GetChild(2);
		LineEdit cardCSVInput = (LineEdit) form.GetChild(6).GetChild(1);

		Tuple<bool, string> validationResult = AreDetailsValid(
			nameInput.Text, 
			emailInput.Text, 
			phoneInput.Text, 
			addressInput.Text, 
			cardNumberInput.Text, 
			cardCSVInput.Text
		);

		(bool isValid, string message) = validationResult;

		if (isValid) {
			validationStatus.AddThemeColorOverride("font_color", new Color(0, 255, 0));
		} else {
			validationStatus.AddThemeColorOverride("font_color", new Color(1, 0.407f, 0.339f));
		}

		validationStatus.Text = message;

		GD.Print(">> " + validationResult);

		if (isValid) {
			ShowThankYou();
		}

	}

}