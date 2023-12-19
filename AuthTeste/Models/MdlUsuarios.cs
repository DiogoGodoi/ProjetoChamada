namespace AuthTeste.Models
{
	public class MdlUsuarios
	{
		    public string Id { get; set; }
			public string? Email { get; set; }
			public string? UserName { get; set; }
			public IEnumerable<string>? Roles { get; set; }
	}
}
