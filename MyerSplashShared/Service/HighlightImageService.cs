﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using MyerSplash.Data;

namespace MyerSplashShared.Service
{
    public class HighlightImageService : ImageServiceBase
    {
        private static DateTime END_TIME => DateTime.Parse("2017/03/20");
        private static int COUNT => 20;

        public HighlightImageService(UnsplashImageFactory factory) : base(factory)
        {
        }

        public async override Task<IEnumerable<UnsplashImage>> GetImagesAsync(CancellationToken token)
        {
            var list = new ObservableCollection<UnsplashImage>();

            var start = DateTime.Now.AddDays(-(Page - 1) * COUNT);

            for (var i = 0; i < COUNT; i++)
            {
                var next = start.AddDays(-i);
                if (next < END_TIME)
                {
                    break;
                }
                list.Add(UnsplashImageFactory.CreateHighlightImage(next, true));
            }

            return await Task.FromResult(list);
        }
    }
}
