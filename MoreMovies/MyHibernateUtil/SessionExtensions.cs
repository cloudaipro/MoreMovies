using NHibernate;
using NHibernate.Engine;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyHibernateUtil
{
	public static class SessionExtensions
	{

		public static void SaveObj(this ISession session, POCOBase obj)
		{
			try
			{
				if (obj.getID() == 0)
				{
					if (obj is DataElement)
						((DataElement)(object)obj).PreAdd();
					session.Save(obj);
				}
				else
				{
					if (obj is DataElement)
						((DataElement)(object)obj).PreUpdate();
					session.Update(obj);
				}
			}
			catch (Exception ex) { throw ex; }
		}

		public static void DeleteObj(this ISession session, POCOBase obj, bool bPermanent = false)
		{
			try
			{
				if (obj is DataElement && bPermanent == false)
				{
					((DataElement)(object)obj).PreDelete();
					if (obj.getID() == 0)
						session.Save(obj);
					else
						session.Update(obj);
				}
				else
				{
					if (obj.getID() == 0) return;
					session.Delete(obj);
				}
			}
			catch (Exception ex) { throw ex; }
		}

		public static IList<T> GetAllObj<T>(this ISession session, string sOrderby = "", int iTake = 0)
		{
			try
			{
				return session.GetXObjs<T>("", sOrderby, iTake);
			}
			catch (Exception ex) { throw ex; }
		}

		public static IList<T> GetXObjs<T>(this ISession session, string sWhere, string sOrderby = "", int iTake = 0)
		{
			try
			{
				string sFinalWhere = "";
				if (typeof(T).IsSubclassOf(typeof(DataElement)))
					sFinalWhere = DataElement.sBasicWhere;
				if (string.IsNullOrWhiteSpace(sWhere) == false)
				{
					if (string.IsNullOrWhiteSpace(sFinalWhere) == false)
						sFinalWhere = " WHERE " + sFinalWhere + " AND (" + sWhere + ") ";
					else
						sFinalWhere = " WHERE " + sWhere + " ";
				}
				else
				{
					if (string.IsNullOrWhiteSpace(sFinalWhere) == false)
						sFinalWhere = " WHERE " + sFinalWhere + " ";
				}


				if (string.IsNullOrWhiteSpace(sOrderby) == false)
					sOrderby = " Order by " + sOrderby;
				if (iTake > 0)
					return session.CreateQuery("from " + typeof(T).Name + " x " + sFinalWhere + sOrderby + " take " + iTake.ToString()).List<T>();
				else
					return session.CreateQuery("from " + typeof(T).Name + " x " + sFinalWhere + sOrderby).List<T>();
			}
			catch (Exception ex) { throw ex; }
		}

		public static T GetObj<T>(this ISession session, int ID, string sIDFieldName = "ID")
		{
			try
			{
				return session.CreateQuery("from " + typeof(T).Name + " where " + sIDFieldName + " = " + ID.ToString()).UniqueResult<T>();
			}
			catch (Exception ex) { throw ex; }
		}

#if NET_4_0
		public static Boolean IsDirtyEntity(this ISession session, Object entity)
		{

			ISessionImplementor sessionImpl = session.GetSessionImplementation();

			IPersistenceContext persistenceContext = sessionImpl.PersistenceContext;

			EntityEntry oldEntry = persistenceContext.GetEntry(entity);

			String className = oldEntry.EntityName;

			IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);



			if ((oldEntry == null) && (entity is INHibernateProxy))
			{

				INHibernateProxy proxy = entity as INHibernateProxy;

				Object obj = sessionImpl.PersistenceContext.Unproxy(proxy);

				oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);

			}



			Object[] oldState = oldEntry.LoadedState;

			Object[] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);




			Int32[] dirtyProps = oldState.Select((o, i) => (oldState[i] == currentState[i]) ? -1 : i).Where(x => x >= 0).ToArray();



			return (dirtyProps != null);

		}

		public static Boolean IsDirtyProperty(this ISession session, Object entity, String propertyName)
		{

			ISessionImplementor sessionImpl = session.GetSessionImplementation();

			IPersistenceContext persistenceContext = sessionImpl.PersistenceContext;

			EntityEntry oldEntry = persistenceContext.GetEntry(entity);

			String className = oldEntry.EntityName;

			IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);




			if ((oldEntry == null) && (entity is INHibernateProxy))
			{

				INHibernateProxy proxy = entity as INHibernateProxy;

				Object obj = sessionImpl.PersistenceContext.Unproxy(proxy);

				oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);

			}



			Object[] oldState = oldEntry.LoadedState;

			Object[] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);

			Int32[] dirtyProps = persister.FindDirty(currentState, oldState, entity, sessionImpl);

			Int32 index = Array.IndexOf(persister.PropertyNames, propertyName);



			Boolean isDirty = (dirtyProps != null) ? (Array.IndexOf(dirtyProps, index) != -1) : false;



			return (isDirty);

		}

		public static Object GetOriginalEntityProperty(this ISession session, Object entity, String propertyName)
		{

			ISessionImplementor sessionImpl = session.GetSessionImplementation();

			IPersistenceContext persistenceContext = sessionImpl.PersistenceContext;

			EntityEntry oldEntry = persistenceContext.GetEntry(entity);

			String className = oldEntry.EntityName;

			IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);



			if ((oldEntry == null) && (entity is INHibernateProxy))
			{

				INHibernateProxy proxy = entity as INHibernateProxy;

				Object obj = sessionImpl.PersistenceContext.Unproxy(proxy);

				oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);

			}



			Object[] oldState = oldEntry.LoadedState;

			Object[] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);

			Int32[] dirtyProps = persister.FindDirty(currentState, oldState, entity, sessionImpl);

			Int32 index = Array.IndexOf(persister.PropertyNames, propertyName);



			Boolean isDirty = (dirtyProps != null) ? (Array.IndexOf(dirtyProps, index) != -1) : false;



			return ((isDirty == true) ? oldState[index] : currentState[index]);

		}
#endif
	}
}
