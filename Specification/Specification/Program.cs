// See https://aka.ms/new-console-template for more information

using Operator;
using Specification;

Console.WriteLine("Hello, World!");


List<Project> projects = new List<Project>()
{
    new Project()
    {
        Id = "1",
        Name ="1",
        ProjectType = ProjectType.A,
        Size = 10
    },
    new Project()
    {
        Id = "2",
        Name ="2",
        ProjectType = ProjectType.B,
        Size = 20
    },
    new Project()
    {
        Id = "3",
        Name ="3",
        ProjectType = ProjectType.C,
        Size = 30
    }
};

//var pis = new ProjectIdSpecification("1");
//Console.WriteLine(pis.IsSatisfiedBy(projects[0]));

//var cc = pis.ToExpression();
//var aa = cc.Compile();
//Console.WriteLine(aa);
//var aaaa= projects.Where(aa).ToList();
//Console.WriteLine(cc);


//var sizeAndType = new ProjectSizeAndTypeSpecification(20,ProjectType.B);
//var pis222 = new ProjectIdSpecification("2");
//var project111 = projects[1];
//Console.WriteLine("ProjectSizeAndTypeSpecification:" + sizeAndType.IsSatisfiedBy(project111));
//Console.WriteLine("ProjectIdSpecification:" + pis222.IsSatisfiedBy(project111));
//var ssssss = sizeAndType.ToExpression();
//var result1 = projects.Where(ssssss.Compile()).ToList();

//------------and
//var sizeSpecification = new ProjectSizeSpecification(20);
//var typeSpecification = new ProjectTypeSpecification(ProjectType.B);

//var and = new AndSpecification<Project>(sizeSpecification, typeSpecification);
//Console.WriteLine("AndSpecification:" + and.IsSatisfiedBy(projects[1]));
//Console.WriteLine("Expression:" + and.ToExpression());


//------------or
var sizeSpecification = new ProjectSizeSpecification(20);
var typeSpecification = new ProjectTypeSpecification(ProjectType.C);

var or = new OrSpecification<Project>(sizeSpecification, typeSpecification);
Console.WriteLine("AndSpecification:" + or.IsSatisfiedBy(projects[1]));
Console.WriteLine("Expression:" + or.ToExpression());

Console.ReadKey();