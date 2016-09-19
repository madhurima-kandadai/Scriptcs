#r "System.Text.Encoding.dll"
#r "System.Threading.Tasks.dll"

using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.SourceGenerator;
using Microsoft.CodeAnalysis.Editing;

Console.WriteLine("Enter a number");
var x  =  Console.Read();
Program p = new Program();
Console.WriteLine("The Value is", Program.GetValue(x));

public class Program
    {
        public static string data { get; set; }
		
        public IEnumerable<string> GetValue(int id)
    {
            var currentNamespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
            IList<string> classNames = new List<string>();
            var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"D:\Roslyn\Roslyn Latest\RoslynCodeGen\ConsoleApp3\src\ConsoleApp3\Entities\")).Where(x => Path.GetExtension(x) == ".cs");
            var currentNamespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
            IList<string> classNames = new List<string>();
            var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"C:\Users\user\Desktop\RoslynCodeGen\ConsoleApp3\src\ConsoleApp3\Entities\")).Where(x => Path.GetExtension(x) == ".cs");
            var text = list.Select(x => CSharpSyntaxTree.ParseText(File.ReadAllText(x))).Cast<CSharpSyntaxTree>();
            foreach (CSharpSyntaxTree syntaxTree in text)
            {
                var root = (CompilationUnitSyntax)syntaxTree.GetRoot();
                var sd = syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
                foreach (var item in sd)
                {
                    classNames.Add(item.Identifier.ToString());
                }
            }
            CreateInterface(classNames, currentNamespace);
            CreateClass(classNames, currentNamespace);
            return list;
    }

        public static void CreateInterface(IList<string> classNames, string currentNamespace)
        {

            var workspace = new AdhocWorkspace();
            var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
            var usingSystemDirectives = generator.NamespaceImportDeclaration("System");
            //var usingSystemGenricDirectives = generator.NamespaceImportDeclaration("System.Generic");
            //var usingEntities = generator.NamespaceImportDeclaration("Entities");
            var IRepositoryAsynInterfaceType = generator.IdentifierName("IRepositoryAsync");
            foreach (var className in classNames)
            {
                var interfaceDeclaration = generator.InterfaceDeclaration("I" + className + "Repository", typeParameters: null,
                                      accessibility: Accessibility.Public,
                                      interfaceTypes: new SyntaxNode[] { IRepositoryAsynInterfaceType },
                                      members: null);
                var namespaceDeclaration = generator.NamespaceDeclaration(currentNamespace, interfaceDeclaration);
                var newNode = generator.CompilationUnit(usingSystemDirectives/*, usingSystemGenricDirectives, usingEntities*/, namespaceDeclaration).
                              NormalizeWhitespace();
                data = newNode.ToString();
                var path = Path.GetFullPath(@"C:\Users\user\Desktop\Scriptcs\Generated");
                var logPath = Path.GetFullPath("Generated\\" + "I" + className + "Repository" + ".cs");
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

        public static void CreateClass(IList<string> classNames, string currentNamespace)
        {
            var workspace = new AdhocWorkspace();
            var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
            var usingSystemDirectives = generator.NamespaceImportDeclaration("System");
            //var usingSystemGenricDirectives = generator.NamespaceImportDeclaration("System.Generic");
            //var usingEntities = generator.NamespaceImportDeclaration("EntityProjectName");
            var IDataContextType = generator.IdentifierName("IDataContext");
            var IUnitofWorkType = generator.IdentifierName("IUnitofWork");
            var constructorParameters = new SyntaxNode[] {
                                            generator.ParameterDeclaration("context", IDataContextType),

                            generator.ParameterDeclaration("unitofWork",IUnitofWorkType) };
            foreach (var className in classNames)
            {
                var constructor = generator.ConstructorDeclaration(className,
                                         constructorParameters, Accessibility.Public,
                                         statements: null);
                var members = new SyntaxNode[] { constructor };
                var IRepositoryInterfaceType = generator.IdentifierName("I" + className + "Repository");
                var classDeclaration = generator.ClassDeclaration(className + "Repository", typeParameters: null,
                                          accessibility: Accessibility.Public,
                                          interfaceTypes: new SyntaxNode[] { IRepositoryInterfaceType },
                                          members: members);
                var namespaceDeclaration = generator.NamespaceDeclaration(currentNamespace, classDeclaration);
                var newNode = generator.CompilationUnit(usingSystemDirectives/*, usingSystemGenricDirectives, usingEntities*/, namespaceDeclaration).
                              NormalizeWhitespace();
                data = newNode.ToString();
                var targetFolder = @"D:\Roslyn\Scriptcs\";
                var path = Path.GetFullPath(targetFolder + @"Generated/");
                var logPath = Path.GetFullPath(path + "\\" + className + "Repository" + ".cs");
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