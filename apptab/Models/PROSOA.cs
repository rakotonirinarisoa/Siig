<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace apptab.Models
=======
﻿namespace apptab.Models
>>>>>>> 4125ae2d4be38c88afefd42bbf507c1316e38fbf
{
    public class PROSOA
    {
        public int? PROJET { get; set; }

        public int? SOA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DELETIONDATE { get; set; }
    }
}