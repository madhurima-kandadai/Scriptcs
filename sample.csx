#r "System.Text.Encoding.dll"
#r "System.Threading.Tasks.dll"
<<<<<<< HEAD
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

Console.WriteLine("Please enter a value");
var x = Convert.ToInt32(Console.ReadLine());
Program test = new Program();
var value = test.GetValue(x);
//System.Console.WriteLine(value);
foreach (var item in value)
{
    System.Console.WriteLine(item);
}
//Console.WriteLine("the value is " + Program.GetValue(x));

=======

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
>>>>>>> 9f6b866b90abe087cfb7b014746956d2b4b6401f

public class Program
    {
        public static string data { get; set; }
<<<<<<< HEAD
        public IEnumerable<string> GetValue(int id)
    {
            var currentNamespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
            IList<string> classNames = new List<string>();
            var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"D:\Roslyn\Roslyn Latest\RoslynCodeGen\ConsoleApp3\src\ConsoleApp3\Entities\")).Where(x => Path.GetExtension(x) == ".cs");
=======
        public static int GetValue(int id)
        {
            var currentNamespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
            IList<string> classNames = new List<string>();
            var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"C:\Users\user\Desktop\RoslynCodeGen\ConsoleApp3\src\ConsoleApp3\Entities\")).Where(x => Path.GetExtension(x) == ".cs");
>>>>>>> 9f6b866b90abe087cfb7b014746956d2b4b6401f
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
<<<<<<< HEAD
            return list;
    }

        public void CreateInterface(IList<string> classNames, string currentNamespace)
=======
			return id++;
			
        }

        public static void CreateInterface(IList<string> classNames, string currentNamespace)
>>>>>>> 9f6b866b90abe087cfb7b014746956d2b4b6401f
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
<<<<<<< HEAD
                var path = Path.GetFullPath(@"D:\Roslyn\Scriptcs\Generated");
                var logPath = Path.GetFullPath(@"D:\Roslyn\Scriptcs\Generated\" + "I" + className + "Repository" + ".cs");
=======
                var path = Path.GetFullPath(@"C:\Users\user\Desktop\Scriptcs\Generated");
                var logPath = Path.GetFullPath("Generated\\" + "I" + className + "Repository" + ".cs");
>>>>>>> 9f6b866b90abe087cfb7b014746956d2b4b6401f
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

<<<<<<< HEAD
        public void CreateClass(IList<string> classNames, string currentNamespace)
=======
        public static void CreateClass(IList<string> classNames, string currentNamespace)
>>>>>>> 9f6b866b90abe087cfb7b014746956d2b4b6401f
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
<<<<<<< HEAD
                var targetFolder = @"D:\Roslyn\Scriptcs\";
=======
                var targetFolder = @"C:\Users\user\Desktop\Scriptcs\";
>>>>>>> 9f6b866b90abe087cfb7b014746956d2b4b6401f
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