using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class User
{
   public UserId Id { get; private set; }
   public Email Email { get; private set; }
   public Username Username { get; private set; }
   public Password Password { get; private set; }
   public Role Role { get; private set; }
   
   public DateTime CreatedAt { get; private set; }

   public User(UserId id, Email email, Username username, Password password, Role role)
   {
      Id = id;
      Email = email;
      Username = username;
      Password = password;
      Role = role;
   }
}