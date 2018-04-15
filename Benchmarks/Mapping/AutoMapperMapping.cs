﻿using AutoMapper;
using Benchmarks.Enums;
using Benchmarks.Models;
using Benchmarks.ViewModels;

namespace Benchmarks.Mapping
{
    public static class AutoMapperMapping
    {
        public static void Init()
        {
            Mapper.Reset();
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<ProductVariant, ProductVariantViewModel>();
                    cfg.CreateMap<Product, ProductViewModel>()
                        .ForMember(dest => dest.DefaultSharedOption, src => src.MapFrom(m => m.DefaultOption));
                    cfg.CreateMap<Test, TestViewModel>()
                        .BeforeMap((src, dest) => dest.Age = src.Age)
                        .AfterMap((src, dest) => dest.Weight = src.Weight * 2)
                        .ForMember(dest => dest.Age, src => src.Ignore())
                        .ForMember(dest => dest.Type, src => src.MapFrom(m => (Types)m.Type))
                        .ForMember(dest => dest.Name, src => src.MapFrom(m => string.Format("{0} - {1} - {2}", m.Name, m.Weight, m.Age)))
                        .ForMember(dest => dest.SpareTheProduct, src => src.ResolveUsing(m => m.SpareProduct))
                        .ConstructUsing((src => new TestViewModel(string.Format("{0} - {1}", src.Name, src.Id))));

                    cfg.CreateMap<News, NewsViewModel>();

                    cfg.CreateMap<Role, RoleViewModel>();
                    cfg.CreateMap<User, UserViewModel>()
                        .ForMember(dest => dest.BelongTo, src => src.MapFrom(m => m.Role));

                    cfg.CreateMap<Article, ArticleViewModel>();
                    cfg.CreateMap<Author, AuthorViewModel>()
                        .ForMember(dest => dest.OwnedArticles, src => src.ResolveUsing(m => m.Articles));

                    cfg.CreateMap<Item, ItemViewModel>();
                });
            

            Mapper.Configuration.CompileMappings();
        }

        public static void InitAdvanced()
        {
            Mapper.Reset();
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<ProductVariant, ProductVariantViewModel>();
                    cfg.CreateMap<Product, ProductViewModel>()
                        .ForMember(dest => dest.DefaultSharedOption, src => src.MapFrom(m => m.DefaultOption));
                    cfg.CreateMap<Test, TestViewModel>()
                        .ForMember(dest => dest.Type, src => src.MapFrom(m => (Types) m.Type))
                        .ForMember(dest => dest.Age, src => src.MapFrom(m => m.Age))
                        .ForMember(dest => dest.Weight, src => src.MapFrom(m => m.Weight*2))
                        .ForMember(dest => dest.Name,
                            src => src.MapFrom(m => string.Format("{0} - {1} - {2}", m.Name, m.Weight, m.Age)))
                        .ForMember(dest => dest.SpareTheProduct, src => src.MapFrom(m => m.SpareProduct))
                        .ForMember(dest => dest.Description, src => src.MapFrom(m => string.Format("{0} - {1}", m.Name, m.Id)));

                    cfg.CreateMap<News, NewsViewModel>();

                    cfg.CreateMap<Role, RoleViewModel>();
                    cfg.CreateMap<User, UserViewModel>()
                        .ForMember(dest => dest.BelongTo, src => src.MapFrom(m => m.Role));

                    cfg.CreateMap<Article, ArticleViewModel>();
                    cfg.CreateMap<Author, AuthorViewModel>()
                        .ForMember(dest => dest.OwnedArticles, src => src.ResolveUsing(m => m.Articles));

                    cfg.CreateMap<Item, ItemViewModel>();
                });

            Mapper.Configuration.CompileMappings();
        }
    }
}
