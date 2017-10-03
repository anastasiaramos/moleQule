using System;
using System.Collections;
using System.Collections.Generic;

namespace moleQule.Library
{
	public interface ICronJob
	{
		string ID { get; set; }
		int Interval { get; set; }
		Dictionary<long, DateTime> LastExecutions { get; set; }
		EComponentStatus Status { get; set; } 

		void Run(object parameter = null);
		EComponentStatus DoRun(object parameter = null);
		EComponentStatus CheckStatus(ISchemaInfo schema);
	}

	public abstract class CronJobBase: ICronJob
    {
		public string ID { get; set; }
		public int Interval { get; set; }
		public Dictionary<long, DateTime> LastExecutions { get; set; }
		public EComponentStatus Status { get; set; }

		protected CronJobBase()
		{
			Status = EComponentStatus.READY;
			LastExecutions = new Dictionary<long, DateTime>();
        }

		public EComponentStatus CheckStatus(ISchemaInfo	schema)
		{
			switch (Status)
			{
				case EComponentStatus.DISABLED:
				case EComponentStatus.WORKING:
					{
						return Status;
					}

				default:
					{
						//Para la primera vez que se consulta
						if (!LastExecutions.ContainsKey(schema.Oid))
						{
							UpdateLastExecution(schema);
#if DEBUG
							LastExecutions[schema.Oid] = DateTime.MinValue;
#endif
						}

						if (DateAndTime.DateDiff(DateInterval.Second, LastExecutions[schema.Oid], DateTime.Now) < Interval)
						{
							return EComponentStatus.UNAVAILABLE;
						}

						return EComponentStatus.READY;
					}
			}
		}

		public void Run(object parameter = null)
		{
			Status = CheckStatus((ISchemaInfo)parameter);
			
			if (Status != EComponentStatus.READY) return;

			Status = EComponentStatus.WORKING;

			try
			{
				//Before calling. Maybe could be an error
				UpdateLastExecution((ISchemaInfo)parameter);

				Status = DoRun(parameter);

				Status = EComponentStatus.OK;

				//After calling. Real last update
				UpdateLastExecution((ISchemaInfo)parameter);

				MyLogger.LogText("COMPLETE", string.Format("{0}::Run", ID));
			}
			catch (Exception ex)
			{
				MyLogger.LogText("FINALIZED WITH ERROR", string.Format("{0}::Run", ID));
				MyLogger.LogException(ex, string.Format("{0}::Run", ID));
				Status = EComponentStatus.ERROR;
			}

			return;
		}

		public abstract EComponentStatus DoRun(object parameter);

		protected void UpdateLastExecution(ISchemaInfo schema)
		{
			if (LastExecutions.ContainsKey(schema.Oid))
				LastExecutions[schema.Oid] = DateTime.Now;
			else
				LastExecutions.Add(schema.Oid, DateTime.Now);
		}
	}
}

