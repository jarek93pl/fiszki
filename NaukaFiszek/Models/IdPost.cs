﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Models
{
    public class IdPost
    {
        public IdPost()
        {

        }
        public IdPost(int id)
        {
            this.id = id;
        }
        public int id { get; set; }
    }
}