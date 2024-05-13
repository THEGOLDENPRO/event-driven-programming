using System;
using Godot;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

public partial class PurchaseButton : Button
{
	public Node thankYouScene;

	public override void _Ready()
	{
		// thankYouScene = ResourceLoader.Load<PackedScene>("res://scenes/thank_you.tscn").Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private Tuple<bool, string> AreDetailsValid(string name, string email, string phoneNumber, string address)
	{
		Regex nameRegex = new Regex(@"^[a-zA-Z]+$");
		Regex emailRegex = new Regex("");

		if (!nameRegex.IsMatch(name)) {
			return Tuple.Create(
				false, "Names should only contain lower case and upper case letters. No numbers or special characters."
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
		LineEdit addressInput = (LineEdit) form.GetChild(3).GetChild(1);
		LineEdit cardNumberInput = (LineEdit) form.GetChild(4).GetChild(1);
		LineEdit cardExpiringDateMonthInput = (LineEdit) form.GetChild(5).GetChild(1);
		LineEdit cardExpiringDateYearInput = (LineEdit) form.GetChild(5).GetChild(2);
		LineEdit cardCSVInput = (LineEdit) form.GetChild(6).GetChild(1);

		Tuple<bool, string> validationResult = AreDetailsValid(
			nameInput.Text, emailInput.Text, phoneInput.Text, addressInput.Text
		);

		GD.Print(">> " + validationResult);
	}
}