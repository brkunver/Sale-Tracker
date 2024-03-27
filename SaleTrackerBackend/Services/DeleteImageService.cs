namespace SaleTrackerBackend.Services;


public class DeleteImageService
{

  private readonly IWebHostEnvironment _webHostEnvironment;
  public DeleteImageService(IWebHostEnvironment webHostEnvironment)
  {
    _webHostEnvironment = webHostEnvironment;
  }

  public void DeleteImage(string name)
  {
    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", name);
    if (name != "default.jpg" && File.Exists(imagePath))
    {
      File.Delete(imagePath);
    }
  }


}