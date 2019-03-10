namespace WotBlitzStatician.JwtSecurity
{
  public interface ISecurityConfiguration
  {
    string Secret { get; set; }
    string SecureWord { get; set; }
  }
}
