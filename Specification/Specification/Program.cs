// See https://aka.ms/new-console-template for more information

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

var pis = new ProjectIdSpecification("1");
Console.WriteLine(pis.IsSatisfiedBy(projects[0]));

var cc = pis.ToExpression();
var aa = cc.Compile();
Console.WriteLine(aa);
var aaaa= projects.Where(aa).ToList();
Console.WriteLine(cc);


var sizeAndType = new ProjectSizeAndTypeSpecification(20,ProjectType.B);
var pis222 = new ProjectIdSpecification("2");
var project111 = projects[1];
Console.WriteLine("ProjectSizeAndTypeSpecification:" + pis.IsSatisfiedBy(project111));
Console.WriteLine("ProjectIdSpecification:" + pis222.IsSatisfiedBy(project111));
var ssssss = sizeAndType.ToExpression();
var result1 = projects.Where(ssssss.Compile()).ToList();

Console.ReadKey();