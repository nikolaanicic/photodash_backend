using AutoMapper;
using Entities.Dtos.CommentDtos;
using Entities.Dtos.PostDtos;
using Entities.Dtos.UserDtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoDash
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<PostForCreationDto, Post>();
            CreateMap<Post, PostForReplyDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<CommentForCreationDto, Comment>();
            CreateMap<User, UserForReplyDto>();
        }
    }
}
