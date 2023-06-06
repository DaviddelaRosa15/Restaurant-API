using RestaurantAppi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Domain.Entities
{
	public class RefreshToken : AuditableBaseEntity
	{
		public string Token { get; set; }
		public string UserId { get; set; }
		public DateTime Expires { get; set; }
		public bool IsExpired => DateTime.UtcNow >= Expires;
		public DateTime? Revoked { get; set; }
		public string ReplacedByToken { get; set; }
		public bool IsActive => Revoked == null && !IsExpired;
	}
}
