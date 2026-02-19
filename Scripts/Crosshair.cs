using Godot;

public partial class Crosshair : Control
{
    [Export] public float LineLength { get; set; } = 15.0F;
    [Export] public float LineWidth { get; set; } = 1.5F;
    [Export] public Color LineColor { get; set; } = Color.Color8(255, 255, 255);

    public override void _Draw()
    {
        var halfLength = LineLength / 2.0F;

        var top = new Vector2(0, -halfLength);
        var bottom = new Vector2(0, +halfLength);

        var left = new Vector2(-halfLength, 0);
        var right = new Vector2(+halfLength, 0);

        DrawLine(top, bottom, LineColor, LineWidth);
        DrawLine(left, right, LineColor, LineWidth);
    }
}
