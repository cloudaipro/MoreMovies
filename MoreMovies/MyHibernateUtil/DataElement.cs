using System;

namespace MyHibernateUtil
{
	public abstract class DataElement : POCOBase
	{
		public virtual string DataStatus { get; set; } = "";
		public virtual string CreatedID { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual string UpdatedID { get; set; }
		public virtual DateTime UpdatedDate { get; set; }

		public static string sDefaultUserID { get; set; } = "";

		public virtual void PreAdd()
		{
			DataStatus = eDATASTATUS.ACTIVE;
			CreatedDate = DateTime.Now;
			UpdatedDate = DateTime.Now;
			CreatedID = sDefaultUserID;
			UpdatedID = sDefaultUserID;
		}

		public virtual void PreUpdate()
		{
			UpdatedDate = DateTime.Now;
			UpdatedID = sDefaultUserID;
		}

		public virtual void PreDelete()
		{
			UpdatedDate = DateTime.Now;
			UpdatedID = sDefaultUserID;
			DataStatus = eDATASTATUS.DELETE;
		}

		public static string sBasicWhere { get; } = "DataStatus <> '" + eDATASTATUS.DELETE + "'";
	}
}
