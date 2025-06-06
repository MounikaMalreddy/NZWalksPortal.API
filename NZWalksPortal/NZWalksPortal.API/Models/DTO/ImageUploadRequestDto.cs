﻿using System.ComponentModel.DataAnnotations;

namespace NZWalksPortal.API.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string? FileDescription { get; set; }
    }
}
