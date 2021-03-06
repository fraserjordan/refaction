﻿using System;

namespace refactor_me.ViewModels
{
    public class ProductOptionViewModel
    {
        public Guid? Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}