namespace _20250607_Task2
{
    /*
     Разработать сервис обработки изображений, который загружает, изменяет и сжимает изображения. 
    Если общий объем обработанных изображений в памяти превышает заданный лимит, старые изображения удаляются, 
    чтобы предотвратить переполнение памяти. Использовать библиотеку: SixLabors.ImageSharp — удобная и кроссплатформенная 
    библиотека для работы с изображениями в C#.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ImageProcessingService(memoryLimitMegabytes: 1); // Create image service with 1MB memory limit

            string[] imagePaths = Directory.GetFiles("images", "*.jpg"); // Get all .jpg images from "images" folder

            foreach (var path in imagePaths) // Process each image
            {
                service.ProcessImage(path, newWidth: 200, newHeight: 200, quality: 75); // Resize and compress image
                service.PrintStatus(); // Print memory usage and status
                Thread.Sleep(50); // Simulate delay
            }

            Console.WriteLine("Image processing completed.");
        }
    }
}
