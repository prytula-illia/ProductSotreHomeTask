using Microsoft.AspNetCore.Http;

namespace DAL.Entities
{
    /// <summary>
    /// File with image
    /// </summary>
    public class FileUpload
    {
        /// <summary>
        /// Image id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Image placeholder
        /// </summary>
        public IFormFile FormFile { get; set; }
    }
}
