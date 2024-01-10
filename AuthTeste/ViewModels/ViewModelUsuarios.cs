using AuthTeste.Models;
using AuthTeste.Models.ModelsIdentity;

namespace AuthTeste.ViewModels
{
    public class ViewModelUsuarios
    {
        public MdlForgotPassword mdlForgotPassword { get; set; }
        public MdlResetPassword mdlResetPassword { get; set;}
        public MdlUserAuth mdlUserAuth { get; set; }
        public MdlUsuario mdlUserCreate { get; set; }
        public MdlUsuariosRoles mdlUsersRoles { get; set; }
    }
}
