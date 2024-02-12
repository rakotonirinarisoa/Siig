// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apptab.Data
{
    public class Extension
    {
        private readonly SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();

        public string GetCon(int id)
        {
            var CONN = "";
            if (db.SI_PROJETS.FirstOrDefault(a => a.ID == id && a.DELETIONDATE == null) != null)
            {
                var isDB = db.SI_PROJETS.FirstOrDefault(a => a.ID == id && a.DELETIONDATE == null);
                if (db.SI_MAPPAGES.FirstOrDefault(a => a.IDPROJET == isDB.ID && a.DELETIONDATE == null) != null)
                {
                    var isPS = db.SI_MAPPAGES.FirstOrDefault(a => a.IDPROJET == isDB.ID && a.DELETIONDATE == null);

                    var instance = isPS.INSTANCE;
                    var auth = isPS.AUTH;
                    var connex = isPS.CONNEXION;
                    var connexPWD = isPS.CONNEXPWD;
                    var dbase = isPS.DBASE;

                    if (auth == 0)
                        CONN = "Data Source=" + instance + ";Initial Catalog=" + dbase + ";Integrated Security=True";
                    else
                        CONN = "Data Source=" + instance + ";Initial Catalog=" + dbase + ";User ID=" + connex + ";Password=" + connexPWD + ";";
                }
            }

            return (CONN);
        }
    }
}
