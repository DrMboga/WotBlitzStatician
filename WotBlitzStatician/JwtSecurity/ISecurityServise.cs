namespace WotBlitzStatician.JwtSecurity
{
  public interface ISecurityServise
  {
    string Authenticate(string secureWord);
  }
}
