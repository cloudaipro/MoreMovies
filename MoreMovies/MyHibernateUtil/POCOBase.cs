using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NHibernate;

namespace MyHibernateUtil
{
	public enum eOPSTATE
	{
		NONE = 1,
		NEW = 2,
		DIRTY = 4,
		DELETE = 16,
		OTHER = 32,
	}
	public abstract class POCOBase : INotifyPropertyChanged
	{
		public abstract int getID();

		public static bool bEditMode = false;
		private eOPSTATE _iState = eOPSTATE.NONE;
		public virtual eOPSTATE iState
		{
			get { return _iState; }
			set
			{
				_iState = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("iState"));
			}
		}



		public virtual event PropertyChangedEventHandler PropertyChanged;
		//        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		public virtual void OnPropertyChanged(string name)
		{
			if (bEditMode == true && iState == eOPSTATE.NONE)
			//	iState = eOPSTATE.NONE;
			//else if (iState == eOPSTATE.NONE)
				iState = eOPSTATE.DIRTY;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public virtual Object ShallowCopy()
		{
			return MemberwiseClone();
		}

	}
}
