using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250607_Task2
{
    public class ImageProcessingService
    {
        // Maximum allowed memory usage in bytes
        private readonly long _memoryLimitBytes;
        // Stores processed images and their size
        private readonly Dictionary<string, (Image image, long size)> _processedImages = new(); 
        // Maintains order to know which images were added first
        private readonly Queue<string> _processingOrder = new();

        // Constructor
        public ImageProcessingService(long memoryLimitMegabytes)
        {
            _memoryLimitBytes = memoryLimitMegabytes * 1024 * 1024; // Convert MB to bytes
        }

        public void ProcessImage(string filePath, int newWidth, int newHeight, int quality)
        {
            Console.WriteLine($"Processing: {filePath}");

            // Load image from file
            using var image = Image.Load(filePath);
            // Resize image to new dimensions
            image.Mutate(x => x.Resize(newWidth, newHeight));

            // Create memory stream for compression
            var compressedStream = new MemoryStream();
            // Compress image with JPEG encoder
            image.Save(compressedStream, new JpegEncoder { Quality = quality });
            // Reset stream position to beginning
            compressedStream.Seek(0, SeekOrigin.Begin);

            // Load processed image back from stream
            var processedImage = Image.Load(compressedStream);
            // Get the size of the image in bytes
            long size = compressedStream.Length;

            // Use filename as key
            string key = Path.GetFileName(filePath);
            // Add the processed image to in-memory cache
            AddImageToMemory(key, processedImage, size);

            // Log size of processed image
            Console.WriteLine($"Processed {key}: {size / 1024} KB");
        }

        // Adds a new image to memory and manages memory limit
        private void AddImageToMemory(string key, Image image, long size)
        {
            // If adding this image would exceed the memory limit, remove oldest images
            while (GetCurrentMemoryUsage() + size > _memoryLimitBytes)
            {
                RemoveOldestImage(); // Remove oldest image to free up memory
            }

            _processedImages[key] = (image, size); // Store image and its size
            _processingOrder.Enqueue(key); // Record processing order
        }

        // Removes the oldest image from memory
        private void RemoveOldestImage()
        {
            // If no images, return
            if (_processingOrder.Count == 0) return;

            // Get the oldest image key
            string oldestKey = _processingOrder.Dequeue();
            if (_processedImages.TryGetValue(oldestKey, out var data)) // If image exists
            {
                data.image.Dispose(); // Release image memory (Dispose the image to free resources)
                _processedImages.Remove(oldestKey); // Remove it from the dictionary
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Removed image: {oldestKey} to free memory"); 
                Console.ResetColor();
            }

            GC.Collect(); // Force garbage collection to clean up memory
            GC.WaitForPendingFinalizers(); // // Wait for all finalizers to complete
        }
        // Calculates total memory currently used by processed images
        private long GetCurrentMemoryUsage()
        {
            return _processedImages.Values.Sum(x => x.size); // Sum sizes of all images
        }

        // Prints current memory usage and how many images are stored
        public void PrintStatus()
        {
            Console.WriteLine($"\nCurrent memory usage: {GetCurrentMemoryUsage() / 1024} KB");
            Console.WriteLine($"Images in memory: {_processedImages.Count}\n");
        }
    }
}
