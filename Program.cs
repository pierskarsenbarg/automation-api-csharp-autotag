using Pulumi.Automation;

var destroy = args.Any() && args[0] == "destroy";

var autotagProgram = PulumiFn.Create<TransformationStack>();

const string ProjectName = "automation-autotags";
const string StackName = "dev";

var stackArgs = new InlineProgramArgs(ProjectName, StackName, autotagProgram);
var stack = await LocalWorkspace.CreateOrSelectStackAsync(stackArgs);

Console.WriteLine("Installing plugins...");
await stack.Workspace.InstallPluginAsync("aws", "v5.29.1");

Console.WriteLine("Setting config...");
await stack.SetConfigAsync("aws:region", new ConfigValue("eu-west-2"));

await stack.RefreshAsync(new RefreshOptions { OnStandardOutput = Console.WriteLine });

if (destroy)
{
    Console.WriteLine("destroying stack...");
    await stack.DestroyAsync(new DestroyOptions { OnStandardOutput = Console.WriteLine });
    Console.WriteLine("stack destroy complete");
}
else
{
    Console.WriteLine("updating stack...");
    var result = await stack.UpAsync(new UpOptions { OnStandardOutput = Console.WriteLine });

    if (result.Summary.ResourceChanges != null)
    {
        Console.WriteLine("update summary:");
        foreach (var change in result.Summary.ResourceChanges)
            Console.WriteLine($"    {change.Key}: {change.Value}");
    }
    Console.WriteLine($"Bucket Name: {result.Outputs["BucketName"].Value}");

}


