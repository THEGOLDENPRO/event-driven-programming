using System;
using Godot;

public partial class SubmitButton : Button
{
	private const int wallpaperCost = 10; // £10 per wallpaper
	private const int wallpaperWidth = 50;
	private const int wallpaperLength = 1000;

	public Node resultScene;

	public override void _Ready()
	{
		resultScene = ResourceLoader.Load<PackedScene>("res://scenes/result.tscn").Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private int calculate_number_of_wallpapers(int length, int width)
	{
		int areaOfEachWallpaper = wallpaperWidth * wallpaperLength;

        int areaOfWallpaperRequested = length * width;

        // The number of wallpapers needed, rounding up to make sure we cover the entire wall.
        int numberOfWallpapersNeeded = (int) Math.Ceiling((double) areaOfWallpaperRequested / areaOfEachWallpaper);

        return numberOfWallpapersNeeded;
	}

	private void ShowResult(int wallpapersNeeded, int totalCost) 
	{
		Label WallpapersAmountLabel = (Label)resultScene.GetChild(1).GetChild(1);
		WallpapersAmountLabel.Text = wallpapersNeeded.ToString();

		GetTree().Root.AddChild(resultScene); // Switch to result scene.
		GetTree().Root.RemoveChild(GetTree().Root.GetChild(0));

	}

	private void _on_pressed()
	{
		Node parent_node = GetParent();

		LengthTextInput length_input = (LengthTextInput) parent_node.GetChild(0).GetChild(1);
		WidthTextInput width_input = (WidthTextInput) parent_node.GetChild(1).GetChild(1);

		int wallpaperLength = int.Parse(length_input.GetLineEdit().Text);
		int wallpaperWidth = int.Parse(width_input.GetLineEdit().Text); // This is in cm so we'll have to convert it to meters for the calculate function.

		int wallpapersNeeded = calculate_number_of_wallpapers(wallpaperLength, wallpaperWidth * 100);

		int totalCost = wallpaperCost * wallpapersNeeded;

		ShowResult(wallpapersNeeded, totalCost);

		GD.Print(
			"wallpapers needed: " + wallpapersNeeded
		);
	}
}
