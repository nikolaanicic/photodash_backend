using System;

namespace Entities.Dtos.PostDtos
{
    public class FindPostDto
    {
        public string OwnerUserName { get; set; }
        public Guid PostId { get; set; }
    }
}
