using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Lambda
{
    public enum ComparisonOperator
    {
        Contains=0,
        Equal= ExpressionType.Equal,
        NotEqual= ExpressionType.NotEqual,
        GreaterThanOrEqual= ExpressionType.GreaterThanOrEqual,
        LessThanOrEqual =ExpressionType.LessThanOrEqual,
    }
}
