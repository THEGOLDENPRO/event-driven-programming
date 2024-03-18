using System;
using Godot;

public partial class SubmitButton : Button
{
	private const int wallpaperCost = 10; // £10 per wallpaper
	private const int wallpaperWidth = 50;
	private const int wallpaperLength = 1000;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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

	private void _on_pressed()
	{
		Node parent_node = GetParent();

		LengthTextInput length_input = (LengthTextInput) parent_node.GetChild(0).GetChild(1);
		WidthTextInput width_input = (WidthTextInput) parent_node.GetChild(1).GetChild(1);

		int wallpaperLength = int.Parse(length_input.GetLineEdit().Text);
		int wallpaperWidth = int.Parse(width_input.GetLineEdit().Text); // This is in cm so we'll have to convert it to meters for the calculate function.

		int wallpaperNeeded = calculate_number_of_wallpapers(wallpaperLength, wallpaperWidth * 100);

		int totalCost = wallpaperCost * wallpaperNeeded;

		GD.Print(
			"wallpapers needed: " + wallpaperNeeded
		);
	}
}
