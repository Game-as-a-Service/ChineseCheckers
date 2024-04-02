namespace ChineseCheckers.Domain;

public class Player(string name)
{
    public string Name { get; private set; } = name;
}
