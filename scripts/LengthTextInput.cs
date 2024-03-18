using Godot;

[Tool]
public partial class LengthTextInput : SpinBox
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetLineEdit().AddThemeFontSizeOverride("font_size", 40);
	}

}
