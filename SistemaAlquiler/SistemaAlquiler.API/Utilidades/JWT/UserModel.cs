using Microsoft.AspNetCore.Identity;

namespace SistemaAlquiler.API.Utilidades.JWT;

public class UserModel: IdentityUser
{
    public string FullName { get; set; }

}
