#r "System.Composition.TypedParts.dll"
#r "System.Composition.Hosting.dll"
#r "System.Composition.Runtime.dll"
#r "System.Composition.AttributedModel.dll"
#r "System.Runtime.dll"
#r "System.Text.Encoding.dll"
#r "System.Threading.Tasks.dll"
#r "Microsoft.CodeAnalysis.dll"
#r "Microsoft.CodeAnalysis.CSharp.dll"
#r "Microsoft.CodeAnalysis.Workspaces.dll"

using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.SourceGenerator;
using Microsoft.CodeAnalysis.Editing;


Test test = new Test();
test.GetValue();

public class Test
{
    public static string data { get; set; }

    public void GetValue()
    {
        try
        {
            Console.WriteLine("Code Generation Started");
            var currentNamespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
            IList<string> classNames = new List<string>();
            var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"c:\users\mkandadai\desktop\roslyncodegen\consoleapp3\src\consoleapp3\Entities\")).Where(x => Path.GetExtension(x) == ".cs");
            var text = list.Select(x => CSharpSyntaxTree.ParseText(File.ReadAllText(x))).Cast<CSharpSyntaxTree>();
            Console.WriteLine("The classes available are as below");
            foreach (CSharpSyntaxTree syntaxTree in text)
            {
                var root = (CompilationUnitSyntax)syntaxTree.GetRoot();
                var sd = syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
                foreach (var item in sd)
                {
                    Console.WriteLine(item.Identifier.ToString());
                    classNames.Add(item.Identifier.ToString());
                }
            }
            Console.WriteLine("Creating Interfaces");
            CreateInterface(classNames, currentNamespace);
        }
        catch (Exception ex)
        {
            if (ex is System.Reflection.ReflectionTypeLoadException)
            {
                var typeLoadException = ex as ReflectionTypeLoadException;
                var loaderExceptions = typeLoadException.LoaderExceptions;
                Console.WriteLine("Loader Exceptions are as below:");
                foreach (var item in loaderExceptions)
                {
                    Console.WriteLine(item.Data + " - " + item.Message);
                }
            }
            else
            {
                Console.WriteLine("Following exception occurred: " + ex.Message);
            }
        }

    }

    public void CreateInterface(IList<string> classNames, string currentNamespace)
    {
        var workspace = new AdhocWorkspace();
        //var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
        //var usingSystemDirectives = generator.NamespaceImportDeclaration("System");
        ////var usingSystemGenricDirectives = generator.NamespaceImportDeclaration("System.Generic");
        ////var usingEntities = generator.NamespaceImportDeclaration("Entities");
        //var IRepositoryAsynInterfaceType = generator.IdentifierName("IRepositoryAsync");
        foreach (var className in classNames)
        {
            //var interfaceDeclaration = generator.InterfaceDeclaration("I" + className + "Repository", typeParameters: null,
            //                      accessibility: Accessibility.Public,
            //                      interfaceTypes: new SyntaxNode[] { IRepositoryAsynInterfaceType },
            //                      members: null);
            //var namespaceDeclaration = generator.NamespaceDeclaration(currentNamespace, interfaceDeclaration);
            //var newNode = generator.CompilationUnit(usingSystemDirectives/*, usingSystemGenricDirectives, usingEntities*/, namespaceDeclaration);
            //.NormalizeWhitespace();
            //data = newNode.ToString();
            var path = Path.GetFullPath(@"Generated\");
            var logPath = Path.GetFullPath(@"Generated\\" + "I" + className + "Repository" + ".cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.SetAttributes(path, FileAttributes.Normal);
            var logFile = File.Create(logPath);
            var logWriter = new System.IO.StreamWriter(logFile);
            logWriter.WriteLine(data);
            logWriter.Dispose();
        }
    }
}
