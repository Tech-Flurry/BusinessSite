using Microsoft.JSInterop;

namespace TechFlurry.BusinessSite.App.Interops
{
    public interface IFirebaseInterop
    {
        Task InitFirebase();
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
        [JSInvokable]
        public void UploadFileCompleted()
        {
            _logger.LogInformation("The file has been uploaded");
        }
    }
}
