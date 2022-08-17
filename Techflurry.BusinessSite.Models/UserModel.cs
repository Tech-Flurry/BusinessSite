namespace TechFlurry.BusinessSite.Models
{
    public class UserModel : DocumentModel
    {
        public UserModel()
        {
            InitValues();
        }
        public UserType UserType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }

        protected virtual void InitValues()
        {
            UserType = UserType.Basic;
        }
    }
    public class AuthorModel : UserModel
    {
        public string Bio { get; set; }
        public string NickName { get; set; }

        protected override void InitValues()
        {
            UserType = UserType.Author;
        }
    }
    public enum UserType
    {
        Basic,
        Author
    }
}
