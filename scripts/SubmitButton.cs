using System;
using Godot;
using System.Text.RegularExpressions;

public partial class SubmitButton : Button
{
	private const int wallpaperCost = 10; // £10 per wallpaper
	private const int wallpaperWidth = 50;
	private const int wallpaperLength = 1000;
	public Node purchaseScene;

	public override void _Ready()
	{
		purchaseScene = ResourceLoader.Load<PackedScene>("res://scenes/purchase.tscn").Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private int CalculateNumberOfWallpapers(int length, int width)
	{
		int areaOfEachWallpaper = wallpaperWidth * wallpaperLength;

		int areaOfWallpaperRequested = length * width;

		// The number of wallpapers needed, rounding up to make sure we cover the entire wall.
		int numberOfWallpapersNeeded = (int) Math.Ceiling((double) areaOfWallpaperRequested / areaOfEachWallpaper);

		return numberOfWallpapersNeeded;
	}

	private bool IsDetailsValid(string email, string phoneNumber, string address)
	{
		Regex emailRegex = new Regex("");
		Regex phoneNumberRegex = new Regex("");

		if (emailRegex.IsMatch(email) && phoneNumberRegex.IsMatch(phoneNumber)) {
			return true;
		}

		return false;
	}

	private void ShowResult(int wallpapersNeeded, int totalCost) 
	{
		Label WallpapersAmountLabel = (Label)purchaseScene.GetChild(2).GetChild(1);
		Label WallpapersCostLabel = (Label)purchaseScene.GetChild(3).GetChild(1);

		WallpapersAmountLabel.Text = wallpapersNeeded.ToString();
		WallpapersCostLabel.Text = totalCost.ToString();

		GetTree().Root.AddChild(purchaseScene); // Switch to result scene.
		GetTree().Root.RemoveChild(GetTree().Root.GetChild(0));
	}

	private void _on_pressed()
	{
		Node parent_node = GetParent();

		LengthTextInput length_input = (LengthTextInput) parent_node.GetChild(0).GetChild(1);
		WidthTextInput width_input = (WidthTextInput) parent_node.GetChild(1).GetChild(1);

		int wallpaperLength = int.Parse(length_input.GetLineEdit().Text);
		int wallpaperWidth = int.Parse(width_input.GetLineEdit().Text); // This is in cm so we'll have to convert it to meters for the calculate function.

		int wallpapersNeeded = CalculateNumberOfWallpapers(wallpaperLength, wallpaperWidth * 100);

		int totalCost = wallpaperCost * wallpapersNeeded;

		ShowResult(wallpapersNeeded, totalCost);

		//if (IsDetailsValid()) {
		//	ShowResult(wallpapersNeeded, totalCost);
		//}
	}
}
