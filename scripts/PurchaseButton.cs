using System;
using Godot;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.IO;

public partial class PurchaseButton : Button
{
	public Node thankYouScene;

	private Label validationStatus;

	public override void _Ready()
	{
		validationStatus = (Label) GetParent().GetChild(-1);
		// thankYouScene = ResourceLoader.Load<PackedScene>("res://scenes/thank_you.tscn").Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private Tuple<bool, string> AreDetailsValid(
		string name, 
		string email, 
		string phoneNumber, 
		string cardNumber, 
		double cardExpiringDateMonth, 
		double cardExpiringDateYear, 
		string cardCSV
	)
	{
		Regex nameRegex = new Regex(@"^[a-zA-Z]+$");
		Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
		Regex phoneRegex = new Regex(@"^\+?[0-9\s-]{7,}$");
		Regex cardNumberRegex = new Regex(@"^(\d{4}\s){3}\d{4}$");
		Regex cardExpiringDateMonthRegex = new Regex(@"");

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

		if (!cardNumberRegex.IsMatch(cardNumber)) {
			return Tuple.Create(
				false, "Card number is invalid!"
			);
		}


		return Tuple.Create(true, "All good 👍");
	}

	private void _on_pressed()
	{
		Node parentNode = GetParent();
		Node form = parentNode.GetChild(4);

		LineEdit nameInput = (LineEdit) form.GetChild(0).GetChild(1);
		LineEdit emailInput = (LineEdit) form.GetChild(1).GetChild(1);
		LineEdit phoneInput = (LineEdit) form.GetChild(2).GetChild(1);
		LineEdit cardNumberInput = (LineEdit) form.GetChild(4).GetChild(1);
		SpinBox cardExpiringDateMonthInput = (SpinBox) form.GetChild(5).GetChild(1);
		SpinBox cardExpiringDateYearInput = (SpinBox) form.GetChild(5).GetChild(2);
		LineEdit cardCSVInput = (LineEdit) form.GetChild(6).GetChild(1);

		Tuple<bool, string> validationResult = AreDetailsValid(
			nameInput.Text, 
			emailInput.Text, 
			phoneInput.Text, 
			cardNumberInput.Text, 
			cardExpiringDateMonthInput.Value, 
			cardExpiringDateYearInput.Value, 
			cardCSVInput.Text
		);

		if (validationResult.Item1) {
			validationStatus.AddThemeColorOverride("font_color", new Color(0, 255, 0));
		} else {
			validationStatus.AddThemeColorOverride("font_color", new Color(1, 0.407f, 0.339f));
		}

		validationStatus.Text = validationResult.Item2;

		GD.Print(">> " + validationResult);
	}
}