using AuthTeste.Models;

namespace AuthTeste.Services.UploadFileService
{
	public interface IFileManager
	{
		public int CreateFile(MdlEscola escola, IFormFile arquivo, string caminho);
		bool DeleteFile(MdlEscola escola, string caminho);
		bool UpdateFile(MdlEscola escola, IFormFile arquivo, string caminho);

	}
}
