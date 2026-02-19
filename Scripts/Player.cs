using Godot;

public partial class Player : CharacterBody3D
{
    [Export] public float MoveSpeed { get; set; } = 1.0F;
    [Export] public float RotationSpeed { get; set; } = 1.0F;
    [Export] public float SpawnDistance { get; set; } = 1.0F;
    [Export] public PackedScene ObjectScene { get; set; }

    public override void _Ready()
    {
        _camera = GetNode<Camera3D>("Camera3D");
    }

    public override void _PhysicsProcess(double delta)
    {
        var inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

        var direction = new Vector3(inputDir.X, 0, inputDir.Y).Normalized();
        direction = direction.Rotated(Vector3.Up, Rotation.Y);

        Velocity = direction * MoveSpeed * MoveSpeedScale * (float)delta;

        MoveAndSlide();
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            var playerRot = Rotation;
            playerRot.Y -= mouseMotion.Relative.X * RotationSpeed * RotSpeedScale;
            Rotation = playerRot;

            var cameraRot = _camera.Rotation;
            cameraRot.X -= mouseMotion.Relative.Y * RotationSpeed * RotSpeedScale;
            cameraRot.X = Mathf.Clamp(cameraRot.X, -Mathf.Pi / 6, Mathf.Pi / 6);
            _camera.Rotation = cameraRot;
        }
        else if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
            {
                SpawnObjectAtLookPosition();
            }
        }
    }

    private Camera3D _camera;
    private const float RotSpeedScale = 0.002f;
    private const float MoveSpeedScale = 60f;

    private void SpawnObjectAtLookPosition()
    {
        var spawnPos = _camera.GlobalTransform.Origin + _camera.GlobalTransform.Basis.Z * -SpawnDistance;
        var node = ObjectScene.Instantiate<Node3D>();
        GetTree().Root.AddChild(node);
        node.Position = spawnPos;
    }
}
