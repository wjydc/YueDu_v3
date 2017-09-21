using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModel
{
	public class ModelValidate_Demo : IValidatableObject
	{
		[StringLength(10, MinimumLength = 4)]
		public string Name { get; set; }

		[DataType(DataType.Date)]
		[Remote("ValidateDate", "Home")]//调用jq插件远程验证
		public DateTime Data { get; set; }

		public bool Show { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			List<ValidationResult> errors = new List<ValidationResult>();
			if (string.IsNullOrEmpty(Name))
			{
				errors.Add(new ValidationResult("用户名不能为空"));
			}

			if (DateTime.Now > Data)
			{
				errors.Add(new ValidationResult("预约日期要大于当前日期"));
			}
			if (!Show)
			{
				errors.Add(new ValidationResult("请选择可见"));
			}

			if (errors.Count == 0 && Name == "admin" && Data.DayOfWeek == DayOfWeek.Friday)
			{
				errors.Add(new ValidationResult("admin 不能选择星期五"));
			}

			return errors;
		}
	}
}