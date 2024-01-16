using AuthTeste.Models;

namespace AuthTeste.Services.UploadFileService
{
	public class FileManager : IFileManager
	{
		private readonly string caminhoServer;
		public FileManager(IWebHostEnvironment _webHostEnvironment)
		{
			caminhoServer = _webHostEnvironment.WebRootPath;
		}
		public int CreateFile(MdlEscola escola, IFormFile arquivo, string caminho)
		{
			try
			{
				if (arquivo != null)
				{
					var caminhoRelativo = caminhoServer + caminho;
					var nomeArquivo = Guid.NewGuid().ToString() + arquivo.FileName;

					if (!Directory.Exists(caminhoRelativo))
					{
						Directory.CreateDirectory(caminhoRelativo);
					}

					if (arquivo.FileName.Contains(".jpg") || arquivo.FileName.Contains(".png") || arquivo.FileName.Contains(".jpeg"))
					{
						using (var stream = File.Create(caminhoRelativo + nomeArquivo))
						{
							arquivo.CopyTo(stream);
							escola.UrlImage = nomeArquivo;
							return 3;
						}
					}
					else
					{
						return 2;
					}
				}
				else
				{
					return 1;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro " + ex.Message);
			}
		}
		public bool DeleteFile(string urlImage, string caminho)
		{
			try
			{
				if (!String.IsNullOrEmpty(urlImage))
				{
					var caminhoImagem = caminhoServer + caminho + urlImage;

					if (File.Exists(caminhoImagem))
					{
						File.Delete(caminhoImagem);

						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
	}
}
