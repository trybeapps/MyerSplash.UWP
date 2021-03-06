﻿using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MyerSplashCustomControl
{
    public class PopupService
    {
        private static volatile PopupService _instance;
        private static object syncRoot = new object();

        private ContentPopupEx _shownCPEX { get; set; }

        public bool CanHide
        {
            get
            {
                return _shownCPEX != null;
            }
        }

        public async Task ShowAsync(FrameworkElement element, LayoutStretch layout = LayoutStretch.Center, bool allowTapToHide = true)
        {
            TryHide();
            _shownCPEX = new ContentPopupEx(element, layout)
            {
                AllowTapMaskToHide = allowTapToHide
            };
            await _shownCPEX.ShowAsync();
        }

        public static PopupService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new PopupService();
                    }
                }
                return _instance;
            }
        }

        public void TryHide()
        {
            if (_shownCPEX != null)
            {
                _shownCPEX.Hide();
                _shownCPEX = null;
            }
        }

        public async void TryHide(int delay)
        {
            await Task.Delay(delay);
            TryHide();
        }
    }
}