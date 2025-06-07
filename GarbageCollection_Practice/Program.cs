namespace GarbageCollection_Practice
{

    /*
     Разработать систему мониторинга использования памяти для большого количества объектов, 
    где при достижении определенного порога памяти (например, 100 MB) неиспользуемые объекты должны освобождаться вручную.

Создайте класс LargeObjectManager, который управляет созданием больших объектов, 
    контролирует использование памяти и очищает их, если лимит превышен.

Пример большого объекта:  _objects.Add(new byte[sizeMB * 1024 * 1024]);

    private List<byte[]> _objects = new List<byte[]>();  
_objects.Add(new byte[sizeMB * 1024 * 1024]);
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            LargeObjectManager manager = new LargeObjectManager(100);

            for (int i = 0; i < 100; i++)
            {
                manager.AddLargeObject(100);
                manager.CheckLimits();
                Thread.Sleep(10);
            }
            Console.ReadKey();
        }
    }
}
