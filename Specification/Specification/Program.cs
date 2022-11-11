// See https://aka.ms/new-console-template for more information

using AbpSpecification;
using Operator;
using Specification;
using Specification.AbpSpecification;

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
    },
    new Project()
    {
        Id = "4",
        Name ="4",
        ProjectType = ProjectType.C,
        Size = 20
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


////------------or
//var sizeSpecification = new ProjectSizeSpecification(20);
//var typeSpecification = new ProjectTypeSpecification(ProjectType.C);

//var or = new OrSpecification<Project>(sizeSpecification, typeSpecification);
//Console.WriteLine("AndSpecification:" + or.IsSatisfiedBy(projects[1]));
//Console.WriteLine("Expression:" + or.ToExpression());

////------------Identity
//Console.WriteLine("Expression:" + ProjectSizeSpecification.Identity.ToExpression());
////------------None
//Console.WriteLine("Expression:" + ProjectSizeSpecification.None.ToExpression());

//------------AndNotSpecification
var size = new AbpProjectSizeSpecification(20);
var type = new AbpProjectTypeSpecification(ProjectType.A);

var andNot = new AndNotSpecification<Project>(size, type);
var expression = andNot.ToExpression();
Console.WriteLine("Expression:" + expression);
var result= projects.Where(expression.Compile()).ToList();

var type2 = new AbpProjectTypeSpecification(ProjectType.B);
var andNot2 = new AndNotSpecification<Project>(size, type2);
var result2 = projects.Where(andNot2.ToExpression().Compile()).ToList();

Console.ReadKey();