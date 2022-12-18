using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using MyNetWebApp.Helpers;
using MyNetWebApp.Interfaces;

namespace MyNetWebApp.Services
{
	public class PhotoService : IPhotoService
	{
        private readonly Cloudinary _cloudinary;
		public PhotoService(IOptions<CloudinarySettings> configs)
		{
            var account = new Account(
                configs.Value.CloudName,
                configs.Value.ApiKey,
                configs.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var imageUploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                Stream stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(200).Width(400).Crop("fill").Gravity(Gravity.Face)

                };

                imageUploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return imageUploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}

