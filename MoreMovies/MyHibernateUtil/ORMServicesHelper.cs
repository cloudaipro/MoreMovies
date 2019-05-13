using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyHibernateUtil
{
	public class ORMServicesHelper
	{
		protected bool bInitialed = false;
		private NHibernateHelper NHB { get; } = new NHibernateHelper();
		protected ISession oSession { get { return NHB.Session; } }

		private NHibernateHelper NHB1 { get; } = new NHibernateHelper(); // for read only session
		protected ISession oSession1 { get { return NHB1.Session; } }
		private string sConfigureFile = "";
		protected List<string> getFactoryList()
		{
			try
			{
				return (string.IsNullOrWhiteSpace(sConfigureFile))? new List<string>() : MyConfigurationExtensions.getFactoryList(sConfigureFile);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void DoDBConnect(string sFileName, string FactoryName = "")
		{
			try
			{
				sConfigureFile = sFileName;
				NHB.Initialize(sFileName, FactoryName);
				NHB1.Initialize(sFileName, FactoryName);
				bInitialed = true;
			}
			catch (Exception ex) { throw ex; }
		}

		public void DoDBDispose()
		{
			try
			{
				NHB?.Dispose();
				NHB1?.Dispose();
			}
			catch (Exception ex) { throw ex; }
		}

	}
}
