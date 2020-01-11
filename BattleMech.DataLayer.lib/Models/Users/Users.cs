namespace BattleMech.DataLayer.PCL.Models.Users {
    public class Users : BaseModel {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public bool Activated { get; set; }
    }
}