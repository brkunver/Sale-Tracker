namespace SaleTrackerBackend.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;


public class CreateImageService
{
  private readonly IWebHostEnvironment _webHostEnvironment;
  public CreateImageService(IWebHostEnvironment webHostEnvironment)
  {
    _webHostEnvironment = webHostEnvironment;
  }

  public async Task<string> CreateImage(IFormFile file)
  {
    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
    if (!Directory.Exists(uploadsFolder))
    {
      Directory.CreateDirectory(uploadsFolder);
    }

    var fileName = Guid.NewGuid().ToString() + ".jpg";
    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }

    var image = Image.Load(filePath);
    int newWidth, newHeight;

    if (image.Width > image.Height)
    {
      newWidth = 500;
      newHeight = (int)((500f / image.Width) * image.Height);
    }
    else
    {
      newHeight = 500;
      newWidth = (int)((500f / image.Height) * image.Width);
    }

    image.Mutate(x => x.Resize(newWidth, newHeight));

    // Düzenlenmiş resmi kaydet
    image.Save(filePath, new JpegEncoder());
    return fileName;
  }


}