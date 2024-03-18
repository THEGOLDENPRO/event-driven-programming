using Godot;

public partial class BeginButton : Godot.Button
{
	public Node calculateWallpaperScene;

	public override void _Ready()
	{
		calculateWallpaperScene = ResourceLoader.Load<PackedScene>("res://scenes/calculate_wallpaper.tscn").Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_pressed()
	{
		GetTree().Root.AddChild(calculateWallpaperScene);
	}

}
