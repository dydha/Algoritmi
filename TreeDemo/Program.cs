
public class Program
{
    public static void Main(string[] args)
    {
        FileService service = FileService.Instance;

        FileNode root1 = new FileNode("Src", true);
        FileTree src = new(root1);
        src.Root.AddChild(new FileNode("API", true));
        src.Root.AddChild(new FileNode("ApplicationLayer", true));
        src.Root.AddChild(new FileNode("Domain", true));
        src.Root.AddChild(new FileNode("Shared", true));
        src.Root.AddChild(new FileNode("file.js", false));

        FileTree tests = new(new FileNode("Tests", true));
        tests.Root.AddChild(new FileNode("EmailService", true));
        tests.Root.AddChild(new FileNode("InfrastructureLayer", true));
        tests.Root.AddChild(new FileNode("PresentationLayer", true));
        tests.Root.AddChild(new FileNode("App.html", false));

        service.AddTree(src);
        service.AddTree(tests);

        service.DisplayTrees();
    }
}
public class FileService
{
    private static Lazy<FileService> _lazy = new Lazy<FileService>(() => new FileService());
    public static FileService Instance = _lazy.Value;

    private static List<FileTree> _trees = [];
    private FileService() { }   
    public void AddTree(FileTree tree)
    {
        if(!_trees.Exists(t => t.Root.Name.Equals(tree.Root.Name, StringComparison.OrdinalIgnoreCase)))
        {
            _trees.Add(tree);
        }
    }
    public void DisplayTrees()
    {
        if(_trees.Count > 0) 
        { 
            _trees.ForEach(t => t.Display(t.Root));
        }
    }
}
public class FileTree
{
    public FileNode Root { get; private set; }

    public FileTree(FileNode root)
    {
        Root = root;
    }

    public void Display(FileNode node, string indent="")
    {
        if (node is null) return;

        Console.WriteLine($"{indent}{node.Name}");
        if (node.Children.Count == 0 || !node.IsDirectory) return;

        foreach(FileNode child in node.Children ) 
        { 
            Display(child, indent+" ");
        }
    }
}
public class FileNode
{
    public string Name { get; private set; }
    public bool IsDirectory { get; private set; }
    public List<FileNode> Children { get; private set; }

    public FileNode(string name, bool isDirectory = true)
    {
        Name = name;
        IsDirectory = isDirectory;
        Children = [];
    }

    public void AddChild(FileNode node)
    {
        if(Children.Exists(n => n.Name.Equals(node.Name, StringComparison.OrdinalIgnoreCase))) 
        {
            return;
        }
        Children.Add(node);
    }
}