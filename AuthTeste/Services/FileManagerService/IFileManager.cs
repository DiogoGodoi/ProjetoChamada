using AuthTeste.Models;

namespace AuthTeste.Services.UploadFileService
{
	public interface IFileManager
	{
		public int CreateFile(MdlEscola escola, IFormFile arquivo, string caminho);
		bool DeleteFile(string urlImage, string caminho);
	}
}
