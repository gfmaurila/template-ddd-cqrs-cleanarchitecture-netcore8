using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Common.Domain.Events.Decorators;
using Template.Common.Domain.Events;
using Template.Domain.Common;

namespace Template.Domain.Users.Events;

[AggregateType(UserEventConstants.UserAggregateTypeName)]
public abstract class BaseUserDomainEvent(Guid aggregateId, DateTimeOffset? occurredOnUtc = null)
        : DomainEvent(aggregateId, occurredOnUtc ?? DateTimeOffset.UtcNow)
{ }
