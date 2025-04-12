namespace StatelessTest.Organization.Entities
{
    public class ManagerEntity
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int OrganizationId { get; set; }

        public OrganizationEntity Organization { get; set; }
    }
}
