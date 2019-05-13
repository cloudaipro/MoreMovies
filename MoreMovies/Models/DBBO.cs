using MyHibernateUtil;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreMovies.Models
{
    public class DBBO : ORMServicesHelper
    {
        private static DBBO oHelper = new DBBO();
        public static ISession Session { get { return oHelper.oSession; } }
        public static ISession Session1 { get { return oHelper.oSession1; } } // for read only session
        public static IStatelessSession StatelessSession { get { return oHelper.oStatelessSession; } }
        public static void DBConnect(string sFileName, string FactoryName = "") { oHelper.DoDBConnect(sFileName, FactoryName); }
        public static void DBDispose() { oHelper.DoDBDispose(); }

    }
}
