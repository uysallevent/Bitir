namespace AuthModule.Dtos
{
    public class Claims
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
