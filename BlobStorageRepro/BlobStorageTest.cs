using System;
using System.IO;
using Azure.Storage.Blobs;
using NUnit.Framework;

namespace BlobStorageRepro
{
    [TestFixture]
    public class BlobStorageTest
    {
        [Test]
        public async void ShouldUploadWithoutError()
        {
            var localFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "test.txt");
            await File.WriteAllTextAsync(localFilePath, "this is a test text document");
            var fileName = Path.GetFileName(localFilePath);
            var container =
                new BlobContainerClient("/* input connection string here */", "/* input container name here*/");
            try
            {
                await container.GetBlobClient(fileName).UploadAsync(localFilePath, true);
            }
            catch (Exception exception)
            {
                Assert.Fail("Upload failed with exception: " + exception);
            }
        }
    }
}