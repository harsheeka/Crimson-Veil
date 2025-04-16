public class Scene
{
    public string Name { get; private set; }
    public Scene(string name) => Name = name;
    public override string ToString() => Name;
}
