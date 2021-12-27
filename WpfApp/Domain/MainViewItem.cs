﻿using System;
using System.Windows.Controls;

namespace WpfApp.Domain
{
    public class MainViewItem
    {
        public string Name { get; }

        private readonly Type _contentType;
        private UserControl? _content;

        public UserControl? Content
        {
            get
            {
                if (null == _content)
                {
                    _content = Activator.CreateInstance(_contentType) as UserControl;
                }
                return _content;
            }
        }

        public MainViewItem(string name, Type contentType)
        {
            Name = name;
            _contentType = contentType;
        }
    }
}
