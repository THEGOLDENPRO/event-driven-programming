using Godot;

public partial class BeginButton : Godot.Button
{
	private Node calculateScene;

	public override void _Ready()
	{
		calculateScene = ResourceLoader.Load<PackedScene>("res://scenes/calculate.tscn").Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_pressed()
	{
		GetTree().Root.AddChild(calculateScene);
		GetTree().Root.RemoveChild(GetTree().Root.GetChild(0));
	}

}
