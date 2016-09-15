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

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

Console.WriteLine("Please enter a value");
var x = Convert.ToInt32(Console.ReadLine());
Test test = new Test();
Console.WriteLine("the value is " + Test.GetValue(x));

public class Test{
	public static string data { get; set; }
	
	public static int GetValue(int id)
	{
			var currentNamespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
				IList<string> classNames = new List<string>();            
				var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"c:\users\mkandadai\desktop\roslyncodegen\consoleapp3\src\consoleapp3\Entities\")).Where(x => Path.GetExtension(x) == ".cs");				
				var text = list.Select(x => CSharpSyntaxTree.ParseText(File.ReadAllText(x))).Cast<CSharpSyntaxTree>();
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
				CreateInterface(classNames, currentNamespace);
				return ++id;
				}	
	
		public static void CreateInterface(IList<string> classNames, string currentNamespace)
        {
            //var workspace = new AdhocWorkspace();
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
                var path = Path.GetFullPath(@"D:\HyperExamples\RoslynCodeGen\RoslynCodeGen\Generated");
                var logPath = Path.GetFullPath(@"D:\HyperExamples\RoslynCodeGen\RoslynCodeGen\Generated\\" + "I" + className + "Repository" + ".cs");
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
