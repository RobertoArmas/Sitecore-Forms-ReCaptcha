using System.Threading.Tasks;

namespace SitecoreModules.Foundation.Forms.Services
{
    public interface IReCaptchaService
    {
        Task<bool> Verify(string response, bool isV3);
        bool VerifySync(string response, bool isV3 = true);
    }
}
