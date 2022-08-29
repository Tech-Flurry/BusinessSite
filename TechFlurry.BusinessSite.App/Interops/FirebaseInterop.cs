using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Buffers.Text;

namespace TechFlurry.BusinessSite.App.Interops
{
    public interface IFirebaseInterop
    {
        Task InitFirebase();
        void PopulateImage( string fileName, ElementReference e );
        void PopulateImage( string fileName, string selector );
        void UploadFile( string fileName, string base64, string contentType );
    }

    public class FirebaseInterop : InteropBase, IFirebaseInterop
    {
        private readonly ILogger<FirebaseInterop> _logger;

        public FirebaseInterop( IJSRuntime jsRuntime, ILogger<FirebaseInterop> logger ) : base("./js/firebase.js", jsRuntime)
        {
            _logger = logger;
        }


        public async Task InitFirebase()
        {
            var module = await Module;
            await module.InvokeVoidAsync("initFirebase", DotNetObjectReference.Create(this));
        }

        public async void UploadFile( string fileName, string base64, string contentType )
        {
            try
            {
                var module = await Module;
                await module.InvokeVoidAsync("uploadImage", fileName, base64, contentType, nameof(UploadFileCompleted));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in uploading the file. Error: {ex.Message}");
            }
        }

        public async void PopulateImage( string fileName, ElementReference e )
        {
            try
            {
                var module = await Module;
                await module.InvokeVoidAsync("populateImage", e, fileName, nameof(PopulateImageSuccess), nameof(PopulateImageError));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in populating the file. Error: {ex.Message}");
            }
        }
        public async void PopulateImage( string fileName, string selector )
        {
            try
            {
                var module = await Module;
                await module.InvokeVoidAsync("populateImage", selector, fileName, nameof(PopulateImageSuccess), nameof(PopulateImageError));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in populating the file. Error: {ex.Message}");
            }
        }

        [JSInvokable]
        public void UploadFileCompleted()
        {
            _logger.LogInformation("The file has been uploaded");
        }
        [JSInvokable]
        public void PopulateImageSuccess( string fileName )
        {
            _logger.LogInformation($"Image (file: {fileName}) has been populated");
        }
        [JSInvokable]
        public void PopulateImageError( string fileName )
        {
            _logger.LogError($"Error in populating image (file: {fileName})");
        }
    }
}
