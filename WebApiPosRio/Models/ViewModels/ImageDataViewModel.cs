using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class ImageDataViewModel
    {
        public string Url { get; set; }

        public bool Estatus { get; set; }

        public string Slug { get; set; }
    }

    public class BodyUploadImageStore
    {
        public List<ImageDataViewModel> ImagesList { get; set; }
    }
}
