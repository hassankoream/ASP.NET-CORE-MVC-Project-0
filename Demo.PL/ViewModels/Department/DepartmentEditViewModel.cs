namespace Demo.PL.ViewModels.Department
{
    public class DepartmentEditViewModel
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
