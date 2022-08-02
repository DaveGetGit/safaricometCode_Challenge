namespace StudentRegistrarApi.ApiModels.Update
{
    public class UpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }
    }
}
