namespace ComputedColumns.POC.EntityFramework.Entities
{
    public class Animal
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Breed { get; set; }

        public string? RegistrationCode { get; set; }

        public string? OwnerName { get; set; }

        public string? Description { get; set; }
        
        public string? ComputedProperty { get; }
    }
}