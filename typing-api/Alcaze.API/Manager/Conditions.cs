using Alcaze.Helper.Exceptions;
using Alcaze.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.API
{
    public class Conditions : List<Tuple<string, ComparisonOperator, object>>
    {
        public void AddCondition(string property, ComparisonOperator comparison, object value)
        {
            this.Add(new Tuple<string, ComparisonOperator, object>(property, comparison, value));
        }
        /// <summary>
        /// Agrega filtro desde querystring
        /// </summary>
        /// <param name="filter">
        /// filter={PropertyName},{ComparitionType},{someValue }|{PropertyName},{ComparitionType},{someValue }|…
        /// {PropertyName} = nombre de la propiedad por la que se desea filtrar
        /// {ComparitionType} = Un entero con los siguientes valores válidos.
        ///         Contains=0,
        ///         Equal= ExpressionType.Equal = 13,
        ///         NotEqual= ExpressionType.NotEqual = 35 ,
        ///         GreaterThanOrEqual= ExpressionType.GreaterThanOrEqual = 16,
        ///         LessThanOrEqual =ExpressionType.LessThanOrEqual = 21,
        /// {someValue} = valor por el que se filtrará
        /// </param>
        public void AddFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter) == true)
                return;
            foreach (var conditionString in filter.Split('|'))
            {
                if (string.IsNullOrWhiteSpace(conditionString))
                    continue;
                var condition = conditionString.Split(',');
                if (condition.Length != 3)
                    throw new NotAcceptableException("");
                AddCondition(condition[0], (ComparisonOperator)Convert.ToInt32(condition[1]), condition[2]);
            }
        }
    }
}
