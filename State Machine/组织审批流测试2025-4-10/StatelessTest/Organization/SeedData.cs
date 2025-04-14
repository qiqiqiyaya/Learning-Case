using StatelessTest.Organization.Entities;

namespace StatelessTest.Organization
{
    public class SeedData
    {
        public static void Initialize(OrgDbContext context)
        {
            var aaa = new EmployeeEntity { Name = "AAAAAAAAAAAA" };
            context.Employees.Add(aaa);
            var bbb = new EmployeeEntity { Name = "BBBBBBBBBBBB" };
            context.Employees.Add(bbb);
            var ccc = new EmployeeEntity { Name = "CCCCCCCCCCCCCCCC" };
            context.Employees.Add(ccc);
            var ddd = new EmployeeEntity { Name = "DDDDDDDDDDDDDDDD" };
            context.Employees.Add(ddd);
            var eee = new EmployeeEntity { Name = "EEEEEEEEEEEEEEE" };
            context.Employees.Add(eee);
            var fff = new EmployeeEntity { Name = "FFFFFFFFFFFFFFF" };
            context.Employees.Add(eee);

            var org1 = new OrganizationEntity { Name = "Organization 1" };
            context.Organizations.Add(org1);
            var org2 = new OrganizationEntity { Name = "Organization 2", ParentId = org1.Id };
            context.Organizations.Add(org2);
            var org3 = new OrganizationEntity { Name = "Organization 3", ParentId = org2.Id };
            context.Organizations.Add(org3);
            var org4 = new OrganizationEntity { Name = "Organization 4", ParentId = org3.Id };
            context.Organizations.Add(org4);
            var org5 = new OrganizationEntity { Name = "Organization 5", ParentId = org2.Id };
            context.Organizations.Add(org5);

            context.Managers.Add(new ManagerEntity { EmployeeId = aaa.Id, Organization = org1, OrganizationId = org1.Id });
            context.Managers.Add(new ManagerEntity { EmployeeId = bbb.Id, Organization = org2, OrganizationId = org2.Id });
            context.Managers.Add(new ManagerEntity { EmployeeId = ccc.Id, Organization = org3, OrganizationId = org3.Id });
            context.Managers.Add(new ManagerEntity { EmployeeId = ddd.Id, Organization = org4, OrganizationId = org4.Id });
            context.Managers.Add(new ManagerEntity { EmployeeId = eee.Id, Organization = org5, OrganizationId = org5.Id });
            context.Managers.Add(new ManagerEntity { EmployeeId = fff.Id, Organization = org5, OrganizationId = org5.Id });

        }
    }
}
