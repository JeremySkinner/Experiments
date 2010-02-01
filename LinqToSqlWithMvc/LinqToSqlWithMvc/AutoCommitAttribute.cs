namespace LinqToSqlWithMvc
{
	using System.Transactions;
	using System.Web.Mvc;
	using LinqToSqlWithMvc.Models;
	using StructureMap;

	public class AutoCommitAttribute : ActionFilterAttribute {
		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			if (filterContext.Controller.ViewData.ModelState.IsValid) {
				var currentDataContext = ObjectFactory.GetInstance<BlogDataContext>();

				using (var transaction = new TransactionScope()) {
					currentDataContext.SubmitChanges();
					transaction.Complete();
				}
			}
		}
	}
}