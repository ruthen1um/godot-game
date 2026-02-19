using Godot;

public partial class Global : Node
{
    public override void _Ready()
    {
        CaptureCursor();
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
            {
                CaptureCursor();
            }
        }
        else if (@event is InputEventKey key)
        {
            if (key.PhysicalKeycode == Key.Escape && key.Pressed)
            {
                UncaptureCursor();
            }
        }
    }

    private void CaptureCursor()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    private void UncaptureCursor()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }
}
