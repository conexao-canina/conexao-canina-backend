using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.ViewModel
{
	public class HealthHistoryViewModel
	{
		public Guid HealthHistoryId { get; set; }
		public string Exam { get; set; }
		public string Vaccine { get; set; }
		public string ConditionsOfHealth { get; set; }
		public bool OwnerConsent { get; set; }
		public DateTime ExamDate { get; set; }
	}
}
