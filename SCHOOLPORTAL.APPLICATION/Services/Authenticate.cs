namespace Application;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using SeedWork;
using Infrastructure;
using Entities;
using Specifications;
public class Authenticate
{
    private readonly IGenericRepository<Student> _studentRepository;
    private readonly IConfiguration _configuration;
    public Authenticate(IGenericRepository<Student> studentRepository, IConfiguration configuration)
    {
        _studentRepository = studentRepository;
        _configuration= configuration;
    }
   public async Task<IResponseData> CreateToken(string email, string password)
   {
     var spec = new BaseSpecification<Student>(s=>s.Email.ToLower() == email.ToLower() && s.Password == password);
     var student = await _studentRepository.GetAsync(spec);
     if (student == null) return new Error{detail = "User not found"};

     var claims = new List<Claim>
     {
        new Claim(ClaimTypes.Name,student.Id.ToString()),
        new Claim(ClaimTypes.Email,student.Email)
     };

     var key = Convert.FromBase64String(_configuration["JWT:KEY"]);
     int exp = int.Parse(_configuration["JWT:EXP"]);

     var tokenHandler = new JwtSecurityTokenHandler();
     var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
     {
       Subject = new ClaimsIdentity(claims.ToArray()),
       Expires = DateTime.UtcNow.AddMinutes(exp),
       SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
       Issuer = _configuration["JWT:ISSUER"],
       Audience = _configuration["JWT:AUDIENCE"]
     });

    string jwt = tokenHandler.WriteToken(securityToken);
    return new Success{detail = jwt};

   }
}