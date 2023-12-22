namespace AuthTeste.Models.ModelsIdentity
{
    public class MdlUsuariosRoles
    {
        public string Id { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public bool EmailConfirmed { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
