namespace JWT_Identity_Stage6.Models
{
    public class UserModel
    {
        private object value1;
        private object value2;
        private object value3;

        public UserModel(object value1, object value2, object value3)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
